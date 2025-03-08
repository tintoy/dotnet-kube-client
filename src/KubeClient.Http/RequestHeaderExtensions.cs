using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace KubeClient.Http
{
    /// <summary>
    ///		Extension method for <see cref="HttpRequestHeaders"/>
    /// </summary>
    public static class RequestHeaderExtensions
    {
        /// <summary>
        ///		Retrieve the value of an optional HTTP request header.
        /// </summary>
        /// <param name="requestHeaders">
        ///		The HTTP request headers to examine.
        /// </param>
        /// <param name="headerName">
        ///		The name of the target header.
        /// </param>
        /// <returns>
        ///		The header value, or <c>null</c> if the header is not present (or an <see cref="string.Empty"/> string if the header is present but has no value).
        /// </returns>
        public static string GetOptionalHeaderValue(this HttpRequestHeaders requestHeaders, string headerName)
        {
            if (requestHeaders == null)
                throw new ArgumentNullException(nameof(requestHeaders));

            if (String.IsNullOrWhiteSpace(headerName))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'headerName'.", nameof(headerName));

            IEnumerable<string> headerValues;
            if (!requestHeaders.TryGetValues(headerName, out headerValues))
                return null;

            return
                headerValues.DefaultIfEmpty(
                    String.Empty
                )
                .FirstOrDefault();
        }
    }
}
