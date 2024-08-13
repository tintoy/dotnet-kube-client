using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace KubeClient.TestCommon.Tests
{
    using Mocks;
    using Models;
    using TestCommon;

    /// <summary>
    ///     Basic tests for <see cref="MockKubeApi"/> functionality.
    /// </summary>
    public class MockKubeApiTests
        : TestBase
    {
        public MockKubeApiTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        [Fact]
        public async Task Can_Watch_PodV1()
        {
            const string podWatchPath = "api/v1/namespaces/default/pods/my-pod";

            Subject<PodV1> watchSource = new Subject<PodV1>();

            List<PodV1> observedPodStates = new List<PodV1>();

            MockKubeApi kubeApi = MockKubeApi.Create(TestOutput, api =>
            {
                api.HandleResourceWatch(podWatchPath, watchSource);
            });

            await using (kubeApi)
            {
                using CancellationTokenSource cancellationSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                using HttpClient apiHttpClient = kubeApi.CreateClient();

                Log.LogInformation("Initiating pod watch request...");

                using Stream watchResponseBody = await apiHttpClient.GetStreamAsync(podWatchPath, cancellationSource.Token);

                Log.LogInformation("Response-body stream is now available; starting to stream pod state...");

                ILogger streamerLogger = LoggerFactory.CreateLogger(nameof(StreamPodState));
                Task watchStreamer = StreamPodState(watchResponseBody, observedPodStates, streamerLogger, cancellationSource.Token);

                watchSource.OnNext(new PodV1
                {
                    Metadata = new ObjectMetaV1
                    {
                        Name = "my-pod",
                        Namespace = "default"
                    },
                    Spec = new PodSpecV1
                    {

                    },
                    Status = new PodStatusV1
                    {
                        Message = "Message 1"
                    },
                });
                watchSource.OnNext(new PodV1
                {
                    Metadata = new ObjectMetaV1
                    {
                        Name = "my-pod",
                        Namespace = "default"
                    },
                    Spec = new PodSpecV1
                    {

                    },
                    Status = new PodStatusV1
                    {
                        Message = "Message 2"
                    },
                });

                watchSource.OnCompleted();

                await watchStreamer;
            }

            Assert.Contains(observedPodStates, pod => pod?.Status?.Message == "Message 1");
            Assert.Contains(observedPodStates, pod => pod?.Status?.Message == "Message 2");
            Assert.Equal(2, observedPodStates.Count);
        }

        async Task StreamPodState<TResource>(Stream watchStream, List<TResource> observedResourceStates, ILogger logger, CancellationToken cancellationToken)
            where TResource : KubeResourceV1
        {
            if (watchStream == null)
                throw new ArgumentNullException(nameof(watchStream));

            if (observedResourceStates == null)
                throw new ArgumentNullException(nameof(observedResourceStates));

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            (string resourceKind, string resourceApiVersion) = KubeObjectV1.GetKubeKind<TResource>();

            using (StreamReader watchReader = new StreamReader(watchStream))
            {
                string rawStateData;

                logger.LogInformation("Waiting for the first line of text from the state stream...");

                while ((rawStateData = await watchReader.ReadLineAsync(cancellationToken)) != null)
                {
                    logger.LogInformation("Got a line of text from the state stream: {RawStateData}", rawStateData);

                    TResource resourceState = JsonConvert.DeserializeObject<TResource>(rawStateData);
                    observedResourceStates.Add(resourceState);

                    logger.LogInformation("Successfully observed state #{StateCount} for {ResourceKind}{ResourceApiVersion} {ResourceName}.",
                        observedResourceStates.Count,
                        resourceKind,
                        resourceApiVersion,
                        resourceState.Metadata?.Name
                    );
                }
            }
        }
    }
}
