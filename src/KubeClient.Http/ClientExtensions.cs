using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Http
{
    /// <summary>
    ///		Invocation-related extension methods for <see cref="HttpClient"/>s that use an <see cref="HttpRequest"/>.
    /// </summary>
    public static partial class ClientExtensions
    {
		#region Invoke

		/// <summary>
		///		Asynchronously execute a request as an HTTP HEAD.
		/// </summary>
		/// <param name="httpClient">
		///		The <see cref="HttpClient"/> used to execute the request.
		/// </param>
		/// <param name="request">
		///		The HTTP request.
		/// </param>
		/// <param name="cancellationToken">
		///		An optional cancellation token that can be used to cancel the asynchronous operation.
		/// </param>
		/// <returns>
		///		An <see cref="HttpResponseMessage"/> representing the response.
		/// </returns>
		public static async Task<HttpResponseMessage> HeadAsync(this HttpClient httpClient, HttpRequest request, CancellationToken cancellationToken = default)
		{
			if (httpClient == null)
				throw new ArgumentNullException(nameof(httpClient));

			if (request == null)
				throw new ArgumentNullException(nameof(request));

			return await httpClient.SendAsync(request, HttpMethod.Head, cancellationToken: cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Asynchronously execute a request as an HTTP GET.
		/// </summary>
		/// <param name="httpClient">
		///		The <see cref="HttpClient"/> used to execute the request.
		/// </param>
		/// <param name="request">
		///		The HTTP request.
		/// </param>
		/// <param name="cancellationToken">
		///		An optional cancellation token that can be used to cancel the asynchronous operation.
		/// </param>
		/// <returns>
		///		An <see cref="HttpResponseMessage"/> representing the response.
		/// </returns>
		public static async Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, HttpRequest request, CancellationToken cancellationToken = default)
		{
			if (httpClient == null)
				throw new ArgumentNullException(nameof(httpClient));

			if (request == null)
				throw new ArgumentNullException(nameof(request));

			return await httpClient.SendAsync(request, HttpMethod.Get, cancellationToken: cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Asynchronously execute a request as an HTTP POST.
		/// </summary>
		/// <param name="httpClient">
		///		The <see cref="HttpClient"/> used to execute the request.
		/// </param>
		/// <param name="request">
		///		The HTTP request.
		/// </param>
		/// <param name="postBody">
		///		Optional <see cref="HttpContent"/> representing the request body.
		/// </param>
		/// <param name="cancellationToken">
		///		An optional cancellation token that can be used to cancel the asynchronous operation.
		/// </param>
		/// <returns>
		///		An <see cref="HttpResponseMessage"/> representing the response.
		/// </returns>
		public static async Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, HttpRequest request, HttpContent postBody = null, CancellationToken cancellationToken = default)
		{
			if (httpClient == null)
				throw new ArgumentNullException(nameof(httpClient));

			if (request == null)
				throw new ArgumentNullException(nameof(request));

			return await httpClient.SendAsync(request, HttpMethod.Post, postBody, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Asynchronously execute a request as an HTTP PUT.
		/// </summary>
		/// <param name="httpClient">
		///		The <see cref="HttpClient"/> used to execute the request.
		/// </param>
		/// <param name="request">
		///		The HTTP request.
		/// </param>
		/// <param name="putBody">
		///		<see cref="HttpContent"/> representing the request body.
		/// </param>
		/// <param name="cancellationToken">
		///		An optional cancellation token that can be used to cancel the asynchronous operation.
		/// </param>
		/// <returns>
		///		An <see cref="HttpResponseMessage"/> representing the response.
		/// </returns>
		public static async Task<HttpResponseMessage> PutAsync(this HttpClient httpClient, HttpRequest request, HttpContent putBody, CancellationToken cancellationToken = default)
		{
			if (httpClient == null)
				throw new ArgumentNullException(nameof(httpClient));

			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (putBody == null)
				throw new ArgumentNullException(nameof(putBody));

			return await httpClient.SendAsync(request, HttpMethod.Put, putBody, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Asynchronously execute a request as an HTTP PATCH.
		/// </summary>
		/// <param name="httpClient">
		///		The <see cref="HttpClient"/> used to execute the request.
		/// </param>
		/// <param name="request">
		///		The HTTP request.
		/// </param>
		/// <param name="patchBody">
		///		<see cref="HttpContent"/> representing the request body.
		/// </param>
		/// <param name="cancellationToken">
		///		An optional cancellation token that can be used to cancel the asynchronous operation.
		/// </param>
		/// <returns>
		///		An <see cref="HttpResponseMessage"/> representing the response.
		/// </returns>
		public static async Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, HttpRequest request, HttpContent patchBody, CancellationToken cancellationToken = default)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (patchBody == null)
				throw new ArgumentNullException(nameof(patchBody));

			return await httpClient.SendAsync(request, OtherHttpMethods.Patch, patchBody, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Asynchronously execute a request as an HTTP DELETE.
		/// </summary>
		/// <param name="httpClient">
		///		The <see cref="HttpClient"/> used to execute the request.
		/// </param>
		/// <param name="request">
		///		The HTTP request.
		/// </param>
		/// <param name="cancellationToken">
		///		An optional cancellation token that can be used to cancel the asynchronous operation.
		/// </param>
		/// <returns>
		///		An <see cref="HttpResponseMessage"/> representing the response.
		/// </returns>
		public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, HttpRequest request, CancellationToken cancellationToken = default)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			return await httpClient.SendAsync(request, HttpMethod.Delete, cancellationToken: cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Asynchronously execute the request using the specified HTTP method.
		/// </summary>
		/// <param name="httpClient">
		///		The <see cref="HttpClient"/> used to execute the request.
		/// </param>
		/// <param name="request">
		///		The HTTP request.
		/// </param>
		/// <param name="method">
		///		An <see cref="HttpMethod"/> representing the method to use.
		/// </param>
		/// <param name="body">
		///		Optional <see cref="HttpContent"/> representing the request body (if any).
		/// </param>
		/// <param name="cancellationToken">
		///		An optional cancellation token that can be used to cancel the asynchronous operation.
		/// </param>
		/// <returns>
		///		An <see cref="HttpResponseMessage"/> representing the response.
		/// </returns>
		public static async Task<HttpResponseMessage> SendAsync(this HttpClient httpClient, HttpRequest request, HttpMethod method, HttpContent body = null, CancellationToken cancellationToken = default)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			using (HttpRequestMessage requestMessage = request.BuildRequestMessage(method, body, httpClient.BaseAddress))
			{
				HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
				try
				{
					request.ExecuteResponseActions(responseMessage);
				}
				catch
				{
					using (responseMessage)
					{
						throw;
					}
				}

				return responseMessage;
			}
		}

		#endregion // Invoke

		#region Helpers

		/// <summary>
		///		Execute the request's configured response actions (if any) against the specified response message.
		/// </summary>
		/// <param name="request">
		///		The <see cref="HttpRequest"/>.
		/// </param>
		/// <param name="responseMessage">
		///		The HTTP response message.
		/// </param>
		static void ExecuteResponseActions(this HttpRequest request, HttpResponseMessage responseMessage)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (responseMessage == null)
				throw new ArgumentNullException(nameof(responseMessage));

			List<Exception> responseActionExceptions = new List<Exception>();
			foreach (ResponseAction<object> responseAction in request.ResponseActions)
			{
				try
				{
					responseAction(responseMessage, HttpRequest.DefaultContext);
				}
				catch (Exception eResponseAction)
				{
					responseActionExceptions.Add(eResponseAction);
				}
			}

			if (responseActionExceptions.Count > 0)
				throw new AggregateException("One or more errors occurred while processing the response message.", responseActionExceptions);
		}

		#endregion // Helpers
	}
}
