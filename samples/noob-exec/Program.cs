﻿using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Samples.NoobExec
{
    using Extensions.KubeConfig.Models;
    using Extensions.WebSockets;
    using Models;

    /// <summary>
    ///     The NoobExec program.
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
            // Show help if no arguments are specified.
            bool showHelp = commandLineArguments.Length == 0;
            if (showHelp)
                commandLineArguments = new[] { "--help" };

            ProgramOptions options = ProgramOptions.Parse(commandLineArguments);
            if (options == null)
                return showHelp ? ExitCodes.Success : ExitCodes.InvalidArguments;

            ILoggerFactory loggers = ConfigureLogging(options);

            try
            {
                KubeClientOptions clientOptions = Config.Load().ToKubeClientOptions(
                    kubeContextName: options.KubeContext
                );

                using (KubeApiClient client = KubeApiClient.Create(clientOptions, loggers))
                {
                    PodV1 targetPod = await client.PodsV1().Get(options.PodName,
                        kubeNamespace: options.KubeNamespace
                    );
                    if (targetPod == null)
                    {
                        Log.LogError("Pod '{PodName}' not found in namespace '{KubeNamespace}' on cluster ({KubeContextName}).",
                            options.PodName,
                            options.KubeNamespace,
                            options.KubeContext
                        );

                        return ExitCodes.NotFound;
                    }

                    if (!String.IsNullOrWhiteSpace(options.ContainerName))
                    {
                        ContainerStatusV1 targetContainer = targetPod.Status.ContainerStatuses.Find(
                            container => container.Name == options.ContainerName
                        );
                        if (targetContainer == null)
                        {
                            Log.LogError("Container '{ContainerName}' not found in Pod '{PodName}' in namespace '{KubeNamespace}' on cluster ({KubeContextName}).",
                                options.ContainerName,
                                options.PodName,
                                options.KubeNamespace,
                                options.KubeContext
                            );

                            return ExitCodes.NotFound;
                        }
                    }
                    else if (targetPod.Status.ContainerStatuses.Count > 1)
                    {
                        Log.LogError("Pod '{PodName}' in namespace '{KubeNamespace}' on cluster ({KubeContextName}) has more than one container. Please specify the name of the target container",
                            options.PodName,
                            options.KubeNamespace,
                            options.KubeContext
                        );

                        return ExitCodes.InvalidArguments;
                    }

                    K8sMultiplexer multiplexer = await client.PodsV1().ExecAndConnect(
                        podName: options.PodName,
                        container: options.ContainerName,
                        command: options.Command,
                        kubeNamespace: options.KubeContext,
                        stdin: true,
                        stdout: true,
                        stderr: true,
                        tty: options.TTY
                    );

                    Log.LogInformation("Connected; if you're running a shell, you may need to press Enter to see a prompt.");

                    using (multiplexer)
                    using (CancellationTokenSource cancellationSource = new CancellationTokenSource())
                    using (Stream localStdIn = Console.OpenStandardInput())
                    using (Stream remoteStdIn = multiplexer.GetStdIn())
                    using (Stream localStdOut = Console.OpenStandardOutput())
                    using (Stream remoteStdOut = multiplexer.GetStdOut())
                    using (Stream localStdErr = Console.OpenStandardError())
                    using (Stream remoteStdErr = multiplexer.GetStdErr())
                    {
                        Task copyStdIn = localStdIn.CopyToAsync(remoteStdIn, cancellationSource.Token);
                        Task copyStdOut = remoteStdOut.CopyToAsync(localStdOut, cancellationSource.Token);
                        Task copyStdErr = remoteStdErr.CopyToAsync(localStdErr, cancellationSource.Token);
                        
                        try
                        {
                            await Task.WhenAll(copyStdIn, copyStdOut, copyStdErr);
                        }
                        catch (OperationCanceledException cancelled) when (cancelled.CancellationToken == cancellationSource.Token)
                        {
                            // Clean termination.
                        }
                    }
                }

                return ExitCodes.Success;
            }
            catch (Exception unexpectedError)
            {
                Log.LogError(unexpectedError, "Unexpected error.");

                return ExitCodes.UnexpectedError;
            }
        }

        /// <summary>
        ///     Configure application-level logging.
        /// </summary>
        /// <param name="options">
        ///     Program options.
        /// </param>
        /// <returns>
        ///     The global logger factory.
        /// </returns>
        static ILoggerFactory ConfigureLogging(ProgramOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole(
                minLevel: options.Verbose ? LogLevel.Trace : LogLevel.Information
            );

            Log = loggerFactory.CreateLogger("Program");

            return loggerFactory;
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
            ///     The target resource was not found.
            /// </summary>
            public const int NotFound = 10;

            /// <summary>
            ///     An unexpected error occurred during program execution.
            /// </summary>
            public const int UnexpectedError = 50;
        }
    }
}
