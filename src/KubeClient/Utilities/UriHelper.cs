using System;

namespace KubeClient.Utilities
{
    /// <summary>
    ///     Helper methods for working with <see cref="Uri"/>s.
    /// </summary>
    public static class UriHelper
    {
        /// <summary>
        ///     A dummy URI to be used as the base URI when dealing with relative URIs.
        /// </summary>
        static readonly Uri DummyBaseUri = new Uri("https://dummy-host");

        /// <summary>
        ///     Get the path (and, if present, the query) of a URI.
        /// </summary>
        /// <param name="uri">
        ///     The target <see cref="Uri"/>.
        /// </param>
        /// <returns>
        ///     The URI's path and query.
        /// </returns>
        /// <remarks>
        ///     Unlike <see cref="Uri.PathAndQuery"/>, also handles relative URIs.
        /// </remarks>
        public static string SafeGetPathAndQuery(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return uri.EnsureAbsolute().PathAndQuery;
        }

        /// <summary>
        ///     Ensure that the URI is an absolute URI.
        /// </summary>
        /// <param name="uri">
        ///     The target URI.
        /// </param>
        /// <returns>
        ///     The target URI, if <see cref="Uri.IsAbsoluteUri"/> is <c>true</c>; otherwise, an absolute URI using <see cref="DummyBaseUri"/> as the base URI.
        /// </returns>
        static Uri EnsureAbsolute(this Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            if (uri.IsAbsoluteUri)
                return uri;

            // Slightly ugly, but System.Uri doesn't attempt to parse relative URIs so we have to convert it to an absolute URI.
            Uri absoluteUri = new Uri(DummyBaseUri, relativeUri: uri);

            return absoluteUri;
        }
    }
}
