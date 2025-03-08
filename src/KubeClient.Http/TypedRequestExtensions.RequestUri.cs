using System;

namespace KubeClient.Http
{
    using Utilities;

    /// <summary>
    ///		<see cref="HttpRequest{TContext}"/> / <see cref="IHttpRequest{TContext}"/> extension methods for request URIs.
    /// </summary>
    public static partial class TypedRequestExtensions
    {
        /// <summary>
        ///		Create a copy of the request with the specified base URI.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The request.
        /// </param>
        /// <param name="baseUri">
        ///		The request base URI (must be absolute).
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///		The request already has an absolute URI.
        /// </exception>
        public static HttpRequest<TContext> WithBaseUri<TContext>(this HttpRequest<TContext> request, Uri baseUri)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (baseUri == null)
                throw new ArgumentNullException(nameof(baseUri));

            if (!baseUri.IsAbsoluteUri)
                throw new ArgumentException("The supplied base URI is not an absolute URI.", nameof(baseUri));

            if (request.Uri.IsAbsoluteUri)
                throw new InvalidOperationException("The request already has an absolute URI.");

            return request.Clone(properties =>
            {
                properties.SetUri(
                    baseUri.AppendRelativeUri(request.Uri)
                );
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified request URI.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The request.
        /// </param>
        /// <param name="requestUri">
        ///		The new request URI.
        ///
        ///		Must be an absolute URI (otherwise, use <see cref="WithRelativeUri{TContext}(HttpRequest{TContext}, Uri)"/>).
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithUri<TContext>(this HttpRequest<TContext> request, Uri requestUri)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (requestUri == null)
                throw new ArgumentNullException(nameof(requestUri));

            return request.Clone(properties =>
            {
                properties.SetUri(requestUri);
            });
        }

        /// <summary>
        ///		Create a copy of the request with the specified request URI appended to its existing URI.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The request.
        /// </param>
        /// <param name="relativeUri">
        ///		The relative request URI.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithRelativeUri<TContext>(this HttpRequest<TContext> request, string relativeUri)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(relativeUri))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'relativeUri'.", nameof(relativeUri));

            return request.WithRelativeUri(
                new Uri(relativeUri, UriKind.Relative)
            );
        }

        /// <summary>
        ///		Create a copy of the request with the specified request URI appended to its existing URI.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The request.
        /// </param>
        /// <param name="relativeUri">
        ///		The relative request URI.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithRelativeUri<TContext>(this HttpRequest<TContext> request, Uri relativeUri)
        {
            if (relativeUri == null)
                throw new ArgumentNullException(nameof(relativeUri));

            if (relativeUri.IsAbsoluteUri)
                throw new ArgumentException("The specified URI is not a relative URI.", nameof(relativeUri));

            return request.Clone(properties =>
            {
                properties.SetUri(
                    request.Uri.AppendRelativeUri(relativeUri)
                );
            });
        }
    }
}
