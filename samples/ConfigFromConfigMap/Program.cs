using HTTPlease;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Samples.ConfigFromConfigMap
{
    using Extensions.Configuration;
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
                const string configMap1Name = "config-from-configmap-1";
                const string configMap2Name = "config-from-configmap-2";
                const string configMapNamespace = "default";

                KubeClientOptions clientOptions = K8sConfig.Load().ToKubeClientOptions(defaultKubeNamespace: configMapNamespace, loggerFactory: loggerFactory);

                if (options.Verbose)
                    clientOptions.LogPayloads = true;

                KubeApiClient client = KubeApiClient.Create(clientOptions);

                Log.Information("Checking for existing ConfigMaps...");

                ConfigMapV1 configMap1 = await client.ConfigMapsV1().Get(configMap1Name, configMapNamespace);
                if (configMap1 != null)
                {
                    Log.Information("Deleting existing ConfigMap {ConfigMapName}...", configMap1Name);
                    await client.ConfigMapsV1().Delete(configMap1Name);
                    Log.Information("Deleted existing ConfigMap {ConfigMapName}.", configMap1Name);
                }

                ConfigMapV1 configMap2 = await client.ConfigMapsV1().Get(configMap2Name, configMapNamespace);
                if ( configMap2 != null )
                {
                    Log.Information("Deleting existing ConfigMap {ConfigMapName}...", configMap2Name);
                    await client.ConfigMapsV1().Delete(configMap2Name);
                    Log.Information("Deleted existing ConfigMap {ConfigMapName}.", configMap2Name);
                }

                Log.Information("Creating ConfigMaps...");

                Log.Information("Creating ConfigMap {ConfigMapName}...", configMap1Name);
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

                Log.Information("Creating ConfigMap {ConfigMapName}...", configMap2Name);
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

                Log.Information("ConfigMaps created.");

                Log.Information("Building configuration...");
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
                Log.Information("Configuration built.");

                Log.Information("Got configuration:");
                Dump(configuration);

                Log.Information("Press enter to update ConfigMaps {ConfigMap1Name} and {ConfigMap2Name}:", configMap1Name, configMap2Name);

                Console.ReadLine();

                Log.Information("Registering callback for change-notifications...");

                ManualResetEvent configurationChanged = new ManualResetEvent(false);

                // See ConfigurationExtensions class below.
                IDisposable reloadNotifications = configuration.OnReload(() =>
                {
                    Log.Information("Got changed configuration:");
                    Dump(configuration);

                    configurationChanged.Set();
                });
                Log.Information("Change-notification callback registered.");

                using (configurationChanged)
                using (reloadNotifications)
                {
                    Log.Information("Updating ConfigMap {ConfigMapName}...", configMap1Name);

                    configMap1.Data["key5"] = "FiveA";
                    configMap1.Data["key6"] = "SixA";

                    // Replace the entire Data dictionary (to modify only some of the data, you'll need to use an untyped JsonPatchDocument).
                    await client.ConfigMapsV1().Update(configMap1Name, patch =>
                    {
                        patch.Replace(patchConfigMap => patchConfigMap.Data,
                            value: configMap1.Data
                        );
                    });

                    Log.Information("Updated ConfigMap {ConfigMapName}.", configMap1Name);

                    Log.Information("Waiting for configuration change...");

                    configurationChanged.WaitOne();

                    Log.Information("Configuration changed via ConfigMap {ConfigMapName}.", configMap1Name);

                    configurationChanged.Reset();

                    Log.Information("Updating ConfigMap {ConfigMapName}...", configMap2Name);

                    configMap2.Data["key5"] = "FiveB";
                    configMap2.Data["key6"] = "SixB";

                    // Replace the entire Data dictionary (to modify only some of the data, you'll need to use an untyped JsonPatchDocument).
                    await client.ConfigMapsV1().Update(configMap2Name, patch =>
                    {
                        patch.Replace(patchConfigMap => patchConfigMap.Data,
                            value: configMap2.Data
                        );
                    });

                    Log.Information("Updated ConfigMap {ConfigMapName}.", configMap2Name);

                    configurationChanged.WaitOne();

                    Log.Information("Configuration changed via ConfigMap {ConfigMapName}.", configMap2Name);
                }

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
        ///     Dump configuration keys and values to the log.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
        static void Dump(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            foreach ((string key, string value) in configuration.AsEnumerable().OrderBy(item => item.Key))
                Log.Information("\t'{Key}' = '{Value}'", key, value);
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

            return new LoggerFactory().AddSerilog();
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
