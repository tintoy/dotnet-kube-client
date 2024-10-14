using System;

namespace KubeClient.Utilities
{
    /// <summary>
    ///     Helper methods for working with <see cref="Uri"/>s.
    /// </summary>
    public static class UriHelper
    {
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

            if (uri.IsAbsoluteUri)
                return uri.PathAndQuery;

            // Slightly ugly, but System.Uri doesn't attempt to parse relative URIs so we have to resort to System.UriBuilder.
            UriBuilder uriComponents = new UriBuilder(uri.OriginalString);

            return $"{uriComponents.Path}{uriComponents.Query}";
        }
    }
}
