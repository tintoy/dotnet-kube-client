using HTTPlease;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace KubeClient.Samples.WatchEvents
{
    using Models;
    using ResourceClients;

    /// <summary>
    ///     A sample program that demonstrates creating how to watch events in a K8s namespace and resolve the resources defined in those events.
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
                KubeClientOptions clientOptions = K8sConfig.Load().ToKubeClientOptions(defaultKubeNamespace: options.KubeNamespace, loggerFactory: loggerFactory);
                if (options.Verbose)
                    clientOptions.LogPayloads = true;

                JsonSerializerSettings serializerSettings = KubeResourceClient.SerializerSettings;
                serializerSettings.Formatting = Formatting.Indented;

                KubeApiClient client = KubeApiClient.Create(clientOptions);
                EventListV1 initialEvents = await client.EventsV1().List();

                ActionBlock<EventV1> eventProcessor = CreateEventProcessor(client);

                Log.LogInformation("Initial events:");
                Log.LogInformation("===============");

                if (initialEvents.Items.Count > 0)
                {
                    foreach (EventV1 initialEvent in initialEvents)
                        eventProcessor.Post(initialEvent);
                }
                else
                    Log.LogInformation("No initial events.");

                Log.LogInformation("===============");

                IObservable<IResourceEventV1<EventV1>> eventStream;
                if (initialEvents.Items.Count > 0)
                {
                    EventV1 lastEvent = initialEvents.Items[initialEvents.Items.Count - 1];

                    eventStream = client.EventsV1().WatchAll(resourceVersion: lastEvent.InvolvedObject.ResourceVersion);
                }
                else
                    eventStream = client.EventsV1().WatchAll();
                
                IDisposable subscription = eventStream.Select(resourceEvent => resourceEvent.Resource).Subscribe(
                    subsequentEvent => eventProcessor.Post(subsequentEvent),
                    error => Log.LogError(error, "Unexpected error while streaming events.")
                );

                using (subscription)
                {
                    Log.LogInformation("Watching for new events (press enter to terminate).");

                    Console.ReadLine();
                }

                Log.LogInformation("Waiting for event processor to shut down...");

                eventProcessor.Complete();
                await eventProcessor.Completion;

                Log.LogInformation("Event processor has shut down.");

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
        /// Create a TPL dataflow block to process events.
        /// </summary>
        /// <param name="client">The Kubernetes API client.</param>
        /// <returns>The configured <see cref="ActionBlock{TInput}"/>.</returns>
        static ActionBlock<EventV1> CreateEventProcessor(IKubeApiClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            ActionBlock<EventV1> eventProcessor = new ActionBlock<EventV1>(async eventToProcess =>
            {
                Log.LogInformation("Event: [{SourceComponent}] {EventReason:l} {EventMessage}",
                    eventToProcess.Source?.Component,
                    eventToProcess.Reason,
                    eventToProcess.Message
                );

                await ResolveRelatedResource(client, eventToProcess);
            }); 

            eventProcessor.Completion.ContinueWith(faulted =>
            {
                AggregateException flattened = faulted.Exception.Flatten();
                if (flattened.InnerExceptions.Count == 1)
                    Log.LogError(flattened.InnerExceptions[0], "Unexpected error while processing event.");
                else
                    Log.LogError(flattened, "Unexpected error while processing event.");
            }, TaskContinuationOptions.OnlyOnFaulted);

            return eventProcessor;
        }

        /// <summary>
        /// Attempt to resolve the related resource for the specified event.
        /// </summary>
        /// <param name="client">The Kubernetes API client.</param>
        /// <param name="eventResource">The <see cref="EventV1"/> to process.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        static async Task ResolveRelatedResource(IKubeApiClient client, EventV1 eventResource)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (eventResource == null)
                throw new ArgumentNullException(nameof(eventResource));

            KubeResourceV1 involvedResource = await client.Dynamic().Get(eventResource.InvolvedObject);
            if (involvedResource != null)
            {
                Log.LogInformation("\tResolved related {ResourceModelName} resource for event {EventName} ({EventNamespace}).",
                    involvedResource.GetType().Name,
                    eventResource.Metadata.Name,
                    eventResource.Metadata.Namespace
                );
            }
            else
            {
                Log.LogInformation("\tFailed to resolve related resource for event {EventName} ({EventNamespace}).",
                    eventResource.Metadata.Name,
                    eventResource.Metadata.Namespace
                );
            }
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
