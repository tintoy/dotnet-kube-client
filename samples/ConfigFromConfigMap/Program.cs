using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Samples.ConfigFromConfigMap
{
    using Extensions.Configuration;
    using Http;
    using Models;

    /// <summary>
    ///     The ConfigFromConfigMap program.
    /// </summary>
    static class Program
    {
        /// <summary>
        ///     The main program's logger.
        /// </summary>
        static ILogger Log { get; set; }

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

            using ServiceProvider loggingServiceProvider = ConfigureLogging(options);
            ILoggerFactory loggerFactory = loggingServiceProvider.GetRequiredService<ILoggerFactory>();

            try
            {
                const string configMap1Name = "config-from-configmap-1";
                const string configMap2Name = "config-from-configmap-2";
                const string configMapNamespace = "default";

                KubeClientOptions clientOptions = K8sConfig.Load().ToKubeClientOptions(defaultKubeNamespace: configMapNamespace, loggerFactory: loggerFactory);

                if (options.Verbose)
                    clientOptions.LogPayloads = true;

                KubeApiClient client = KubeApiClient.Create(clientOptions);

                Log.LogInformation("Checking for existing ConfigMaps...");

                ConfigMapV1 configMap1 = await client.ConfigMapsV1().Get(configMap1Name, configMapNamespace);
                if (configMap1 != null)
                {
                    Log.LogInformation("Deleting existing ConfigMap {ConfigMapName}...", configMap1Name);
                    await client.ConfigMapsV1().Delete(configMap1Name);
                    Log.LogInformation("Deleted existing ConfigMap {ConfigMapName}.", configMap1Name);
                }

                ConfigMapV1 configMap2 = await client.ConfigMapsV1().Get(configMap2Name, configMapNamespace);
                if ( configMap2 != null )
                {
                    Log.LogInformation("Deleting existing ConfigMap {ConfigMapName}...", configMap2Name);
                    await client.ConfigMapsV1().Delete(configMap2Name);
                    Log.LogInformation("Deleted existing ConfigMap {ConfigMapName}.", configMap2Name);
                }

                Log.LogInformation("Creating ConfigMaps...");

                Log.LogInformation("Creating ConfigMap {ConfigMapName}...", configMap1Name);
                configMap1 = await client.ConfigMapsV1().Create(new ConfigMapV1
                {
                    Metadata = new ObjectMetaV1
                    {
                        Name = configMap1Name,
                        Namespace = configMapNamespace
                    },
                    Data =
                    {
                        ["Key1"] = "OneA",
                        ["Key2"] = "TwoA",
                        ["Key3"] = "ThreeA"
                    }
                });

                Log.LogInformation("Creating ConfigMap {ConfigMapName}...", configMap2Name);
                configMap2 = await client.ConfigMapsV1().Create(new ConfigMapV1
                {
                    Metadata = new ObjectMetaV1
                    {
                        Name = configMap2Name,
                        Namespace = configMapNamespace
                    },
                    Data =
                    {
                        ["Key1"] = "OneB",
                        ["Key2"] = "TwoB",
                        ["Key4"] = "FourB"
                    }
                });

                Log.LogInformation("ConfigMaps created.");

                Log.LogInformation("Building configuration...");
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddKubeConfigMap(clientOptions,
                        configMapName: configMap1Name,
                        reloadOnChange: true
                    )
                    .AddKubeConfigMap(clientOptions,
                        configMapName: configMap2Name,
                        reloadOnChange: true
                    )
                    .Build();
                Log.LogInformation("Configuration built.");

                Log.LogInformation("Got configuration:");
                Dump(configuration);

                Log.LogInformation("Press enter to update ConfigMaps {ConfigMap1Name} and {ConfigMap2Name}:", configMap1Name, configMap2Name);

                Console.ReadLine();

                Log.LogInformation("Registering callback for change-notifications...");

                ManualResetEvent configurationChanged = new ManualResetEvent(false);

                // See ConfigurationExtensions class below.
                IDisposable reloadNotifications = configuration.OnReload(() =>
                {
                    Log.LogInformation("Got changed configuration:");
                    Dump(configuration);

                    configurationChanged.Set();
                });
                Log.LogInformation("Change-notification callback registered.");

                using (configurationChanged)
                using (reloadNotifications)
                {
                    Log.LogInformation("Updating ConfigMap {ConfigMapName}...", configMap1Name);

                    configMap1.Data["key5"] = "FiveA";
                    configMap1.Data["key6"] = "SixA";

                    // Replace the entire Data dictionary (to modify only some of the data, you'll need to use an untyped JsonPatchDocument).
                    await client.ConfigMapsV1().Update(configMap1Name, patch =>
                    {
                        patch.Replace(patchConfigMap => patchConfigMap.Data,
                            value: configMap1.Data
                        );
                    });

                    Log.LogInformation("Updated ConfigMap {ConfigMapName}.", configMap1Name);

                    Log.LogInformation("Waiting for configuration change...");

                    configurationChanged.WaitOne();

                    Log.LogInformation("Configuration changed via ConfigMap {ConfigMapName}.", configMap1Name);

                    configurationChanged.Reset();

                    Log.LogInformation("Updating ConfigMap {ConfigMapName}...", configMap2Name);

                    configMap2.Data["key5"] = "FiveB";
                    configMap2.Data["key6"] = "SixB";

                    // Replace the entire Data dictionary (to modify only some of the data, you'll need to use an untyped JsonPatchDocument).
                    await client.ConfigMapsV1().Update(configMap2Name, patch =>
                    {
                        patch.Replace(patchConfigMap => patchConfigMap.Data,
                            value: configMap2.Data
                        );
                    });

                    Log.LogInformation("Updated ConfigMap {ConfigMapName}.", configMap2Name);

                    configurationChanged.WaitOne();

                    Log.LogInformation("Configuration changed via ConfigMap {ConfigMapName}.", configMap2Name);
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
                Log.LogError(unexpectedError, "Unexpected error.");

                return ExitCodes.UnexpectedError;
            }
        }

        /// <summary>
        ///     Dump configuration keys and values to the log.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
        static void Dump(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            foreach ((string key, string value) in configuration.AsEnumerable().OrderBy(item => item.Key))
                Log.LogInformation("\t'{Key}' = '{Value}'", key, value);
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

    /// <summary>
    ///     Extension methods for working with <see cref="IConfiguration"/>.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        ///     Register a callback for configuration reload notifications.
        /// </summary>
        /// <param name="configuration">
        ///     The configuration.
        /// </param>
        /// <param name="action">
        ///     The <see cref="Action"/> delegate to invoke when the configuration is reloaded.
        /// </param>
        /// <returns>
        ///     An <see cref="IDisposable"/> which, when disposed, terminates the notifications.
        /// </returns>
        public static IDisposable OnReload(this IConfiguration configuration, Action action)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            // ChangeToken.OnChange takes care of requesting a new change token each time the callback is invoked.

            return ChangeToken.OnChange(configuration.GetReloadToken, action);
        }

        /// <summary>
        /// Register a callback for configuration reload notifications.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="action">The <see cref="Action{T1}"/> delegate to invoke when the configuration is reloaded.</param>
        /// <returns>An <see cref="IDisposable"/> which, when disposed, terminates the notifications.</returns>
        public static IDisposable OnReload(this IConfiguration configuration, Action<IConfiguration> action)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            // ChangeToken.OnChange takes care of requesting a new change token each time the callback is invoked.

            return ChangeToken.OnChange(configuration.GetReloadToken, action, configuration);
        }
    }
}
