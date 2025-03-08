using System;

namespace KubeClient.Http
{
	/// <summary>
	///		A facility for creating <see cref="HttpRequest"/>s.
	/// </summary>
	public sealed class HttpRequestFactory
    {
	    /// <summary>
		///		Create a new <see cref="HttpRequestFactory"/>.
		/// </summary>
		/// <param name="baseRequest">
		///		The <see cref="HttpRequest"/> used as a base for requests created by the factory.
		/// </param>
	    public HttpRequestFactory(HttpRequest baseRequest)
	    {
		    if (baseRequest == null)
			    throw new ArgumentNullException(nameof(baseRequest));

		    BaseRequest = baseRequest;
	    }

		/// <summary>
		///		The <see cref="HttpRequest"/> used as a base for requests created by the factory.
		/// </summary>
		public HttpRequest BaseRequest { get; }

		/// <summary>
		///		Create a new <see cref="HttpRequest"/> with the specified request URI.
		/// </summary>
		/// <param name="requestUri">
		///		The request URI.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest"/>.
		/// </returns>
		public HttpRequest Create(Uri requestUri)
		{
			if (requestUri == null)
				throw new ArgumentNullException(nameof(requestUri));

			return BaseRequest.WithUri(requestUri);
		}
    }

	/// <summary>
	///		A facility for creating <see cref="HttpRequest{TContext}"/>s.
	/// </summary>
	/// <typeparam name="TContext">
	///		The type of object used as a context for resolving deferred parameters.
	/// </typeparam>
	public sealed class HttpRequestFactory<TContext>
	{
		/// <summary>
		///		Create a new <see cref="HttpRequestFactory"/>.
		/// </summary>
		/// <param name="baseRequest">
		///		The <see cref="HttpRequest{TContext}"/> used as a base for requests created by the factory.
		/// </param>
		public HttpRequestFactory(HttpRequest<TContext> baseRequest)
		{
			if (baseRequest == null)
				throw new ArgumentNullException(nameof(baseRequest));

			BaseRequest = baseRequest;
		}

		/// <summary>
		///		The <see cref="HttpRequest{TContext}"/> used as a base for requests created by the factory.
		/// </summary>
		public HttpRequest<TContext> BaseRequest { get; }

		/// <summary>
		///		Create a new <see cref="HttpRequest{TContext}"/> with the specified request URI.
		/// </summary>
		/// <param name="requestUri">
		///		The request URI.
		/// </param>
		/// <returns>
		///		The new <see cref="HttpRequest{TContext}"/>.
		/// </returns>
		public HttpRequest<TContext> Create(Uri requestUri)
		{
			if (requestUri == null)
				throw new ArgumentNullException(nameof(requestUri));

			return BaseRequest.WithUri(requestUri);
		}
	}
}
