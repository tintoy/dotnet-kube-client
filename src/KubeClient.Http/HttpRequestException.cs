using System.Net;
using System.Net.Http;

namespace KubeClient.Http
{
	/// <summary>
	///		Exception thrown when an error response is received while making an HTTP request.
	/// </summary>
	/// <remarks>
	///		TODO: Throw this from response.ReadContentAsAsync&lt;TResponse, TErrorResponse&gt;.
	/// </remarks>
	public class HttpRequestException<TResponse>
		: HttpRequestException
	{
		/// <summary>
		///		Create a new <see cref="HttpRequestException{TResponse}"/>.
		/// </summary>
		/// <param name="statusCode">
		///		The response's HTTP status code.
		///	</param>
		/// <param name="response">
		///		The response body.
		///	</param>
		public HttpRequestException(HttpStatusCode statusCode, TResponse response)
			: this(statusCode, response, $"The request failed with unexpected status code '{statusCode}'.")
		{
		}

#if NETSTANDARD2_1
		/// <summary>
		///		Create a new <see cref="HttpRequestException{TResponse}"/>.
		/// </summary>
		/// <param name="statusCode">
		///		The response's HTTP status code.
		///	</param>
		/// <param name="response">
		///		The response body.
		///	</param>
		/// <param name="message">
		///		The exception message.
		///	</param>
		public HttpRequestException(HttpStatusCode statusCode, TResponse response, string message)
			: base(message)
		{
			StatusCode = statusCode;
			Response = response;
		}
#else // NETSTANDARD2_1
        /// <summary>
		///		Create a new <see cref="HttpRequestException{TResponse}"/>.
		/// </summary>
		/// <param name="statusCode">
		///		The response's HTTP status code.
		///	</param>
		/// <param name="response">
		///		The response body.
		///	</param>
		/// <param name="message">
		///		The exception message.
		///	</param>
		public HttpRequestException(HttpStatusCode statusCode, TResponse response, string message)
            : base(message, inner: null, statusCode)
        {
            Response = response;
        }
#endif // NETSTANDARD2_1

#if NETSTANDARD2_1
        /// <summary>
		///		The response's HTTP status code.
		/// </summary>
		public HttpStatusCode StatusCode { get; }
#endif // NETSTANDARD2_1

        /// <summary>
        ///		The response body.
        /// </summary>
        public TResponse Response { get; }

		/// <summary>
		/// 	Create a new <see cref="HttpRequestException{TRespose}"/>.
		/// </summary>
		/// <param name="statusCode">
		///		The HTTP response status code.
		/// </param>
		/// <param name="response">
		///		A <typeparamref name="TResponse"/> representing the HTTP response body.
		/// </param>
		/// <returns>
		///		The configured <see cref="HttpRequestException{TRespose}"/>.
		/// </returns>
		public static HttpRequestException<TResponse> Create(HttpStatusCode statusCode, TResponse response)
		{
			string message = $"HTTP request failed ({statusCode}).";

			IHttpErrorResponse errorResponse = response as IHttpErrorResponse;
			if (errorResponse != null)
				message = errorResponse.GetExceptionMesage();

			return new HttpRequestException<TResponse>(statusCode, response, message);
		}
	}
}
