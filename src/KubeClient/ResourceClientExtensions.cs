using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient
{
    using Models;
    using ResourceClients;

    /// <summary>
    ///     Extension methods for various K8s resource clients.
    /// </summary>
    public static class ResourceClientExtensions
    {
        /// <summary>
        ///     Update (PATCH) an existing Deployment.
        /// </summary>
        /// <param name="deploymentClient">
        ///     The Kubernetes Deployment (v1) API client.
        /// </param>
        /// <param name="existingDeployment">
        ///     A <see cref="DeploymentV1"/> representing the Deployment to update.
        /// </param>
        /// <param name="patchAction">
        ///     A delegate that customises the patch operation.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the Deployment's current state.
        /// </returns>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <remarks>
        ///     The state of <paramref name="existingDeployment"/> is not used for update; it is simply convenient shorthand for specifying the deployment's name and namespace.
        /// </remarks>
        public static async Task<DeploymentV1> UpdateDeployment(this IDeploymentClientV1 deploymentClient, DeploymentV1 existingDeployment, Action<JsonPatchDocument<DeploymentV1>> patchAction, CancellationToken cancellationToken = default)
        {
            if (deploymentClient == null)
                throw new ArgumentNullException(nameof(deploymentClient));

            if (existingDeployment == null)
                throw new ArgumentNullException(nameof(existingDeployment));
            
            DeploymentV1 updatedDeployment = await deploymentClient.Update(existingDeployment.Metadata.Name, patchAction, existingDeployment.Metadata.Namespace, cancellationToken);

            // Re-fetch Deployment state so we pick up annotations added or updated by the controller.
            updatedDeployment = await deploymentClient.Get(updatedDeployment.Metadata.Name, updatedDeployment.Metadata.Namespace, cancellationToken);

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
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicaSetV1"/> representing the ReplicaSet's current state; <c>null</c>, if no corresponding ReplicaSet was found.
        /// </returns>
        public static async Task<ReplicaSetV1> FindReplicaSetForRevision(IKubeApiClient client, DeploymentV1 deployment, int targetRevision, CancellationToken cancellationToken = default)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (deployment == null)
                throw new ArgumentNullException(nameof(deployment));

            string matchLabelSelector = deployment.GetLabelSelector();
            
            ReplicaSetListV1 replicaSets = await client.ReplicaSetsV1().List(matchLabelSelector, deployment.Metadata.Namespace, cancellationToken);
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
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DeploymentV1"/> representing the Deployment's current state.
        /// </returns>
        public static async Task<DeploymentV1> RollbackDeployment(IKubeApiClient client, DeploymentV1 existingDeployment, ReplicaSetV1 targetRevisionReplicaSet, CancellationToken cancellationToken = default)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            if (existingDeployment == null)
                throw new ArgumentNullException(nameof(existingDeployment));
            
            if (targetRevisionReplicaSet == null)
                throw new ArgumentNullException(nameof(targetRevisionReplicaSet));

            int? targetRevision = targetRevisionReplicaSet.GetRevision();
            if (targetRevision == null)
                throw new InvalidOperationException($"Cannot determine Deployment revision represented by ReplicaSet '{targetRevisionReplicaSet.Metadata.Name}' in namespace '{targetRevisionReplicaSet.Metadata.Namespace}'.");
            
            DeploymentV1 rolledBackDeployment = await client.DeploymentsV1().Update(existingDeployment.Metadata.Name, kubeNamespace: existingDeployment.Metadata.Namespace, cancellationToken: cancellationToken, patchAction: patch =>
            {
                // Restore Deployment's Pod-template specification to the one used by the target ReplicaSet.
                patch.Replace(deployment =>
                    deployment.Spec.Template.Spec,
                    value: targetRevisionReplicaSet.Spec.Template.Spec
                );

                // Since the old Rollback API is obsolete (as of v1beta2), we have to update the Deployment's revision by hand.
                patch.Replace(deployment =>
                    deployment.Metadata.Annotations, // Due to JSON-PATCH limitations in the K8s API, we have to replace the entire Annotations property, not attempt to update individual items within the dictionary.
                    value: new Dictionary<string, string>(existingDeployment.Metadata.Annotations)
                    {
                        [K8sAnnotations.Deployment.Revision] = targetRevision.Value.ToString()
                    }
                );
            });

            // Re-fetch Deployment state so we pick up annotations added or updated by the controller.
            rolledBackDeployment = await client.DeploymentsV1().Get(rolledBackDeployment.Metadata.Name, rolledBackDeployment.Metadata.Namespace, cancellationToken);

            return rolledBackDeployment;
        }
    }
}