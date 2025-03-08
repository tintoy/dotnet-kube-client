using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KubeClient.Http.Testability.Xunit
{
    /// <summary>
    ///		Factory methods for mocked <see cref="HttpClient"/>s used by tests.
    /// </summary>
    public static class TestClients
    {
        /// <summary>
        ///		Create an <see cref="HttpClient"/> that always responds to requests with the <see cref="HttpStatusCode.OK"/> status code.
        /// </summary>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient RespondWithOk()
        {
            return TestHandlers.RespondWith(HttpStatusCode.OK).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that always responds to requests with the <see cref="HttpStatusCode.BadRequest"/> status code.
        /// </summary>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient RespondWithBadRequest()
        {
            return TestHandlers.RespondWith(HttpStatusCode.BadRequest).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that always responds to requests with the <see cref="HttpStatusCode.BadRequest"/> status code.
        /// </summary>
        /// <param name="responseBody">
        ///		A string to be used as the response message body.
        /// </param>
        /// <param name="responseMediaType">
        ///		The response media type.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient RespondWithBadRequest(string responseBody, string responseMediaType)
        {
            if (String.IsNullOrWhiteSpace(responseMediaType))
                throw new ArgumentException("Must specify a valid media type.", nameof(responseMediaType));

            return
                TestHandlers.RespondWith(
                    request => request.CreateResponse(HttpStatusCode.BadRequest, responseBody, responseMediaType)
                )
                .CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that always responds to requests with the specified status code.
        /// </summary>
        /// <param name="statusCode">
        ///		The HTTP status code.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient RespondWith(HttpStatusCode statusCode)
        {
            return
                TestHandlers.RespondWith(
                    request => request.CreateResponse(statusCode)
                )
                .CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that calls the specified delegate to synchronously respond to requests.
        /// </summary>
        /// <param name="handler">
        ///		A delegate that takes an incoming <see cref="HttpRequest"/> and returns an outgoing <see cref="HttpResponseMessage"/>.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient RespondWith(Func<HttpRequestMessage, HttpResponseMessage> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            return TestHandlers.RespondWith(handler).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that calls the specified delegate to asynchronously respond to requests.
        /// </summary>
        /// <param name="handler">
        ///		A delegate that takes an incoming <see cref="HttpRequest"/> and asynchronously returns an outgoing <see cref="HttpResponseMessage"/>.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient AsyncRespondWith(Func<HttpRequestMessage, Task<HttpResponseMessage>> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            return TestHandlers.AsyncRespondWith(handler).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that expects an incoming GET request message and returns an <see cref="HttpStatusCode.OK"/> response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectGet(Uri expectedRequestUri)
        {
            return TestHandlers.ExpectGet(expectedRequestUri, HttpStatusCode.OK, assertion: null).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming GET request message and returns an <see cref="HttpStatusCode.OK"/> response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectGet(Uri expectedRequestUri, Action<HttpRequestMessage> assertion)
        {
            return TestHandlers.ExpectGet(expectedRequestUri, HttpStatusCode.OK, assertion).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming GET request message and returns a predefined response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="responseStatusCode">
        ///		The HTTP status code for the outgoing response message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectGet(Uri expectedRequestUri, HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
        {
            return TestHandlers.Expect(expectedRequestUri, HttpMethod.Get, responseStatusCode, assertion).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that expects an incoming POST request message and returns an <see cref="HttpStatusCode.OK"/> response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectPost(Uri expectedRequestUri)
        {
            return TestHandlers.ExpectPost(expectedRequestUri, HttpStatusCode.OK, assertion: null).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming POST request message and returns an <see cref="HttpStatusCode.OK"/> response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectPost(Uri expectedRequestUri, Action<HttpRequestMessage> assertion)
        {
            return TestHandlers.ExpectPost(expectedRequestUri, HttpStatusCode.OK, assertion).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming POST request message and returns a predefined response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="responseStatusCode">
        ///		The HTTP status code for the outgoing response message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectPost(Uri expectedRequestUri, HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
        {
            return TestHandlers.Expect(expectedRequestUri, HttpMethod.Post, responseStatusCode, assertion).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that expects an incoming PUT request message and returns an <see cref="HttpStatusCode.OK"/> response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectPut(Uri expectedRequestUri)
        {
            return TestHandlers.ExpectPut(expectedRequestUri, HttpStatusCode.OK, assertion: null).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming PUT request message and returns an <see cref="HttpStatusCode.OK"/> response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectPut(Uri expectedRequestUri, Action<HttpRequestMessage> assertion)
        {
            return TestHandlers.ExpectPut(expectedRequestUri, HttpStatusCode.OK, assertion).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming PUT request message and returns a predefined response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="responseStatusCode">
        ///		The HTTP status code for the outgoing response message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectPut(Uri expectedRequestUri, HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
        {
            return TestHandlers.Expect(expectedRequestUri, HttpMethod.Put, responseStatusCode, assertion).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that expects an incoming DELETE request message and returns an <see cref="HttpStatusCode.OK"/> response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectDelete(Uri expectedRequestUri)
        {
            return TestHandlers.ExpectDelete(expectedRequestUri, HttpStatusCode.OK, assertion: null).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming DELETE request message and returns an <see cref="HttpStatusCode.OK"/> response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectDelete(Uri expectedRequestUri, Action<HttpRequestMessage> assertion)
        {
            return TestHandlers.ExpectDelete(expectedRequestUri, HttpStatusCode.OK, assertion).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming DELETE request message and returns a predefined response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="responseStatusCode">
        ///		The HTTP status code for the outgoing response message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectDelete(Uri expectedRequestUri, HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
        {
            return TestHandlers.Expect(expectedRequestUri, HttpMethod.Delete, responseStatusCode, assertion).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming request message and returns a predefined response.
        /// </summary>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient Expect(Action<HttpRequestMessage> assertion)
        {
            return TestHandlers.Expect(HttpStatusCode.OK, assertion).CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming request message and returns a predefined response.
        /// </summary>
        /// <param name="responseStatusCode">
        ///		The HTTP status code for the outgoing response message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient Expect(HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
        {
            return
                TestHandlers.RespondWith(requestMessage =>
                {
                    Assert.NotNull(requestMessage);

                    assertion?.Invoke(requestMessage);

                    return requestMessage.CreateResponse(responseStatusCode);
                })
                .CreateClient();
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming request message and returns a predefined response.
        /// </summary>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="expectedRequestMethod">
        ///		The expected HTTP method (e.g. GET / POST / PUT) for the incoming request message.
        /// </param>
        /// <param name="responseStatusCode">
        ///		The HTTP status code for the outgoing response message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient Expect(Uri expectedRequestUri, HttpMethod expectedRequestMethod, HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
        {
            if (expectedRequestUri == null)
                throw new ArgumentNullException(nameof(expectedRequestUri));

            if (expectedRequestMethod == null)
                throw new ArgumentNullException(nameof(expectedRequestMethod));

            return
                TestHandlers.Expect(responseStatusCode, requestMessage =>
                {
                    Assert.Equal(expectedRequestMethod, requestMessage.Method);
                    Assert.Equal(expectedRequestUri, requestMessage.RequestUri);

                    assertion?.Invoke(requestMessage);
                })
                .CreateClient();
        }
    }
}
