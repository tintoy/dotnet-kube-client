using System;
using System.Net;
using System.Net.Http;

namespace KubeClient.Tests.Http.Formatters
{
    using KubeClient.Http.Formatters;
    using KubeClient.Http.Testability;

	/// <summary>
	/// 	Formatter-related extension methods for <see cref="HttpRequestMessage"/> / <see cref="HttpResponseMessage"/>.
	/// </summary>
	public static class MessageExtensions
	{
		/// <summary>
		/// 	Create a <see cref="HttpResponseMessage">response message</see>.
		/// </summary>
		/// <typeparam name="TBody">
		///		The type of object that represents the response body.
		/// </typeparam>
		/// <param name="request">
		///		The <see cref="HttpRequestMessage">response message</see>.
		/// </param>
		/// <param name="statusCode">
		///		The response status code.
		/// </param>
		/// <param name="body">
		///		The response body.
		/// </param>
		/// <param name="mediaType">
		///		The target media type for the response body.
		/// </param>
		/// <param name="formatter">
		///		The content formatter that will serialise the response body.
		/// </param>
		/// <returns>
		///		The configured response message.
		/// </returns>
		public static HttpResponseMessage CreateResponse<TBody>(this HttpRequestMessage request, HttpStatusCode statusCode, TBody body, string mediaType, IOutputFormatter formatter)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (String.IsNullOrWhiteSpace(mediaType))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'mediaType'.", nameof(mediaType));

			HttpResponseMessage response = request.CreateResponse(statusCode);
			try
			{
				response.Content = new FormattedObjectContent(
					formatter,
					typeof(TBody),
					body,
					mediaType
				);
			}
			catch
			{
				using (response)
				{
					throw;
				}
			}

			return response;
		}
	}
}
