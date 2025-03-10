﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KubeClient.Http.Testability.Xunit
{
	/// <summary>
	///		Assertion functionality for <see cref="HttpRequest"/> / <see cref="HttpRequest{TContext}"/>.
	/// </summary>
	public static class RequestAssert
    {
		#region Untyped requests
		
		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		/// <remarks>
		///		Uses the HTTP GET method and no base URI.
		/// </remarks>
		public static void MessageHasUri(HttpRequest request, string expectedUri)
		{
			MessageHasUri(request, HttpMethod.Get,
				baseUri: null,
				expectedUri: expectedUri
			);
		}
		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		/// <remarks>
		///		Uses the HTTP GET method and no base URI.
		/// </remarks>
		public static void MessageHasUri(HttpRequest request, Uri expectedUri)
		{
			MessageHasUri(request, HttpMethod.Get,
				baseUri: null,
				expectedUri: expectedUri
			);
		}

		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the request.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		/// <remarks>
		///		Uses the HTTP GET method.
		/// </remarks>
		public static void MessageHasUri(HttpRequest request, Uri baseUri, string expectedUri)
		{
			MessageHasUri(request, HttpMethod.Get, baseUri, expectedUri);
		}

		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the request.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		/// <remarks>
		///		Uses the HTTP GET method.
		/// </remarks>
		public static void MessageHasUri(HttpRequest request, Uri baseUri, Uri expectedUri)
		{
			MessageHasUri(request, HttpMethod.Get, baseUri, expectedUri);
		}

		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the request.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		public static void MessageHasUri(HttpRequest request, HttpMethod method, Uri baseUri, string expectedUri)
		{
			if (String.IsNullOrWhiteSpace(expectedUri))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'expectedUri'.", nameof(expectedUri));

			Message(request, method, baseUri, requestMessage =>
			{
				MessageAssert.HasRequestUri(requestMessage, expectedUri);
			});
		}

		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the request.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		public static void MessageHasUri(HttpRequest request, HttpMethod method, Uri baseUri, Uri expectedUri)
		{
			Message(request, method, baseUri, requestMessage =>
			{
				MessageAssert.HasRequestUri(requestMessage, expectedUri);
			});
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/>.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		public static void Message(HttpRequest request, HttpMethod method, Action<HttpRequestMessage> assertion)
		{
			Message(request, method,
				bodyContent: null,
				baseUri: null,
				assertion: assertion
			);
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/>.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="baseUri">
		///		The request base URI.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		public static void Message(HttpRequest request, HttpMethod method, Uri baseUri, Action<HttpRequestMessage> assertion)
		{
			Message(request, method,
				bodyContent: null,
				baseUri: baseUri,
				assertion: assertion
			);
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/>.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="bodyContent">
		///		<see cref="HttpContent"/> representing the request body content.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		public static void Message(HttpRequest request, HttpMethod method, HttpContent bodyContent, Action<HttpRequestMessage> assertion)
		{
			Message(request, method, bodyContent, null, assertion);
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/>.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="bodyContent">
		///		<see cref="HttpContent"/> representing the request body content.
		/// </param>
		/// <param name="baseUri">
		///		An optional base URI for the request.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		public static void Message(HttpRequest request, HttpMethod method, HttpContent bodyContent, Uri baseUri, Action<HttpRequestMessage> assertion)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (assertion == null)
				throw new ArgumentNullException(nameof(assertion));

			if (method == null)
				throw new ArgumentNullException(nameof(method));

			using (HttpRequestMessage requestMessage = request.BuildRequestMessage(method, bodyContent, baseUri))
			{
				assertion(requestMessage);
			}
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/>.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="bodyContent">
		///		<see cref="HttpContent"/> representing the request body content.
		/// </param>
		/// <param name="baseUri">
		///		An optional base URI for the request.
		/// </param>
		/// <param name="asyncAssertion">
		///		An asynchronous delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		public static async Task MessageAsync(HttpRequest request, HttpMethod method, HttpContent bodyContent, Uri baseUri, Func<HttpRequestMessage, Task> asyncAssertion)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (asyncAssertion == null)
				throw new ArgumentNullException(nameof(asyncAssertion));

			if (method == null)
				throw new ArgumentNullException(nameof(method));

			using (HttpRequestMessage requestMessage = request.BuildRequestMessage(method, bodyContent, baseUri))
			{
				await asyncAssertion(requestMessage);
			}
		}

		#endregion // Untyped requests

		#region Typed requests

		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values for each request.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		/// <remarks>
		///		Uses the HTTP GET method and no base URI.
		/// </remarks>
		public static void MessageHasUri<TContext>(HttpRequest<TContext> request, TContext context, string expectedUri)
		{
			MessageHasUri(request, HttpMethod.Get, context,
				baseUri: null,
				expectedUri: expectedUri
			);
		}

		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values for each request.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		/// <remarks>
		///		Uses the HTTP GET method and no base URI.
		/// </remarks>
		public static void MessageHasUri<TContext>(HttpRequest<TContext> request, TContext context, Uri expectedUri)
		{
			MessageHasUri(request, HttpMethod.Get, context,
				baseUri: null,
				expectedUri: expectedUri
			);
		}

		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values for each request.
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the request.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		/// <remarks>
		///		Uses the HTTP GET method.
		/// </remarks>
		public static void MessageHasUri<TContext>(HttpRequest<TContext> request, TContext context, Uri baseUri, string expectedUri)
		{
			MessageHasUri(request, HttpMethod.Get, context, baseUri, expectedUri);
		}

		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values for each request.
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the request.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		/// <remarks>
		///		Uses the HTTP GET method.
		/// </remarks>
		public static void MessageHasUri<TContext>(HttpRequest<TContext> request, TContext context, Uri baseUri, Uri expectedUri)
		{
			MessageHasUri(request, HttpMethod.Get, context, baseUri, expectedUri);
		}

		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values for each request.
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the request.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		public static void MessageHasUri<TContext>(HttpRequest<TContext> request, HttpMethod method, TContext context, Uri baseUri, string expectedUri)
		{
			if (String.IsNullOrWhiteSpace(expectedUri))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'expectedUri'.", nameof(expectedUri));

			Message(request, method, context, baseUri, requestMessage =>
			{
				MessageAssert.HasRequestUri(requestMessage, expectedUri);
			});
		}

		/// <summary>
		///		Assert that the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest"/> has the specified URI.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values for each request.
		/// </param>
		/// <param name="baseUri">
		///		The base URI for the request.
		/// </param>
		/// <param name="expectedUri">
		///		The expected URI.
		/// </param>
		public static void MessageHasUri<TContext>(HttpRequest<TContext> request, HttpMethod method, TContext context, Uri baseUri, Uri expectedUri)
		{
			Message(request, method, context, baseUri, requestMessage =>
			{
				MessageAssert.HasRequestUri(requestMessage, expectedUri);
			});
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest{TContext}"/>.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest{TContext}"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		/// <remarks>
		///		Uses the default value for <typeparamref name="TContext"/>.
		/// </remarks>
		public static void Message<TContext>(HttpRequest<TContext> request, HttpMethod method, Action<HttpRequestMessage> assertion)
		{
			Message(request, method,
				context: default(TContext),
				bodyContent: null,
				baseUri: null,
				assertion: assertion
			);
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest{TContext}"/>.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest{TContext}"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="baseUri">
		///		The request base URI.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		/// <remarks>
		///		Uses the default value for <typeparamref name="TContext"/>.
		/// </remarks>
		public static void Message<TContext>(HttpRequest<TContext> request, HttpMethod method, Uri baseUri, Action<HttpRequestMessage> assertion)
		{
			Message(request, method,
				context: default(TContext),
				bodyContent: null,
				baseUri: baseUri,
				assertion: assertion
			);
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest{TContext}"/>.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest{TContext}"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		public static void Message<TContext>(HttpRequest<TContext> request, HttpMethod method, TContext context, Action<HttpRequestMessage> assertion)
		{
			Message(request, method, context,
				bodyContent: null,
				baseUri: null,
				assertion: assertion
			);
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest{TContext}"/>.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest{TContext}"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values.
		/// </param>
		/// <param name="baseUri">
		///		The request base URI.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		public static void Message<TContext>(HttpRequest<TContext> request, HttpMethod method, TContext context, Uri baseUri, Action<HttpRequestMessage> assertion)
		{
			Message(request, method, context,
				bodyContent: null,
				baseUri: baseUri,
				assertion: assertion
			);
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest{TContext}"/>.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest{TContext}"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values.
		/// </param>
		/// <param name="bodyContent">
		///		<see cref="HttpContent"/> representing the request body content.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		public static void Message<TContext>(HttpRequest<TContext> request, HttpMethod method, TContext context, HttpContent bodyContent, Action<HttpRequestMessage> assertion)
		{
			Message(request, method, context, bodyContent,
				baseUri: null,
				assertion: assertion
			);
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest{TContext}"/>.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest{TContext}"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values.
		/// </param>
		/// <param name="bodyContent">
		///		<see cref="HttpContent"/> representing the request body content.
		/// </param>
		/// <param name="baseUri">
		///		An optional base URI for the request.
		/// </param>
		/// <param name="assertion">
		///		A delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		public static void Message<TContext>(HttpRequest<TContext> request, HttpMethod method, TContext context, HttpContent bodyContent, Uri baseUri, Action<HttpRequestMessage> assertion)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (assertion == null)
				throw new ArgumentNullException(nameof(assertion));

			if (method == null)
				throw new ArgumentNullException(nameof(method));

			using (HttpRequestMessage requestMessage = request.BuildRequestMessage(method, context, bodyContent, baseUri))
			{
				assertion(requestMessage);
			}
		}

		/// <summary>
		///		Make assertions about the <see cref="HttpRequestMessage"/> generated by the <see cref="HttpRequest{TContext}"/>.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequest{TContext}"/>.
		/// </param>
		/// <param name="method">
		///		The HTTP method (e.g. GET / POST / PUT).
		/// </param>
		/// <param name="context">
		///		The <typeparamref name="TContext"/> instance used as a context for resolving deferred values.
		/// </param>
		/// <param name="bodyContent">
		///		<see cref="HttpContent"/> representing the request body content.
		/// </param>
		/// <param name="baseUri">
		///		An optional base URI for the request.
		/// </param>
		/// <param name="asyncAssertion">
		///		An asynchronous delegate that makes assertions about the <see cref="HttpRequestMessage"/>.
		/// </param>
		public static async Task MessageAsync<TContext>(HttpRequest<TContext> request, HttpMethod method, TContext context, HttpContent bodyContent, Uri baseUri, Func<HttpRequestMessage, Task> asyncAssertion)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (asyncAssertion == null)
				throw new ArgumentNullException(nameof(asyncAssertion));

			if (method == null)
				throw new ArgumentNullException(nameof(method));

			using (HttpRequestMessage requestMessage = request.BuildRequestMessage(method, context, bodyContent, baseUri))
			{
				await asyncAssertion(requestMessage);
			}
		}

		#endregion // Typed requests
	}
}
