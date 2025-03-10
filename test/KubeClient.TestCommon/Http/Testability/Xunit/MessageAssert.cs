﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace KubeClient.Http.Testability.Xunit
{
    /// <summary>
    ///		Assertion functionality for HTTP request / response message generated by <see cref="HttpRequest"/> / <see cref="HttpRequest{TContext}"/>.
    /// </summary>
    public static class MessageAssert
    {
        /// <summary>
        ///		Assert that the request message has the specified URI.
        /// </summary>
        /// <param name="requestMessage">
        ///		The <see cref="HttpRequestMessage"/>.
        /// </param>
        /// <param name="expectedUri">
        ///		The expected URI.
        /// </param>
        public static void HasRequestUri(HttpRequestMessage requestMessage, string expectedUri)
        {
            if (requestMessage == null)
                throw new ArgumentNullException(nameof(requestMessage));

            if (String.IsNullOrWhiteSpace(expectedUri))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'expectedUri'.", nameof(expectedUri));

            HasRequestUri(requestMessage,
                new Uri(expectedUri, UriKind.RelativeOrAbsolute)
            );
        }

        /// <summary>
        ///		Assert that the request message has the specified URI.
        /// </summary>
        /// <param name="requestMessage">
        ///		The <see cref="HttpRequestMessage"/>.
        /// </param>
        /// <param name="expectedUri">
        ///		The expected URI.
        /// </param>
        public static void HasRequestUri(HttpRequestMessage requestMessage, Uri expectedUri)
        {
            if (requestMessage == null)
                throw new ArgumentNullException(nameof(requestMessage));

            if (requestMessage == null)
                throw new ArgumentNullException(nameof(requestMessage));

            Assert.Equal(expectedUri,
                requestMessage.RequestUri
            );
        }

        /// <summary>
        ///		Assert that the request message's Accept header contains the specified media type.
        /// </summary>
        /// <param name="requestMessage">
        ///		The <see cref="HttpRequestMessage"/>.
        /// </param>
        /// <param name="mediaType">
        ///		The expected media type.
        /// </param>
        public static void AcceptsMediaType(HttpRequestMessage requestMessage, string mediaType)
        {
            if (requestMessage == null)
                throw new ArgumentNullException(nameof(requestMessage));

            if (String.IsNullOrWhiteSpace(mediaType))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'mediaType'.", nameof(mediaType));

            Assert.Contains(requestMessage.Headers.Accept,
                accept => accept.MediaType == mediaType
            );
        }

        /// <summary>
        ///		Asynchronously assert that the request message body is equal to the specified string.
        /// </summary>
        /// <param name="requestMessage">
        ///		The HTTP request message to examine.
        /// </param>
        /// <param name="expectedBody">
        ///		A string containing the expected message body.
        /// </param>
        /// <returns>
        ///		The actual message body.
        /// </returns>
        public static async Task<string> BodyIsAsync(HttpRequestMessage requestMessage, string expectedBody)
        {
            if (requestMessage == null)
                throw new ArgumentNullException(nameof(requestMessage));

            string actualBody = null;
            if (requestMessage.Content != null)
                actualBody = await requestMessage.Content.ReadAsStringAsync();

            Assert.Equal(expectedBody, actualBody);

            return actualBody;
        }
    }
}
