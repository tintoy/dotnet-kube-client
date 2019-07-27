using HTTPlease.Testability;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace KubeClient.Tests
{
    using ErrorHandling;
    using Models;
    using ResourceClients;
    using TestCommon;

    /// <summary>
    ///     Integration tests for server-side apply (via <see cref="IDynamicResourceClient"/>).
    /// </summary>
    public class ServerSideApplyTests
        : TestBase
    {
        /// <summary>
        ///     Create a new server-side-apply test suite.
        /// </summary>
        /// <param name="testOutput">
        ///     Output for the current test.
        /// </param>
        public ServerSideApplyTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        /// <summary>
        /// Verify that server-side apply of a model results in a PATCH request with the correct content type.
        /// </summary>
        [Fact(DisplayName = "Server-side apply of model")]
        public async Task ApplyModel()
        {
            MockMessageHandler handler = new MockMessageHandler(async (HttpRequestMessage request) =>
            {
                Assert.Equal("PATCH", request.Method.Method);

                Assert.NotNull(request.Content);

                string requestBody = await request.Content.ReadAsStringAsync();
                Log.LogInformation("Request body:\n{RequestBody:l}", requestBody);

                Assert.NotNull(request.Content.Headers.ContentType);
                Assert.Equal("application/apply-patch+yaml", request.Content.Headers.ContentType.MediaType);

                Assert.Contains("fieldManager=my-field-manager", request.RequestUri.Query);

                return request.CreateResponse(HttpStatusCode.OK,
                    responseBody: JsonConvert.SerializeObject(new PodV1
                    {
                        Metadata = new ObjectMetaV1
                        {
                            Name = "my-pod",
                            Namespace = "my-namespace",
                            Finalizers = // Array test
                            {
                                "foo",
                                "bar"
                            }
                        }
                    }),
                    mediaType: "application/json"
                );
            });

            using (KubeApiClient client = handler.CreateClient(loggerFactory: LoggerFactory))
            {
                PodV1 resource = new PodV1
                {
                    Metadata = new ObjectMetaV1
                    {
                        Name = "my-pod",
                        Namespace = "my-namespace",
                        Finalizers = // Array test
                        {
                            "foo",
                            "bar"
                        }
                    }
                };

                await client.Dynamic().Apply(resource,
                    fieldManager: "my-field-manager"
                );
            }
        }

        /// <summary>
        /// Verify that server-side apply of raw YAML results in a PATCH request with the correct content type.
        /// </summary>
        [Fact( DisplayName = "Server-side apply of raw YAML" )]
        public async Task ApplyYaml()
        {
            MockMessageHandler handler = new MockMessageHandler(request =>
            {
                Assert.Equal("PATCH", request.Method.Method);

                Assert.NotNull(request.Content);
                Assert.NotNull(request.Content.Headers.ContentType);
                Assert.Equal("application/apply-patch+yaml", request.Content.Headers.ContentType.MediaType);

                Assert.Contains("fieldManager=my-field-manager", request.RequestUri.Query);

                return request.CreateResponse(HttpStatusCode.OK,
                    responseBody: JsonConvert.SerializeObject(new PodV1
                    {
                        Metadata = new ObjectMetaV1
                        {
                            Name = "my-pod",
                            Namespace = "my-namespace"
                        }
                    }),
                    mediaType: "application/json"
                );
            });

            const string podYaml = @"
kind: Pod
apiVersion: v1
metadata:
    name: my-pod
            ";

            using (KubeApiClient client = handler.CreateClient(loggerFactory: LoggerFactory))
            {
                await client.Dynamic().ApplyYaml(
                    name: "my-pod",
                    kind: "Pod",
                    apiVersion: "v1",
                    yaml: podYaml,
                    fieldManager: "my-field-manager",
                    kubeNamespace: "my-namespace"
                );
            }
        }
    }
}
