using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KubeClient.Tests.Http.Formatters
{
    using KubeClient.Http.Formatters;
    using KubeClient.Http.Formatters.Json;
    using KubeClient.Http.Testability.Xunit;

    /// <summary>
    ///		Factory methods for JSON-formatted mocked <see cref="HttpClient"/>s used by tests.
    /// </summary>
    public static class JsonTestClients
    {
        /// <summary>
        ///		Create an <see cref="HttpClient"/> that always responds to requests with the specified status code.
        /// </summary>
        /// <typeparam name="TBody">
        ///		The response body type.
        /// </typeparam>
        /// <param name="statusCode">
        ///		The HTTP status code.
        /// </param>
        /// <param name="body">
        ///		The response body (will be serialised as JSON).
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient RespondWith<TBody>(HttpStatusCode statusCode, TBody body)
        {
            return TestClients.RespondWith(
                request => request.CreateResponse(statusCode, body, WellKnownMediaTypes.Json, new JsonFormatter())
            );
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming request message and returns a predefined (JSON-formatted) response.
        /// </summary>
        /// <typeparam name="TResponseBody">
        ///		The type to be sent as a response body.
        /// </typeparam>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="expectedRequestMethod">
        ///		The expected HTTP method (e.g. GET / POST / PUT) for the incoming request message.
        /// </param>
        /// <param name="responseBody">
        ///		The <typeparamref name="TResponseBody"/> instance to be serialised into the outgoing response message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectJson<TResponseBody>(Uri expectedRequestUri, HttpMethod expectedRequestMethod, TResponseBody responseBody)
        {
            return ExpectJson(expectedRequestUri, expectedRequestMethod, responseBody, assertion: null);
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming request message and returns a predefined (JSON-formatted) response.
        /// </summary>
        /// <typeparam name="TResponseBody">
        ///		The type to be sent as a response body.
        /// </typeparam>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="expectedRequestMethod">
        ///		The expected HTTP method (e.g. GET / POST / PUT) for the incoming request message.
        /// </param>
        /// <param name="responseBody">
        ///		The <typeparamref name="TResponseBody"/> instance to be serialised into the outgoing response message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectJson<TResponseBody>(Uri expectedRequestUri, HttpMethod expectedRequestMethod, TResponseBody responseBody, Action<HttpRequestMessage> assertion)
        {
            return ExpectJson(expectedRequestUri, expectedRequestMethod, HttpStatusCode.OK, responseBody, assertion);
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming request message and returns a predefined (JSON-formatted) response.
        /// </summary>
        /// <typeparam name="TResponseBody">
        ///		The type to be sent as a response body.
        /// </typeparam>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="expectedRequestMethod">
        ///		The expected HTTP method (e.g. GET / POST / PUT) for the incoming request message.
        /// </param>
        /// <param name="responseStatusCode">
        ///		The HTTP status code for the outgoing response message.
        /// </param>
        /// <param name="responseBody">
        ///		The <typeparamref name="TResponseBody"/> instance to be serialised into the outgoing response message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectJson<TResponseBody>(Uri expectedRequestUri, HttpMethod expectedRequestMethod, HttpStatusCode responseStatusCode, TResponseBody responseBody, Action<HttpRequestMessage> assertion)
        {
            return Expect(expectedRequestUri, expectedRequestMethod, responseStatusCode, responseBody, WellKnownMediaTypes.Json, new JsonFormatter(), assertion);
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming request message and returns a predefined response.
        /// </summary>
        /// <typeparam name="TResponseBody">
        ///		The type to be sent as a response body.
        /// </typeparam>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="expectedRequestMethod">
        ///		The expected HTTP method (e.g. GET / POST / PUT) for the incoming request message.
        /// </param>
        /// <param name="responseStatusCode">
        ///		The HTTP status code for the outgoing response message.
        /// </param>
        /// <param name="responseBody">
        ///		The <typeparamref name="TResponseBody"/> instance to be serialised into the outgoing response message.
        /// </param>
        /// <param name="responseMediaType">
        ///		The media type for the ougoing response message body.
        /// </param>
        /// <param name="responseFormatter">
        ///		The <see cref="IOutputFormatter"/> used to serialise the outgoing response message.
        /// </param>
        /// <param name="assertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient Expect<TResponseBody>(Uri expectedRequestUri, HttpMethod expectedRequestMethod, HttpStatusCode responseStatusCode, TResponseBody responseBody, string responseMediaType, IOutputFormatter responseFormatter, Action<HttpRequestMessage> assertion)
        {
            if (expectedRequestUri == null)
                throw new ArgumentNullException(nameof(expectedRequestUri));

            if (expectedRequestMethod == null)
                throw new ArgumentNullException(nameof(expectedRequestMethod));

            if (String.IsNullOrWhiteSpace(responseMediaType))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'responseMediaType'.", nameof(responseMediaType));

            if (responseFormatter == null)
                throw new ArgumentNullException(nameof(responseFormatter));

            return TestClients.RespondWith(requestMessage =>
            {
                Xunit.Assert.NotNull(requestMessage);
                Xunit.Assert.Equal(expectedRequestMethod, requestMessage.Method);
                Xunit.Assert.Equal(expectedRequestUri, requestMessage.RequestUri);

                assertion?.Invoke(requestMessage);

                return requestMessage.CreateResponse(
                    responseStatusCode,
                    responseBody,
                    responseMediaType,
                    new JsonFormatter()
                );
            });
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming request message and returns a predefined (JSON-formatted) response.
        /// </summary>
        /// <typeparam name="TResponseBody">
        ///		The type to be sent as a response body.
        /// </typeparam>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="expectedRequestMethod">
        ///		The expected HTTP method (e.g. GET / POST / PUT) for the incoming request message.
        /// </param>
        /// <param name="responseBody">
        ///		The <typeparamref name="TResponseBody"/> instance to be serialised into the outgoing response message.
        /// </param>
        /// <param name="asyncAssertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectJson<TResponseBody>(Uri expectedRequestUri, HttpMethod expectedRequestMethod, TResponseBody responseBody, Func<HttpRequestMessage, Task> asyncAssertion)
        {
            return ExpectJson(expectedRequestUri, expectedRequestMethod, HttpStatusCode.OK, responseBody, asyncAssertion);
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming request message and returns a predefined (JSON-formatted) response.
        /// </summary>
        /// <typeparam name="TResponseBody">
        ///		The type to be sent as a response body.
        /// </typeparam>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="expectedRequestMethod">
        ///		The expected HTTP method (e.g. GET / POST / PUT) for the incoming request message.
        /// </param>
        /// <param name="responseStatusCode">
        ///		The HTTP status code for the outgoing response message.
        /// </param>
        /// <param name="responseBody">
        ///		The <typeparamref name="TResponseBody"/> instance to be serialised into the outgoing response message.
        /// </param>
        /// <param name="asyncAssertion">
        ///		A delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient ExpectJson<TResponseBody>(Uri expectedRequestUri, HttpMethod expectedRequestMethod, HttpStatusCode responseStatusCode, TResponseBody responseBody, Func<HttpRequestMessage, Task> asyncAssertion)
        {
            return Expect(expectedRequestUri, expectedRequestMethod, responseStatusCode, responseBody, WellKnownMediaTypes.Json, new JsonFormatter(), asyncAssertion);
        }

        /// <summary>
        ///		Create an <see cref="HttpClient"/> that performs assertions on an incoming request message and returns a predefined response.
        /// </summary>
        /// <typeparam name="TResponseBody">
        ///		The type to be sent as a response body.
        /// </typeparam>
        /// <param name="expectedRequestUri">
        ///		The expected URI for the incoming request message.
        /// </param>
        /// <param name="expectedRequestMethod">
        ///		The expected HTTP method (e.g. GET / POST / PUT) for the incoming request message.
        /// </param>
        /// <param name="responseStatusCode">
        ///		The HTTP status code for the outgoing response message.
        /// </param>
        /// <param name="responseBody">
        ///		The <typeparamref name="TResponseBody"/> instance to be serialised into the outgoing response message.
        /// </param>
        /// <param name="responseMediaType">
        ///		The media type for the ougoing response message body.
        /// </param>
        /// <param name="responseFormatter">
        ///		The <see cref="IOutputFormatter"/> used to serialise the outgoing response message.
        /// </param>
        /// <param name="asyncAssertion">
        ///		An asynchronous delegate that makes assertions about the incoming request message.
        /// </param>
        /// <returns>
        ///		The configured <see cref="HttpClient"/>.
        /// </returns>
        public static HttpClient Expect<TResponseBody>(Uri expectedRequestUri, HttpMethod expectedRequestMethod, HttpStatusCode responseStatusCode, TResponseBody responseBody, string responseMediaType, IOutputFormatter responseFormatter, Func<HttpRequestMessage, Task> asyncAssertion)
        {
            if (expectedRequestUri == null)
                throw new ArgumentNullException(nameof(expectedRequestUri));

            if (expectedRequestMethod == null)
                throw new ArgumentNullException(nameof(expectedRequestMethod));

            if (String.IsNullOrWhiteSpace(responseMediaType))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'responseMediaType'.", nameof(responseMediaType));

            if (responseFormatter == null)
                throw new ArgumentNullException(nameof(responseFormatter));

            return TestClients.AsyncRespondWith(async requestMessage =>
            {
                Xunit.Assert.NotNull(requestMessage);
                Xunit.Assert.Equal(expectedRequestMethod, requestMessage.Method);
                Xunit.Assert.Equal(expectedRequestUri, requestMessage.RequestUri);

                Task assertionTask = asyncAssertion?.Invoke(requestMessage);
                if (assertionTask != null)
                    await assertionTask;

                return requestMessage.CreateResponse(
                    responseStatusCode,
                    responseBody,
                    responseMediaType,
                    new JsonFormatter()
                );
            });
        }
    }
}
