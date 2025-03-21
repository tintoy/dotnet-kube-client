using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KubeClient.Tests
{
    using KubeClient.Http;
    using KubeClient.Http.Clients;
    using KubeClient.Http.Diagnostics;
    using KubeClient.Http.Formatters;
    using KubeClient.Http.Testability;
    using KubeClient.Http.Testability.Xunit;
    using Logging;
    using Models;
    using Utilities;

    /// <summary>
    ///     Tests for <see cref="KubeApiClient"/> logging.
    /// </summary>
    public class LoggingTests
    {
        /// <summary>
        ///     Create a new <see cref="KubeApiClient"/> logging test suite.
        /// </summary>
        public LoggingTests()
        {
        }

        /// <summary>
        ///     Verify that the client's logger emits BeginRequest / EndRequest log entries for PodV1 get-by-name.
        /// </summary>
        [Fact(DisplayName = "Emit BeginRequest / EndRequest log entries for PodV1 get-by-name (NotFound)")]
        public async Task PodsV1_GetByName_NotFound()
        {
            var logEntries = new List<LogEntry>();

			TestLogger logger = new TestLogger(LogLevel.Information);
			logger.LogEntries.Subscribe(
				logEntry => logEntries.Add(logEntry)
			);

			ClientBuilder clientBuilder = new ClientBuilder()
				.WithLogging(logger);

			HttpClient httpClient = clientBuilder.CreateClient("http://localhost:1234", TestHandlers.RespondWith(request =>
            {
                return request.CreateResponse(HttpStatusCode.NotFound,
                    responseBody: JsonConvert.SerializeObject(new StatusV1
                    {
                        Status = "Failure",
                        Reason = "NotFound"
                    }),
                    WellKnownMediaTypes.Json
                );
            }));

            KubeClientOptions clientOptions = new KubeClientOptions("http://localhost:1234");
            using (KubeApiClient kubeClient = KubeApiClient.CreateTestClient(httpClient, clientOptions))
            {
                PodV1 pod = await kubeClient.PodsV1().Get(name: "foo");
                Assert.Null(pod);
            }

            Assert.Equal(2, logEntries.Count);

			LogEntry logEntry1 = logEntries[0];
			Assert.Equal(LogEventIds.BeginRequest, logEntry1.EventId);
			Assert.Equal("Performing GET request to 'http://localhost:1234/api/v1/namespaces/default/pods/foo'.",
				logEntry1.Message
			);
			Assert.Equal("GET",
				logEntry1.Properties["Method"]
			);
			Assert.Equal(new Uri("http://localhost:1234/api/v1/namespaces/default/pods/foo"),
				logEntry1.Properties["RequestUri"]
			);

			LogEntry logEntry2 = logEntries[1];
			Assert.Equal(LogEventIds.EndRequest, logEntry2.EventId);
			Assert.Equal("Completed GET request to 'http://localhost:1234/api/v1/namespaces/default/pods/foo' (NotFound).",
				logEntry2.Message
			);
			Assert.Equal("GET",
				logEntry2.Properties["Method"]
			);
			Assert.Equal(new Uri("http://localhost:1234/api/v1/namespaces/default/pods/foo"),
				logEntry2.Properties["RequestUri"]
			);
			Assert.Equal(HttpStatusCode.NotFound,
				logEntry2.Properties["StatusCode"]
			);
        }

        /// <summary>
        ///     Verify that the client's logger emits BeginRequest / EndRequest log entries for PodV1 get-by-name.
        /// </summary>
        [Fact(DisplayName = "Emit BeginRequest / EndRequest log entries for PodV1 get-by-name (OK)")]
        public async Task PodsV1_GetByName_OK()
        {
            var logEntries = new List<LogEntry>();

			TestLogger logger = new TestLogger(LogLevel.Information);
			logger.LogEntries.Subscribe(
				logEntry => logEntries.Add(logEntry)
			);

			ClientBuilder clientBuilder = new ClientBuilder()
				.WithLogging(logger);

            HttpClient httpClient = clientBuilder.CreateClient("http://localhost:1234", TestHandlers.RespondWith(request =>
            {
                return request.CreateResponse(HttpStatusCode.OK,
                    responseBody: JsonConvert.SerializeObject(new PodV1
                    {
                        Metadata = new ObjectMetaV1
                        {
                            Name = "foo",
                            Namespace = "default"
                        }
                    }),
                    WellKnownMediaTypes.Json
                );
            }));

            KubeClientOptions clientOptions = new KubeClientOptions("http://localhost:1234");
            using (KubeApiClient kubeClient = KubeApiClient.CreateTestClient(httpClient, clientOptions))
            {
                PodV1 pod = await kubeClient.PodsV1().Get(name: "foo");
                Assert.NotNull(pod);
                Assert.NotNull(pod.Metadata);
                Assert.Equal("foo", pod.Metadata.Name);
                Assert.Equal("default", pod.Metadata.Namespace);
            }

            Assert.Equal(2, logEntries.Count);

			LogEntry logEntry1 = logEntries[0];
            Assert.Equal(LogLevel.Debug, logEntry1.Level);
			Assert.Equal(LogEventIds.BeginRequest, logEntry1.EventId);
			Assert.Equal("Performing GET request to 'http://localhost:1234/api/v1/namespaces/default/pods/foo'.",
				logEntry1.Message
			);
			Assert.Equal("GET",
				logEntry1.Properties["Method"]
			);
			Assert.Equal(new Uri("http://localhost:1234/api/v1/namespaces/default/pods/foo"),
				logEntry1.Properties["RequestUri"]
			);

			LogEntry logEntry2 = logEntries[1];
            Assert.Equal(LogLevel.Debug, logEntry2.Level);
			Assert.Equal(LogEventIds.EndRequest, logEntry2.EventId);
			Assert.Equal("Completed GET request to 'http://localhost:1234/api/v1/namespaces/default/pods/foo' (OK).",
				logEntry2.Message
			);
			Assert.Equal("GET",
				logEntry2.Properties["Method"]
			);
			Assert.Equal(new Uri("http://localhost:1234/api/v1/namespaces/default/pods/foo"),
				logEntry2.Properties["RequestUri"]
			);
			Assert.Equal(HttpStatusCode.OK,
				logEntry2.Properties["StatusCode"]
			);
        }

        /// <summary>
        ///     Verify that the client's logger emits the correct log entry from a custom request action.
        /// </summary>
        [Fact(DisplayName = "Emit log entry from custom request action")]
        public async Task Custom_Request_Action()
        {
            var logEntries = new List<LogEntry>();

            TestLogger logger = new TestLogger(LogLevel.Information);
            logger.LogEntries.Subscribe(
                logEntry => logEntries.Add(logEntry)
            );

            ClientBuilder clientBuilder = new ClientBuilder()
                .WithLogging(logger);

            HttpClient httpClient = clientBuilder.CreateClient("http://localhost:1234/api", TestHandlers.RespondWith(request =>
            {
                return request.CreateResponse(HttpStatusCode.OK,
                    responseBody: JsonConvert.SerializeObject(new StatusV1
                    {
                        Status = "Success",
                    }),
                    WellKnownMediaTypes.Json
                );
            }));

            HttpRequest request = HttpRequest.Create("{Foo}/{Bar}?Baz={Baz}")
                .WithRequestAction((HttpRequestMessage requestMessage) =>
                {
                    logger.LogDebug("Start streaming {RequestMethod} request for {RequestUri}...", HttpMethod.Get, requestMessage.RequestUri.SafeGetPathAndQuery());
                }).WithTemplateParameters(new
                {
                    Foo = "foo",
                    Bar = "bAr",
                    Baz = "b4z",
                });

            using (HttpResponseMessage response = await httpClient.GetAsync(request))
            {
                // [0] = Custom message, [1] = Begin request, [2] End request
                Assert.Equal(3, logEntries.Count);

                Assert.Equal("Start streaming GET request for /api/foo/bAr?Baz=b4z...", logEntries[0].Message);
                
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
