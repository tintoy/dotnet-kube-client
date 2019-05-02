using HTTPlease;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Samples.DeploymentWithRollback
{
    using Extensions.KubeConfig.Models;
    using Models;

    /// <summary>
    ///     A sample program that demonstrates creating a K8s deployment, updating it, and then rolling back to an older version.
    /// </summary>
    static class Program
    {
        /// <summary>
        ///     The main program entry-point.
        /// </summary>
        /// <param name="commandLineArguments">
        ///     The program's command-line arguments.
        /// </param>
        /// <returns>
        ///     The program exit-code.
        /// </returns>
        static async Task<int> Main(string[] commandLineArguments)
        {
            ProgramOptions options = ProgramOptions.Parse(commandLineArguments);
            if (options == null)
                return ExitCodes.InvalidArguments;

            ILoggerFactory loggerFactory = ConfigureLogging(options);

            try
            {
                KubeClientOptions clientOptions = K8sConfig.Load().ToKubeClientOptions(defaultKubeNamespace: options.KubeNamespace, loggerFactory: loggerFactory);
                if (options.Verbose)
                    clientOptions.LogPayloads = true;

                KubeApiClient client = KubeApiClient.Create(clientOptions);

                Log.Information("Looking for existing Deployment {DeploymentName} in namespace {KubeNamespace}...",
                    options.DeploymentName,
                    options.KubeNamespace
                );

                DeploymentV1 existingDeployment = await client.DeploymentsV1().Get(options.DeploymentName, options.KubeNamespace);
                if (existingDeployment != null)
                {
                    Log.Error("Cannot continue - deployment  {DeploymentName} in namespace {KubeNamespace} already exists.",
                        options.DeploymentName,
                        options.KubeNamespace
                    );

                    return ExitCodes.AlreadyExists;
                }

                Log.Information("Ok, Deployment does not exist yet - we're ready to go.");

                Log.Information("Creating Deployment {DeploymentName} in namespace {KubeNamespace}...",
                    options.DeploymentName,
                    options.KubeNamespace
                );
                DeploymentV1 initialDeployment = await CreateInitialDeployment(client, options.DeploymentName, options.KubeNamespace);
                int? initialRevision = initialDeployment.GetRevision();
                if (initialRevision == null)
                {
                    Log.Error("Unable to determine initial revision of Deployment {DeploymentName} in namespace {KubeNamespace} (missing annotation).",
                        options.DeploymentName,
                        options.KubeNamespace
                    );

                    return ExitCodes.UnexpectedError;
                }
                Log.Information("Created Deployment {DeploymentName} in namespace {KubeNamespace} (revision {DeploymentRevision}).",
                    options.DeploymentName,
                    options.KubeNamespace,
                    initialRevision
                );

                Log.Information("Updating Deployment {DeploymentName} in namespace {KubeNamespace}...",
                    options.DeploymentName,
                    options.KubeNamespace
                );
                DeploymentV1 updatedDeployment = await UpdateDeployment(client, initialDeployment);
                int? updatedRevision = updatedDeployment.GetRevision();
                if (updatedRevision == null)
                {
                    Log.Error("Unable to determine updated revision of Deployment {DeploymentName} in namespace {KubeNamespace} (missing annotation).",
                        options.DeploymentName,
                        options.KubeNamespace
                    );

                    return ExitCodes.UnexpectedError;
                }
                Log.Information("Updated Deployment {DeploymentName} in namespace {KubeNamespace} (revision {DeploymentRevision}).",
                    options.DeploymentName,
                    options.KubeNamespace,
                    updatedRevision
                );

                Log.Information("Searching for ReplicaSet that corresponds to revision {Revision} of {DeploymentName} in namespace {KubeNamespace}...",
                    options.DeploymentName,
                    options.KubeNamespace,
                    initialRevision
                );
                ReplicaSetV1 targetReplicaSet = await FindReplicaSetForRevision(client, updatedDeployment, initialRevision.Value);
                if (targetReplicaSet == null)
                {
                    Log.Error("Cannot find ReplicaSet that corresponds to revision {Revision} of {DeploymentName} in namespace {KubeNamespace}...",
                        options.DeploymentName,
                        options.KubeNamespace,
                        initialRevision
                    );

                    return ExitCodes.NotFound;
                }
                Log.Information("Found ReplicaSet {ReplicaSetName} in namespace {KubeNamespace}.",
                    targetReplicaSet.Metadata.Name,
                    targetReplicaSet.Metadata.Namespace
                );

                Log.Information("Rolling Deployment {DeploymentName} in namespace {KubeNamespace} back to initial revision {DeploymentRevision}...",
                    options.DeploymentName,
                    options.KubeNamespace,
                    initialRevision
                );
                DeploymentV1 rolledBackDeployment = await RollbackDeployment(client, updatedDeployment, targetReplicaSet);
                Log.Information("Rollback initiated for Deployment {DeploymentName} in namespace {KubeNamespace} from revision {FromRevision} to {ToRevision} (new revision will be {NewRevision})...",
                    options.DeploymentName,
                    options.KubeNamespace,
                    updatedRevision,
                    initialRevision,
                    rolledBackDeployment.GetRevision()
                );

                return ExitCodes.Success;
            }
            catch (HttpRequestException<StatusV1> kubeError)
            {
                Log.Error(kubeError, "Kubernetes API error: {@Status}", kubeError.Response);

                return ExitCodes.UnexpectedError;
            }
            catch (Exception unexpectedError)
            {
                Log.Error(unexpectedError, "Unexpected error.");

                return ExitCodes.UnexpectedError;
            }
        }

        /// <summary>
        ///     Create the initial Deployment.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        /// <param name="deploymentName">
        ///     The name of the Deployment to create.
        /// </param>
        /// <param name="deploymentNamespace">
        ///     The name of the Kubernetes namespace in which the Deployment will be created.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the Deployment's current state.
        /// </returns>
        static async Task<DeploymentV1> CreateInitialDeployment(IKubeApiClient client, string deploymentName, string deploymentNamespace)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (String.IsNullOrWhiteSpace(deploymentName))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'deploymentName'.", nameof(deploymentName));
            
            if (String.IsNullOrWhiteSpace(deploymentNamespace))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'deploymentNamespace'.", nameof(deploymentNamespace));
            
            DeploymentV1 initialDeployment = await client.DeploymentsV1().Create(new DeploymentV1
            {
                Metadata = new ObjectMetaV1
                {
                    Name = deploymentName,
                    Namespace = deploymentNamespace
                },
                Spec = new DeploymentSpecV1
                {
                    Selector = new LabelSelectorV1
                    {
                        MatchLabels =
                        {
                            ["app"] = deploymentName
                        }
                    },
                    Replicas = 2,
                    RevisionHistoryLimit = 3,
                    Template = new PodTemplateSpecV1
                    {
                        Metadata = new ObjectMetaV1
                        {
                            Labels =
                            {
                                ["app"] = deploymentName
                            }
                        },
                        Spec = new PodSpecV1
                        {
                            Containers =
                            {
                                new ContainerV1
                                {
                                    Name = "demo-container",
                                    Image = "ubuntu:xenial",
                                    Command = { "/bin/sleep", "20m" }
                                }
                            }
                        }
                    }
                }
            });

            // Re-fetch Deployment state so we pick up annotations added by the controller.
            initialDeployment = await client.DeploymentsV1().Get(initialDeployment.Metadata.Name, initialDeployment.Metadata.Namespace);

            return initialDeployment;
        }

        /// <summary>
        ///     Update a Deployment, modifying its Command.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        /// <param name="existingDeployment">
        ///     A <see cref="DeploymentV1"/> representing the Deployment to update.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the Deployment's current state.
        /// </returns>
        static async Task<DeploymentV1> UpdateDeployment(IKubeApiClient client, DeploymentV1 existingDeployment)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (existingDeployment == null)
                throw new ArgumentNullException(nameof(existingDeployment));
            
            DeploymentV1 updatedDeployment = await client.DeploymentsV1().Update(existingDeployment.Metadata.Name, kubeNamespace: existingDeployment.Metadata.Namespace, patchAction: patch =>
            {
                patch.Replace(
                    path: deployment => deployment.Spec.Template.Spec.Containers[0].Command,
                    value: new List<string> { "/bin/sleep", "30m" }
                );
            });

            // Re-fetch Deployment state so we pick up annotations added or updated by the controller.
            updatedDeployment = await client.DeploymentsV1().Get(updatedDeployment.Metadata.Name, updatedDeployment.Metadata.Namespace);

            return updatedDeployment;
        }

        /// <summary>
        ///     Find the ReplicaSet that corresponds to the specified revision of the specified Deployment.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        /// <param name="deployment">
        ///     The target Deployment.
        /// </param>
        /// <param name="targetRevision">
        ///     The target revision.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicaSetV1"/> representing the ReplicaSet's current state; <c>null</c>, if no corresponding ReplicaSet was found.
        /// </returns>
        static async Task<ReplicaSetV1> FindReplicaSetForRevision(IKubeApiClient client, DeploymentV1 deployment, int targetRevision)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));
            
            ReplicaSetListV1 replicaSets = await client.ReplicaSetsV1().List(
                labelSelector: $"app={deployment.Metadata.Name}",
                kubeNamespace: deployment.Metadata.Namespace
            );

            ReplicaSetV1 targetRevisionReplicaSet = replicaSets.Items.FirstOrDefault(
                replicaSet => replicaSet.GetRevision() == targetRevision
            );

            return targetRevisionReplicaSet;
        }

        /// <summary>
        ///     Roll back a Deployment to the revision represented by the specified ReplicaSet.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        /// <param name="existingDeployment">
        ///     The target Deployment.
        /// </param>
        /// <param name="targetRevisionReplicaSet">
        ///     The ReplicaSet that represents the target revision.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the Deployment's current state.
        /// </returns>
        static async Task<DeploymentV1> RollbackDeployment(IKubeApiClient client, DeploymentV1 existingDeployment, ReplicaSetV1 targetRevisionReplicaSet)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (existingDeployment == null)
                throw new ArgumentNullException(nameof(existingDeployment));
            
            if (targetRevisionReplicaSet == null)
                throw new ArgumentNullException(nameof(targetRevisionReplicaSet));

            if (!DoesDeploymentOwnReplicaSet(existingDeployment, targetRevisionReplicaSet))
                throw new InvalidOperationException($"ReplicaSet '{targetRevisionReplicaSet.Metadata.Name}' in namespace '{targetRevisionReplicaSet.Metadata.Namespace}' is not owned by Deployment '{existingDeployment.Metadata.Name}'.");

            int? targetRevision = targetRevisionReplicaSet.GetRevision();
            if (targetRevision == null)
                throw new InvalidOperationException($"Cannot determine Deployment revision represented by ReplicaSet '{targetRevisionReplicaSet.Metadata.Name}' in namespace '{targetRevisionReplicaSet.Metadata.Namespace}'.");
            
            DeploymentV1 rolledBackDeployment = await client.DeploymentsV1().Update(existingDeployment.Metadata.Name, kubeNamespace: existingDeployment.Metadata.Namespace, patchAction: patch =>
            {
                patch.Replace(deployment =>
                    deployment.Spec.Template.Spec,
                    value: targetRevisionReplicaSet.Spec.Template.Spec
                );

                // Since the Rollback API is now obsolete, we have to update the Deployment's revision by hand.
                Dictionary<string, string> annotationsWithModifiedRevision = existingDeployment.Metadata.Annotations;
                annotationsWithModifiedRevision[K8sAnnotations.Deployment.Revision] = targetRevision.Value.ToString();
                patch.Replace(deployment =>
                    deployment.Metadata.Annotations,
                    value: annotationsWithModifiedRevision
                );
            });

            // Re-fetch Deployment state so we pick up annotations added or updated by the controller.
            rolledBackDeployment = await client.DeploymentsV1().Get(rolledBackDeployment.Metadata.Name, rolledBackDeployment.Metadata.Namespace);

            return rolledBackDeployment;
        }

        /// <summary>
        ///     Determine whether a Deployment owns a ReplicaSet.
        /// </summary>
        /// <param name="deployment">
        ///     The Deployment to examine.
        /// </param>
        /// <param name="replicaSet">
        ///     The ReplicaSet to examine.
        /// </param>
        /// <returns>
        ///     <c>true</c>, if the ReplicaSet has an owner-reference to the Deployment; otherwise, <c>false</c>.
        /// </returns>
        static bool DoesDeploymentOwnReplicaSet(DeploymentV1 deployment, ReplicaSetV1 replicaSet)
        {
            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));
            
            if (replicaSet == null)
                throw new ArgumentNullException(nameof(replicaSet));
            
            // Sanity-check: does the target ReplicaSet actually represent a revision of the existing Deployment?
            bool isReplicaSetForDeployment = replicaSet.Metadata.OwnerReferences.Any(ownerReference =>
                ownerReference.Kind == deployment.Kind
                &&
                ownerReference.ApiVersion == deployment.ApiVersion
                &&
                ownerReference.Name == deployment.Metadata.Name
            );
            
            return isReplicaSetForDeployment;
        }

        /// <summary>
        ///     Configure the global application logger.
        /// </summary>
        /// <param name="options">
        ///     Program options.
        /// </param>
        /// <returns>
        ///     The MEL-style logger factory.
        /// </returns>
        static ILoggerFactory ConfigureLogging(ProgramOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.LiterateConsole(
                    outputTemplate: "[{Level:u3}] {Message:l}{NewLine}{Exception}"
                );

            if (options.Verbose)
                loggerConfiguration.MinimumLevel.Verbose();

            Log.Logger = loggerConfiguration.CreateLogger();

            return new LoggerFactory().AddSerilog(Log.Logger);
        }

        /// <summary>
        ///     Global initialisation.
        /// </summary>
        static Program()
        {
            SynchronizationContext.SetSynchronizationContext(
                new SynchronizationContext()
            );
        }

        /// <summary>
        ///     Well-known program exit codes.
        /// </summary>
        public static class ExitCodes
        {
            /// <summary>
            ///     Program completed successfully.
            /// </summary>
            public const int Success = 0;

            /// <summary>
            ///     One or more command-line arguments were missing or invalid.
            /// </summary>
            public const int InvalidArguments = 1;

            /// <summary>
            ///     Resource not found.
            /// </summary>
            public const int NotFound = 2;

            /// <summary>
            ///     Resource already exists.
            /// </summary>
            public const int AlreadyExists = 3;

            /// <summary>
            ///     An unexpected error occurred during program execution.
            /// </summary>
            public const int UnexpectedError = 5;
        }
    }
}
