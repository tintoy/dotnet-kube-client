using System;
using System.Net.Http;

namespace KubeClient.Utilities
{
    using Http;

    /// <summary>
    ///     Helper methods for working with <see cref="HttpRequest"/> and related types.
    /// </summary>
    public static class HttpRequestHelper
    {
        /// <summary>
        ///     Expand an <see cref="HttpRequest"/>'s request URI, populating its URI template if necessary.
        /// </summary>
        /// <param name="request">
        ///     The target <see cref="HttpRequest"/>.
        /// </param>
        /// <param name="baseUri">
        ///     The base URI to use (optional, unless <see cref="HttpRequestBase.Uri"/> is <c>null</c> or not an absolute URI).
        /// </param>
        /// <returns>
        ///     The expanded request URI (always an absolute URI).
        /// </returns>
        public static Uri ExpandRequestUri(this HttpRequest request, Uri baseUri = null)
        {
            using (HttpRequestMessage requestMessage = request.BuildRequestMessage(HttpMethod.Get, body: null, baseUri))
            {
                return requestMessage.RequestUri;
            }
        }

        /// <summary>
        ///     Expand an <see cref="HttpRequest{TContext}"/>'s request URI, populating its URI template if necessary.
        /// </summary>
        /// <typeparam name="TContext">
        ///     The type of object used as a context when evaluating request template parameters.
        /// </typeparam>
        /// <param name="request">
        ///     The target <see cref="HttpRequest{TContext}"/>.
        /// </param>
        /// <param name="context">
        ///     The <typeparamref name="TContext"/> used as a context when evaluating request template parameters.
        /// </param>
        /// <param name="baseUri">
        ///     The base URI to use (optional, unless <see cref="HttpRequestBase.Uri"/> is <c>null</c> or not an absolute URI).
        /// </param>
        /// <returns>
        ///     The expanded request URI (always an absolute URI).
        /// </returns>
        public static Uri ExpandRequestUri<TContext>(this HttpRequest<TContext> request, TContext context, Uri baseUri = null)
        {
            using (HttpRequestMessage requestMessage = request.BuildRequestMessage(HttpMethod.Get, context, body: null, baseUri))
            {
                return requestMessage.RequestUri;
            }
        }
    }
}
