using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KubeClient.Tests.Http.Formatters
{
    using KubeClient.Http;
    using KubeClient.Http.Formatters.Json;

    /// <summary>
    ///		Tests for reading responses from invoked messages.
    /// </summary>
    public class ReadResponseTests
    {
        /// <summary>
        ///		The default request used for tests.
        /// </summary>
        static readonly HttpRequest DefaultRequest = HttpRequest.Factory.Create("http://localhost/");

        /// <summary>
        ///		Create a new response-read test suite.
        /// </summary>
        public ReadResponseTests()
        {
        }

        /// <summary>
        ///		Verify that the body of an <see cref="HttpRequest"/>'s response (with a status code that indicates success) can be read using the JSON formatter.
        /// </summary>
        /// <returns>
        ///		A <see cref="Task"/> representing asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Response_Read_Json_Success()
        {
            TestBody expectedBody = new TestBody
            {
                StringProperty = "This is a test",
                IntProperty = 123
            };
            using (HttpClient client = JsonTestClients.RespondWith(HttpStatusCode.OK, expectedBody))
            {
                TestBody actualBody = await client
                    .GetAsync(DefaultRequest)
                    .ReadContentAsAsync<TestBody>(new NewtonsoftJsonFormatter());

                Assert.NotNull(actualBody);
                Assert.NotSame(expectedBody, actualBody);
                Assert.Equal(expectedBody.StringProperty, actualBody.StringProperty);
                Assert.Equal(expectedBody.IntProperty, actualBody.IntProperty);
            }
        }

        /// <summary>
        ///		Verify that the body of an <see cref="HttpRequest"/>'s response (with a status code that has been declared as indicating success) can be read using the JSON formatter.
        /// </summary>
        /// <returns>
        ///		A <see cref="Task"/> representing asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Response_Read_Json_TreatAsSuccess()
        {
            TestBody expectedBody = new TestBody
            {
                StringProperty = "This is a test",
                IntProperty = 123
            };
            using (HttpClient client = JsonTestClients.RespondWith(HttpStatusCode.BadRequest, expectedBody))
            {
                TestBody actualBody = await client
                    .GetAsync(DefaultRequest)
                    .ReadContentAsAsync<TestBody>(new NewtonsoftJsonFormatter(), HttpStatusCode.OK, HttpStatusCode.BadRequest);

                Assert.NotNull(actualBody);
                Assert.NotSame(expectedBody, actualBody);
                Assert.Equal(expectedBody.StringProperty, actualBody.StringProperty);
                Assert.Equal(expectedBody.IntProperty, actualBody.IntProperty);
            }
        }

        /// <summary>
        ///		Verify that the body of an <see cref="HttpRequest"/>'s response (with a status code that indicates failure) can be transformed by the onFailureResponse handler into a valid response body.
        /// </summary>
        /// <returns>
        ///		A <see cref="Task"/> representing asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Response_Read_Json_Failure_Transformed()
        {
            TestBody expectedBody = new TestBody
            {
                StringProperty = "This is a failure response",
                IntProperty = 456
            };

            TestBody responseBody = new TestBody
            {
                StringProperty = "This is a test",
                IntProperty = 123
            };
            using (HttpClient client = JsonTestClients.RespondWith(HttpStatusCode.BadRequest, responseBody))
            {
                TestBody actualBody = await client
                    .GetAsync(DefaultRequest)
                    .ReadContentAsAsync(new NewtonsoftJsonFormatter(),
                        onFailureResponse: () => new TestBody
                        {
                            StringProperty = expectedBody.StringProperty,
                            IntProperty = expectedBody.IntProperty
                        }
                    );

                Assert.NotNull(actualBody);
                Assert.NotSame(expectedBody, actualBody);
                Assert.Equal(expectedBody.StringProperty, actualBody.StringProperty);
                Assert.Equal(expectedBody.IntProperty, actualBody.IntProperty);
            }
        }

        /// <summary>
        ///		A simple data-type to be deserialised from the response.
        /// </summary>
        /// <remarks>
        ///		Must be public to support XML serialisation (or have the DataContract attribute applied to it).
        /// </remarks>
        public class TestBody
        {
            /// <summary>
            ///		A string property.
            /// </summary>
            public string StringProperty { get; set; }

            /// <summary>
            ///		An integer property.
            /// </summary>
            public int IntProperty { get; set; }
        }
    }
}
