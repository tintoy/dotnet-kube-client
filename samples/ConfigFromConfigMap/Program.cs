using HTTPlease;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Samples.ConfigFromConfigMap
{
    using Extensions.Configuration;
    using Extensions.KubeConfig.Models;
    using Models;

    /// <summary>
    ///     The ConfigFromConfigMap program.
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
                const string configMapName = "config-from-configmap";
                const string configMapNamespace = "default";

                KubeClientOptions clientOptions = K8sConfig.Load().ToKubeClientOptions(defaultKubeNamespace: configMapNamespace);
                if (options.Verbose)
                    clientOptions.LogPayloads = true;

                KubeApiClient client = KubeApiClient.Create(clientOptions, loggerFactory);

                Log.Information("Checking for existing ConfigMap...");
                ConfigMapV1 configMap = await client.ConfigMapsV1().Get(configMapName, configMapNamespace);
                if (configMap != null)
                {
                    Log.Information("Deleting existing ConfigMap...");
                    await client.ConfigMapsV1().Delete(configMapName);
                    Log.Information("Existing ConfigMap deleted.");
                }

                Log.Information("Creating new ConfigMap...");
                configMap = await client.ConfigMapsV1().Create(new ConfigMapV1
                {
                    Metadata = new ObjectMetaV1
                    {
                        Name = configMapName,
                        Namespace = configMapNamespace
                    },
                    Data =
                    {
                        ["Key1"] = "One",
                        ["Key2"] = "Two"
                    }
                });
                Log.Information("New ConfigMap created.");

                Log.Information("Building configuration...");
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddKubeConfigMap(clientOptions,
                        configMapName: "config-from-configmap",
                        reloadOnChange: true
                    )
                    .Build();
                Log.Information("Configuration built.");

                configuration.GetReloadToken().RegisterChangeCallback(_ =>
                {
                    Log.Information("Got changed configuration:");
                    foreach (var item in configuration.AsEnumerable())
                        Log.Information("\t'{Key}' = '{Value}'", item.Key, item.Value);
                }, state: null);

                Log.Information("Got configuration:");
                foreach (var item in configuration.AsEnumerable())
                    Log.Information("\t'{Key}' = '{Value}'", item.Key, item.Value);

                Log.Information("Press enter to update ConfigMap...");

                Console.ReadLine();

                Log.Information("Updating ConfigMap...");

                configMap.Data["One"] = "1";
                configMap.Data["Two"] = "2";

                // Replace the entire Data dictionary (otherwise, use an untyped JsonPatchDocument).
                await client.ConfigMapsV1().Update(configMapName, patch =>
                {
                    patch.Replace(patchConfigMap => patchConfigMap.Data,
                        value: configMap.Data
                    );
                });

                Log.Information("Updated ConfigMap.");

                Log.Information("Waiting for configuration change; press enter to terminate.");

                Console.ReadLine();

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
            ///     An unexpected error occurred during program execution.
            /// </summary>
            public const int UnexpectedError = 5;
        }
    }
}
