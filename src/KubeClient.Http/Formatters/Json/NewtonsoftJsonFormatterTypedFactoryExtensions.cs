using System;

namespace KubeClient.Http.Formatters.Json
{
	/// <summary>
	///		JSON request extension methods for <see cref="HttpRequestFactory{TContext}"/>.
	/// </summary>
	public static class NewtonsoftJsonFormatterTypedFactoryExtensions
    {
		/// <summary>
		///		Create a new HTTP request that expects and uses JSON as its primary format.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="requestFactory">
		///		The HTTP request factory.
		/// </param>
		/// <param name="requestUri">
		///		The request URI (can be relative or absolute).
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest"/>.
		/// </returns>
		public static HttpRequest<TContext> Json<TContext>(this HttpRequestFactory<TContext> requestFactory, string requestUri)
		{
			if (requestFactory == null)
				throw new ArgumentNullException(nameof(requestFactory));

			if (String.IsNullOrWhiteSpace(requestUri))
				throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'requestUri'.", nameof(requestUri));

			return
				requestFactory.Create(requestUri)
					.ExpectJson()
					.UseJson();
		}

		/// <summary>
		///		Create a new HTTP request that expects and uses JSON as its primary format.
		/// </summary>
		/// <typeparam name="TContext">
		///		The type of object used as a context for resolving deferred parameters.
		/// </typeparam>
		/// <param name="requestFactory">
		///		The HTTP request factory.
		/// </param>
		/// <param name="requestUri">
		///		The request URI (can be relative or absolute).
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest"/>.
		/// </returns>
		public static HttpRequest<TContext> Json<TContext>(this HttpRequestFactory<TContext> requestFactory, Uri requestUri)
		{
			if (requestFactory == null)
				throw new ArgumentNullException(nameof(requestFactory));

			if (requestUri == null)
				throw new ArgumentNullException(nameof(requestUri));

			return
				requestFactory.Create(requestUri)
					.ExpectJson()
					.UseJson();
		}
	}
}
