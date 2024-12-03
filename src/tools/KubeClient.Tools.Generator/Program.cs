﻿using HTTPlease;
using KubeClient.ApiMetadata;
using KubeClient.Extensions.CustomResources.CodeGen;
using KubeClient.Extensions.CustomResources.Schema;
using KubeClient.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using Document = Microsoft.CodeAnalysis.Document;

namespace KubeClient.Tools.Generator
{
    /// <summary>
    ///     The KubeClient code-generator tool.
    /// </summary>
    static class Program
    {
        /// <summary>
        ///     The source for root-level cancellation tokens.
        /// </summary>
        static readonly CancellationTokenSource Cancellation = new CancellationTokenSource();

        /// <summary>
        ///     Standard console-app cancellation behaviour.
        /// </summary>
        static Program()
        {
            Console.CancelKeyPress += OnConsoleCancellation;
        }

        /// <summary>
        ///     The root logger for the code-generator tool.
        /// </summary>
        public static ILogger Log { get; private set; } = null!;

        /// <summary>
        ///     The tool's main program entry point.
        /// </summary>
        /// <param name="commandLineArguments">
        ///     The tool's command-line arguments.
        /// </param>
        /// <returns>
        ///     The program exit code.
        /// </returns>
        static async Task<int> Main(string[] commandLineArguments)
        {
            ProgramOptions? options = ProgramOptions.Parse(commandLineArguments);
            if (options == null)
                return ExitCodes.InvalidArguments;

            using ServiceProvider loggingServiceProvider = ConfigureLogging(options);

            try
            {
                IKubeApiClient kubeApiClient = KubeApiClient.Create(
                    K8sConfig.Load().ToKubeClientOptions(
                        kubeContextName: options.KubeContextName,
                        defaultKubeNamespace: "default",
                        loggerFactory: loggingServiceProvider.GetRequiredService<ILoggerFactory>()
                    )
                );

                CustomResourceDefinitionListV1 crds = await kubeApiClient.CustomResourceDefinitionsV1().List(cancellationToken: Cancellation.Token);
                
                Dictionary<KubeResourceType, CustomResourceDefinitionV1> customResourceTypes = new Dictionary<KubeResourceType, CustomResourceDefinitionV1>();
                foreach (CustomResourceDefinitionV1 crd in crds)
                {
                    foreach (CustomResourceDefinitionVersionV1 crdVersion in crd.Spec.Versions)
                    {
                        KubeResourceType versionedResourceType = new KubeResourceType(
                            Group: crd.Spec.Group,
                            Version: crdVersion.Name,
                            ResourceKind: crd.Spec.Names.Kind
                        );
                        customResourceTypes.Add(versionedResourceType, crd);
                    }
                }

                KubeApiMetadataCache metadataCache = new KubeApiMetadataCache();
                await metadataCache.Load(kubeApiClient, cancellationToken: Cancellation.Token);

                using AdhocWorkspace workspace = new AdhocWorkspace();

                Project project = workspace.AddProject("KubeClient.Generated", LanguageNames.CSharp);

                KubeResourceType targetResourceKind = new KubeResourceType(options.Group, options.Version, options.Kind);
                if (customResourceTypes.TryGetValue(targetResourceKind, out CustomResourceDefinitionV1? kafkaConnectorDefinition))
                {
                    KubeApiMetadata? resourceTypeMetadata = metadataCache.Get(
                        kind: targetResourceKind.ResourceKind,
                        apiGroup: targetResourceKind.Group,
                        apiVersion: targetResourceKind.Version
                    );
                    if (resourceTypeMetadata == null)
                    {
                        Log.LogError("Failed to retrieve metadata for resource type {@ResourceType}.", targetResourceKind);

                        return ExitCodes.UnexpectedError;
                    }

                    KubeSchema schema = JsonSchemaParserV1.BuildKubeSchema(metadataCache, kafkaConnectorDefinition);
                    project = ModelGeneratorV1.GenerateModels(schema, targetResourceKind, project, options.Namespace);
                }

                if (!workspace.TryApplyChanges(project.Solution))
                {
                    Log.LogError("Failed to apply solution changes to workspace.");

                    return ExitCodes.UnexpectedError;
                }

                foreach (Document document in project.Documents.OrderBy(document => document.Name, StringComparer.OrdinalIgnoreCase).Take(1))
                {
                    Document formattedDocument = await Formatter.FormatAsync(document, workspace.Options, cancellationToken: Cancellation.Token);
                    if (formattedDocument.TryGetSyntaxRoot(out SyntaxNode? syntaxRoot))
                    {
                        string generatedCode = syntaxRoot.ToFullString();
                        Log.LogInformation("\n{DocumentSourceText:l}", generatedCode);

                        await File.WriteAllTextAsync(options.OutputFile, generatedCode);
                    }
                    else
                    {
                        Log.LogError("Failed to retrieve source text for document {DocumentName}.", document.Name);

                        return ExitCodes.UnexpectedError;
                    }
                }

                return ExitCodes.Success;
            }
            catch (HttpRequestException<StatusV1> kubeError)
            {
                Log.LogError(kubeError, "Kubernetes API error: {@Status}", kubeError.Response);

                return ExitCodes.UnexpectedError;
            }
            catch (Exception unexpectedError)
            {
                Log.LogError(unexpectedError, "An unexpected error has occurred.");

                return ExitCodes.UnexpectedError;
            }
            finally
            {
                using (Cancellation)
                {
                    Cancellation.Cancel();
                }
            }
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
            ///     An unexpected error occurred during program execution.
            /// </summary>
            public const int UnexpectedError = 5;
        }

        /// <summary>
        ///     Configure application-level logging and populate <see cref="Log"/>.
        /// </summary>
        /// <param name="options">
        ///     Program options.
        /// </param>
        /// <returns>
        ///     The global logging service provider.
        /// </returns>
        static ServiceProvider ConfigureLogging(ProgramOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            ServiceProvider loggingServiceProvider = new ServiceCollection()
                .AddLogging(logging =>
                {
                    logging.SetMinimumLevel(
                        options.Verbose ? LogLevel.Trace : LogLevel.Information
                    );
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .BuildServiceProvider(new ServiceProviderOptions
                {
                    ValidateOnBuild = true,
                    ValidateScopes = true,
                });

            try
            {
                ILoggerFactory loggerFactory = loggingServiceProvider.GetRequiredService<ILoggerFactory>();

                Log = loggerFactory.CreateLogger(typeof(Program));

                return loggingServiceProvider;
            }
            catch (Exception)
            {
                // Clean up, on failure (if possible).
                using (loggingServiceProvider)
                {
                    throw;
                }
            }
        }

        static IHostBuilder BuildToolHost(ProgramOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    if (options.Verbose)
                        logging.SetMinimumLevel(LogLevel.Debug);
                    else
                        logging.SetMinimumLevel(LogLevel.Information);

                    logging.AddDebug();
                    logging.AddSimpleConsole(console =>
                    {
                        if (Console.IsOutputRedirected || Console.IsErrorRedirected)
                            console.ColorBehavior = LoggerColorBehavior.Disabled;
                        else
                            console.ColorBehavior = LoggerColorBehavior.Enabled;
                    });
                })
                .UseConsoleLifetime(consoleLifetime =>
                {
                    consoleLifetime.SuppressStatusMessages = !options.Verbose;
                });
        }

        static void OnConsoleCancellation(object? sender, ConsoleCancelEventArgs args)
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));

            if (args == null)
                throw new ArgumentNullException(nameof(args));

            Cancellation.Cancel();

            args.Cancel = true; // We'll handle shutdown.
        }
    }
}
