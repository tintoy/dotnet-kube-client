using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KubeClient.Http.Testability.Xunit
{
    using MessageHandlers;

    /// <summary>
    ///		Factory methods for mocked <see cref="MockMessageHandler"/>s used by tests.
    /// </summary>
    public static class TestHandlers
	{
		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that always responds to requests with the <see cref="HttpStatusCode.OK"/> status code.
		/// </summary>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler RespondWithOk()
		{
			return RespondWith(HttpStatusCode.OK);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that always responds to requests with the <see cref="HttpStatusCode.BadRequest"/> status code.
		/// </summary>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler RespondWithBadRequest()
		{
			return RespondWith(HttpStatusCode.BadRequest);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that always responds to requests with the <see cref="HttpStatusCode.BadRequest"/> status code.
		/// </summary>
		/// <param name="responseBody">
		///		A string to be used as the response message body.
		/// </param>
		/// <param name="responseMediaType">
		///		The response media type.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler RespondWithBadRequest(string responseBody, string responseMediaType)
		{
			if (String.IsNullOrWhiteSpace(responseMediaType))
				throw new ArgumentException("Must specify a valid media type.", nameof(responseMediaType));

			return RespondWith(
				request => request.CreateResponse(HttpStatusCode.BadRequest, responseBody, responseMediaType)
			);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that always responds to requests with the specified status code.
		/// </summary>
		/// <param name="statusCode">
		///		The HTTP status code.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler RespondWith(HttpStatusCode statusCode)
		{
			return RespondWith(
				request => request.CreateResponse(statusCode)
			);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that calls the specified delegate to synchronously respond to requests.
		/// </summary>
		/// <param name="handler">
		///		A delegate that takes an incoming <see cref="HttpRequest"/> and returns an outgoing <see cref="HttpResponseMessage"/>.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler RespondWith(Func<HttpRequestMessage, HttpResponseMessage> handler)
		{
			if (handler == null)
				throw new ArgumentNullException(nameof(handler));

			return new MockMessageHandler(handler);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that calls the specified delegate to asynchronously respond to requests.
		/// </summary>
		/// <param name="handler">
		///		A delegate that takes an incoming <see cref="HttpRequest"/> and asynchronously returns an outgoing <see cref="HttpResponseMessage"/>.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler AsyncRespondWith(Func<HttpRequestMessage, Task<HttpResponseMessage>> handler)
		{
			if (handler == null)
				throw new ArgumentNullException(nameof(handler));

			return new MockMessageHandler(handler);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that expects an incoming GET request message and returns an <see cref="HttpStatusCode.OK"/> response.
		/// </summary>
		/// <param name="expectedRequestUri">
		///		The expected URI for the incoming request message.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectGet(Uri expectedRequestUri)
		{
			return ExpectGet(expectedRequestUri, HttpStatusCode.OK, assertion: null);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming GET request message and returns an <see cref="HttpStatusCode.OK"/> response.
		/// </summary>
		/// <param name="expectedRequestUri">
		///		The expected URI for the incoming request message.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the incoming request message.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectGet(Uri expectedRequestUri, Action<HttpRequestMessage> assertion)
		{
			return ExpectGet(expectedRequestUri, HttpStatusCode.OK, assertion);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming GET request message and returns a predefined response.
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
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectGet(Uri expectedRequestUri, HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
		{
			return Expect(expectedRequestUri, HttpMethod.Get, responseStatusCode, assertion);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that expects an incoming POST request message and returns an <see cref="HttpStatusCode.OK"/> response.
		/// </summary>
		/// <param name="expectedRequestUri">
		///		The expected URI for the incoming request message.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectPost(Uri expectedRequestUri)
		{
			return ExpectPost(expectedRequestUri, HttpStatusCode.OK, assertion: null);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming POST request message and returns an <see cref="HttpStatusCode.OK"/> response.
		/// </summary>
		/// <param name="expectedRequestUri">
		///		The expected URI for the incoming request message.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the incoming request message.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectPost(Uri expectedRequestUri, Action<HttpRequestMessage> assertion)
		{
			return ExpectPost(expectedRequestUri, HttpStatusCode.OK, assertion);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming POST request message and returns a predefined response.
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
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectPost(Uri expectedRequestUri, HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
		{
			return Expect(expectedRequestUri, HttpMethod.Post, responseStatusCode, assertion);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that expects an incoming PUT request message and returns an <see cref="HttpStatusCode.OK"/> response.
		/// </summary>
		/// <param name="expectedRequestUri">
		///		The expected URI for the incoming request message.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectPut(Uri expectedRequestUri)
		{
			return ExpectPut(expectedRequestUri, HttpStatusCode.OK, assertion: null);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming PUT request message and returns an <see cref="HttpStatusCode.OK"/> response.
		/// </summary>
		/// <param name="expectedRequestUri">
		///		The expected URI for the incoming request message.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the incoming request message.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectPut(Uri expectedRequestUri, Action<HttpRequestMessage> assertion)
		{
			return ExpectPut(expectedRequestUri, HttpStatusCode.OK, assertion);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming PUT request message and returns a predefined response.
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
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectPut(Uri expectedRequestUri, HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
		{
			return Expect(expectedRequestUri, HttpMethod.Put, responseStatusCode, assertion);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that expects an incoming DELETE request message and returns an <see cref="HttpStatusCode.OK"/> response.
		/// </summary>
		/// <param name="expectedRequestUri">
		///		The expected URI for the incoming request message.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectDelete(Uri expectedRequestUri)
		{
			return ExpectDelete(expectedRequestUri, HttpStatusCode.OK, assertion: null);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming DELETE request message and returns an <see cref="HttpStatusCode.OK"/> response.
		/// </summary>
		/// <param name="expectedRequestUri">
		///		The expected URI for the incoming request message.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the incoming request message.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectDelete(Uri expectedRequestUri, Action<HttpRequestMessage> assertion)
		{
			return ExpectDelete(expectedRequestUri, HttpStatusCode.OK, assertion);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming DELETE request message and returns a predefined response.
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
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler ExpectDelete(Uri expectedRequestUri, HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
		{
			return Expect(expectedRequestUri, HttpMethod.Delete, responseStatusCode, assertion);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming request message and returns a predefined response.
		/// </summary>
		/// <param name="assertion">
		///		A delegate that makes assertions about the incoming request message.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler Expect(Action<HttpRequestMessage> assertion)
		{
			return Expect(HttpStatusCode.OK, assertion);
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming request message and returns a predefined response.
		/// </summary>
		/// <param name="responseStatusCode">
		///		The HTTP status code for the outgoing response message.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the incoming request message.
		/// </param>
		/// <returns>
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler Expect(HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
		{
			return RespondWith(requestMessage =>
			{
				Assert.NotNull(requestMessage);

				assertion?.Invoke(requestMessage);

				return requestMessage.CreateResponse(responseStatusCode);
			});
		}

		/// <summary>
		///		Create an <see cref="MockMessageHandler"/> that performs assertions on an incoming request message and returns a predefined response.
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
		///		The configured <see cref="MockMessageHandler"/>.
		/// </returns>
		public static MockMessageHandler Expect(Uri expectedRequestUri, HttpMethod expectedRequestMethod, HttpStatusCode responseStatusCode, Action<HttpRequestMessage> assertion)
		{
			if (expectedRequestUri == null)
				throw new ArgumentNullException(nameof(expectedRequestUri));

			if (expectedRequestMethod == null)
				throw new ArgumentNullException(nameof(expectedRequestMethod));

			return Expect(responseStatusCode, requestMessage =>
			{
				Assert.Equal(expectedRequestMethod, requestMessage.Method);
				Assert.Equal(expectedRequestUri, requestMessage.RequestUri);

				assertion?.Invoke(requestMessage);
			});
		}
	}
}
