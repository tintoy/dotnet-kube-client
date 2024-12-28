using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KubeClient.Http.Diagnostics
{
    /// <summary>
    ///		Extension methods for <see cref="ILogger"/> used to log messages about requests and responses.
    /// </summary>
    public static class LoggerExtensions
	{
		/// <summary>
		/// 	Log an event representing the start of an HTTP request.
		/// </summary>
		/// <param name="logger">
		///		The logger used to log the event.
		/// </param>
		/// <param name="request">
		///		An <see cref="HttpRequestMessage"/> representing the request.
		/// </param>
		public static void BeginRequest(this ILogger logger, HttpRequestMessage request)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));

			if (request == null)
				throw new ArgumentNullException(nameof(request));

			logger.LogDebug(LogEventIds.BeginRequest, "Performing {Method} request to '{RequestUri}'.",
				request.Method?.Method,
				request.RequestUri
			);
		}

		/// <summary>
		/// 	Asynchronously log an event representing the body of an HTTP request.
		/// </summary>
		/// <param name="logger">
		///		The logger used to log the event.
		/// </param>
		/// <param name="request">
		///		An <see cref="HttpRequestMessage"/> representing the request.
		/// </param>
		/// <returns>
		/// 	A <see cref="Task"/> representing the asynchronous operation.
		/// </returns>
		public static async Task RequestBody(this ILogger logger, HttpRequestMessage request)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));

			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (request.Content == null)
				throw new InvalidOperationException("HttpRequestMessage.Content is null.");

			if (!logger.IsEnabled(LogLevel.Debug))
				return; // Don't bother capturing request body if we won't be able to log it.

			string requestBody = await request.Content.ReadAsStringAsync();

			logger.LogDebug(LogEventIds.RequestBody, "Send request body for {Method} request to '{RequestUri}':\n{RequestBody}",
				request.Method?.Method,
				request.RequestUri,
				requestBody
			);
		}

		/// <summary>
		/// 	Asynchronously log an event representing the body of an HTTP response.
		/// </summary>
		/// <param name="logger">
		///		The logger used to log the event.
		/// </param>
		/// <param name="response">
		///		An <see cref="HttpResponseMessage"/> representing the response.
		/// </param>
		/// <returns>
		/// 	A <see cref="Task"/> representing the asynchronous operation.
		/// </returns>
		public static async Task ResponseBody(this ILogger logger, HttpResponseMessage response)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));

			if (response == null)
				throw new ArgumentNullException(nameof(response));

			if (response.RequestMessage == null)
				throw new InvalidOperationException("HttpResponseMessage.RequestMessage is null."); // Can't examine original request so we don't know if the response is streamed.

			if (response.Content == null)
				throw new InvalidOperationException("HttpResponseMessage.Content is null.");

			if (!logger.IsEnabled(LogLevel.Debug))
				return; // Don't bother capturing response body if we won't be able to log it.

			string responseBody = await response.Content.ReadAsStringAsync();

			logger.LogDebug(LogEventIds.ResponseBody, "Receive response body for {Method} request to '{RequestUri}' ({StatusCode}):\n{Body}",
				response.RequestMessage.Method?.Method,
				response.RequestMessage.RequestUri,
				response.StatusCode,
				responseBody
			);
		}

		/// <summary>
		/// 	Log an event representing the streamed body of an HTTP response.
		/// </summary>
		/// <param name="logger">
		///		The logger used to log the event.
		/// </param>
		/// <param name="response">
		///		An <see cref="HttpResponseMessage"/> representing the response.
		/// </param>
		public static void StreamedResponse(this ILogger logger, HttpResponseMessage response)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));

			if (response == null)
				throw new ArgumentNullException(nameof(response));

			logger.LogDebug(LogEventIds.StreamedResponse, "Receive response body for {Method} request to '{RequestUri}' (response is streamed so body cannot be logged).",
				response.RequestMessage.Method?.Method,
				response.RequestMessage.RequestUri
			);
		}
		
		/// <summary>
		/// 	Log an event representing the completion of an HTTP request.
		/// </summary>
		/// <param name="logger">
		///		The logger used to log the event.
		/// </param>
		/// <param name="request">
		///		An <see cref="HttpRequestMessage"/> representing the request.
		/// </param>
		/// <param name="statusCode">
		///		An <see cref="HttpStatusCode"/> representing the response status code.
		/// </param>
		public static void EndRequest(this ILogger logger, HttpRequestMessage request, HttpStatusCode statusCode)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));

			if (request == null)
				throw new ArgumentNullException(nameof(request));

			logger.LogDebug(LogEventIds.EndRequest, "Completed {Method} request to '{RequestUri}' ({StatusCode}).",
				request.Method?.Method,
				request.RequestUri,
				statusCode
			);
		}

		/// <summary>
		/// 	Log an event representing an error encountered while performing an HTTP request.
		/// </summary>
		/// <param name="logger">
		///		The logger used to log the event.
		/// </param>
		/// <param name="request">
		///		An <see cref="HttpRequestMessage"/> representing the request.
		/// </param>
		/// <param name="error">
		///		An <see cref="Exception"/> representing the error.
		/// </param>
		public static void RequestError(this ILogger logger, HttpRequestMessage request, Exception error)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));

			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (error == null)
				throw new ArgumentNullException(nameof(error));

			logger.LogDebug(LogEventIds.RequestError, error, "{Method} request to '{RequestUri} failed: {ErrorMessage}",
				request.Method?.Method,
				request.RequestUri,
				error.Message
			);
		}
	}
}
