using HTTPlease.Testability;
using Newtonsoft.Json;
using System.Net;
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
