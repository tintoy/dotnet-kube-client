using HTTPlease.Testability;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Tests.ErrorHandling
{
    using Models;
    using TestCommon;

    /// <summary>
    ///     Tests relating to exceptions raised by <see cref="KubeApiClient"/>.
    /// </summary>
    public class ExceptionTests
        : TestBase
    {
        /// <summary>
        ///     Create a new <see cref="KubeApiClient"/> exception test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public ExceptionTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        ///     Verify that listing Pod (v1) results in a <see cref="KubeClientException"/> if the server returns <see cref="HttpStatusCode.InternalServerError"/> with a valid <see cref="StatusV1"/> in the response body.
        /// </summary>
        [Fact(DisplayName = "List PodV1 returning InternalServerError with StatusV1 body throws KubeClientException")]
        public async Task List_Pods_InternalServerError_StatusV1_Throws_KubeClientException()
        {
            MockMessageHandler handler = new MockMessageHandler(
                (HttpRequestMessage request) => request.CreateResponse(HttpStatusCode.InternalServerError,
                    responseBody: JsonConvert.SerializeObject(new StatusV1
                    {
                        Code = 500,
                        Reason = "TestError",
                        Message = "Simulated API error.",
                        Details = new StatusDetailsV1()
                    }),
                    mediaType: "application/json"
                )
            );

            KubeApiException exception;
            using (KubeApiClient client = handler.CreateClient(loggerFactory: LoggerFactory))
            {
                exception = await Assert.ThrowsAsync<KubeApiException>(
                    () => client.PodsV1().List()
                );
            }

            Assert.True(exception.HasStatus);
            Assert.Equal(500, exception.Status.Code);
            Assert.Equal("TestError", exception.Status.Reason);
            Assert.Equal("Simulated API error.", exception.Status.Message);
        }

        /// <summary>
        ///     Verify that listing Pod (v1) results in a <see cref="KubeClientException"/> if the server returns <see cref="HttpStatusCode.InternalServerError"/> with a valid <see cref="StatusV1"/> in the response body.
        /// </summary>
        [Fact(DisplayName = "List PodV1 returning InternalServerError with StatusV1 body throws KubeClientException")]
        public async Task Delete_Pod_InternalServerError_StatusV1_Throws_KubeApiException()
        {
            MockMessageHandler handler = new MockMessageHandler(
                (HttpRequestMessage request) => request.CreateResponse(HttpStatusCode.InternalServerError,
                    responseBody: JsonConvert.SerializeObject(new StatusV1
                    {
                        Code = 500,
                        Reason = "TestError",
                        Message = "Simulated API error.",
                        Details = new StatusDetailsV1()
                    }),
                    mediaType: "application/json"
                )
            );

            KubeApiException exception;
            using (KubeApiClient client = handler.CreateClient(loggerFactory: LoggerFactory))
            {
                exception = await Assert.ThrowsAsync<KubeApiException>(async () =>
                {
                    await client.PodsV1().Delete("my-pod", propagationPolicy: DeletePropagationPolicy.Background);
                });

                Assert.True(exception.HasStatus);
                Assert.Equal(500, exception.Status.Code);
                Assert.Equal("TestError", exception.Status.Reason);
                Assert.Equal("Simulated API error.", exception.Status.Message);

                exception = await Assert.ThrowsAsync<KubeApiException>(async () =>
                {
                    await client.PodsV1().Delete("my-pod", propagationPolicy: DeletePropagationPolicy.Foreground);
                });
                Assert.True(exception.HasStatus);
                Assert.Equal(500, exception.Status.Code);
                Assert.Equal("TestError", exception.Status.Reason);
                Assert.Equal("Simulated API error.", exception.Status.Message);
            }
        }
    }
}
