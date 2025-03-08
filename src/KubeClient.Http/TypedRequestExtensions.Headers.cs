using System;
using System.Net.Http.Headers;

namespace KubeClient.Http
{
    using ValueProviders;

    /// <summary>
    ///		<see cref="HttpRequest{TContext}"/> / <see cref="IHttpRequest{TContext}"/> extension methods for HTTP headers.
    /// </summary>
    public static partial class TypedRequestExtensions
    {
        /// <summary>
        ///		Create a copy of the request that adds a header to each request.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///		The header value data-type.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="headerName">
        ///		The header name.
        /// </param>
        /// <param name="headerValue">
        ///		The header value.
        /// </param>
        /// <param name="ensureQuoted">
        ///		Ensure that the header value is quoted?
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithHeader<TContext, TValue>(this HttpRequest<TContext> request, string headerName, TValue headerValue, bool ensureQuoted = false)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(headerName))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(headerName));

            if (headerValue == null)
                throw new ArgumentNullException(nameof(headerValue));

            return request.WithHeaderFromProvider(headerName,
                ValueProvider<TContext>.FromConstantValue(headerValue).Convert().ValueToString(),
                ensureQuoted
            );
        }

        /// <summary>
        ///		Create a copy of the request that adds a header with its value obtained from the specified delegate.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///		The type of header value to add.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="headerName">
        ///		The header name.
        /// </param>
        /// <param name="getValue">
        ///		A delegate that returns the header value for each request.
        /// </param>
        /// <param name="ensureQuoted">
        ///		Ensure that the header value is quoted?
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithHeader<TContext, TValue>(this HttpRequest<TContext> request, string headerName, Func<TValue> getValue, bool ensureQuoted = false)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(headerName))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(headerName));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return request.WithHeaderFromProvider(headerName,
                ValueProvider<TContext>.FromFunction(getValue).Convert().ValueToString(),
                ensureQuoted
            );
        }

        /// <summary>
        ///		Create a copy of the request that adds a header with its value obtained from the specified delegate.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///		The type of header value to add.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="headerName">
        ///		The header name.
        /// </param>
        /// <param name="getValue">
        ///		A delegate that extracts the header value from the context for each request.
        /// </param>
        /// <param name="ensureQuoted">
        ///		Ensure that the header value is quoted?
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithHeader<TContext, TValue>(this HttpRequest<TContext> request, string headerName, Func<TContext, TValue> getValue, bool ensureQuoted = false)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(headerName))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(headerName));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return request.WithHeaderFromProvider(headerName,
                ValueProvider<TContext>.FromSelector(getValue).Convert().ValueToString(),
                ensureQuoted
            );
        }

        /// <summary>
        ///		Create a copy of the request, but with the specified media type added to the "Accept" header.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="mediaType">
        ///		The media-type name.
        /// </param>
        /// <param name="quality">
        ///		An optional media-type quality.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest<TContext> AcceptMediaType<TContext>(this HttpRequest<TContext> request, string mediaType, double? quality = null)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(mediaType))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'mediaType'.", nameof(mediaType));

            MediaTypeWithQualityHeaderValue mediaTypeHeader =
                quality.HasValue ?
                    new MediaTypeWithQualityHeaderValue(mediaType, quality.Value)
                    :
                    new MediaTypeWithQualityHeaderValue(mediaType);

            return request.WithRequestAction(requestMessage =>
            {
                requestMessage.Headers.Accept.Add(mediaTypeHeader);
            });
        }

        /// <summary>
        ///		Create a copy of the request, but with no media types in the "Accept" header.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest"/>.
        /// </returns>
        public static HttpRequest<TContext> AcceptNoMediaTypes<TContext>(this HttpRequest<TContext> request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return request.WithRequestAction(requestMessage =>
            {
                requestMessage.Headers.Accept.Clear();
            });
        }

        /// <summary>
        ///		Create a copy of the request that adds an "If-Match" header to each request.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="headerValue">
        ///		The header value.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithIfMatchHeader<TContext>(this HttpRequest<TContext> request, string headerValue)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (headerValue == null)
                throw new ArgumentNullException(nameof(headerValue));

            return request.WithHeader("If-Match", () => headerValue, ensureQuoted: true);
        }

        /// <summary>
        ///		Create a copy of the request that adds an "If-Match" header with its value obtained from the specified delegate.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="getValue">
        ///		A delegate that extracts the header value from the context for each request.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithIfMatchHeader<TContext>(this HttpRequest<TContext> request, Func<TContext, string> getValue)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return request.WithHeader("If-Match", getValue, ensureQuoted: true);
        }

        /// <summary>
        ///		Create a copy of the request that adds an "If-Match" header with its value obtained from the specified delegate.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="getValue">
        ///		A delegate that returns the header value for each request.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithIfMatchHeader<TContext>(this HttpRequest<TContext> request, Func<string> getValue)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return request.WithHeader("If-Match", getValue, ensureQuoted: true);
        }

        /// <summary>
        ///		Create a copy of the request that adds an "If-None-Match" header to each request.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="headerValue">
        ///		The header value.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithIfNoneMatchHeader<TContext>(this HttpRequest<TContext> request, string headerValue)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (headerValue == null)
                throw new ArgumentNullException(nameof(headerValue));

            return request.WithHeader("If-None-Match", () => headerValue, ensureQuoted: true);
        }

        /// <summary>
        ///		Create a copy of the request that adds an "If-None-Match" header with its value obtained from the specified delegate.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="getValue">
        ///		A delegate that extracts the header value from the context for each request.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithIfNoneMatchHeader<TContext>(this HttpRequest<TContext> request, Func<TContext, string> getValue)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return request.WithHeader("If-None-Match", getValue, ensureQuoted: true);
        }

        /// <summary>
        ///		Create a copy of the request that adds an "If-None-Match" header with its value obtained from the specified delegate.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="getValue">
        ///		A delegate that returns the header value for each request.
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithIfNoneMatchHeader<TContext>(this HttpRequest<TContext> request, Func<string> getValue)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));

            return request.WithHeader("If-None-Match", getValue, ensureQuoted: true);
        }

        /// <summary>
        ///		Create a copy of the request that adds a header to each request.
        /// </summary>
        /// <typeparam name="TContext">
        ///		The type of object used as a context for resolving deferred parameters.
        /// </typeparam>
        /// <param name="request">
        ///		The HTTP request.
        /// </param>
        /// <param name="headerName">
        ///		The header name.
        /// </param>
        /// <param name="valueProvider">
        ///		The header value provider.
        /// </param>
        /// <param name="ensureQuoted">
        ///		Ensure that the header value is quoted?
        /// </param>
        /// <returns>
        ///		The new <see cref="HttpRequest{TContext}"/>.
        /// </returns>
        public static HttpRequest<TContext> WithHeaderFromProvider<TContext>(this HttpRequest<TContext> request, string headerName, IValueProvider<TContext, string> valueProvider, bool ensureQuoted = false)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (String.IsNullOrWhiteSpace(headerName))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'name'.", nameof(headerName));

            if (valueProvider == null)
                throw new ArgumentNullException(nameof(valueProvider));

            return request.WithRequestAction((requestMessage, context) =>
            {
                requestMessage.Headers.Remove(headerName);

                string headerValue = valueProvider.Get(context);
                if (headerValue == null)
                    return;

                if (ensureQuoted)
                    headerValue = EnsureQuoted(headerValue);

                requestMessage.Headers.Add(headerName, headerValue);
            });
        }
    }
}
