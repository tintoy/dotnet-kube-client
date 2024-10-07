using HTTPlease;
using KubeClient.Extensions.CustomResources;
using KubeClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace KubeClient.Tools.Generator
{
    static partial class Program
    {
        static readonly CancellationTokenSource Cancellation = new CancellationTokenSource();

        static Program()
        {
            Console.CancelKeyPress += OnConsoleCancellation;
        }

        public static ILogger? Log { get; private set; }

        static async Task<int> Main(string[] commandLineArguments)
        {
            ProgramOptions options = ProgramOptions.Parse(commandLineArguments);
            if (options == null)
                return ExitCodes.InvalidArguments;

            using ServiceProvider loggingServiceProvider = ConfigureLogging(options);

            try
            {
                // TODO: Configure from ProgramOptions.

                IKubeApiClient kubeApiClient = KubeApiClient.Create(
                    K8sConfig.Load().ToKubeClientOptions()
                );

                CustomResourceDefinitionV1? crd = await kubeApiClient.CustomResourceDefinitionsV1().Get("kafka.strimzi.io.v1beta2.KafkaConnector", Cancellation.Token);
                if (crd == null)
                    throw new Exception("Cannot find CRD for KafkaConnector (v1beta2).");

                // TODO: Generate.

                return ExitCodes.Success;
            }
            catch (HttpRequestException<StatusV1> kubeError)
            {
                Log?.LogError(kubeError, "Kubernetes API error: {@Status}", kubeError.Response);

                return ExitCodes.UnexpectedError;
            }
            catch (Exception unexpectedError)
            {
                Log?.LogError(unexpectedError, "An unexpected error has occurred.");

                return ExitCodes.UnexpectedError;
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
