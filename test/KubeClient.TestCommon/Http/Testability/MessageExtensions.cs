using System;
using System.Net;
using System.Net.Http;

namespace KubeClient.Http.Testability
{
    using KubeClient.Http;
    using KubeClient.Http.Formatters;

    /// <summary>
    /// 	Extension methods for <see cref="HttpRequestMessage"/> / <see cref="HttpResponseMessage"/>.
    /// </summary>
    public static class MessageExtensions
    {
        /// <summary>
        /// 	Create a <see cref="HttpResponseMessage">response message</see> with an <see cref="HttpStatusCode.OK"/> status code.
        /// </summary>
        /// <param name="request">
        ///		The <see cref="HttpRequestMessage">response message</see>.
        /// </param>
        /// <returns>
        ///		The configured response message.
        /// </returns>
        public static HttpResponseMessage CreateResponse(this HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// 	Create a <see cref="HttpResponseMessage">response message</see>.
        /// </summary>
        /// <param name="request">
        ///		The <see cref="HttpRequestMessage">response message</see>.
        /// </param>
        /// <param name="statusCode">
        ///		The response status code.
        /// </param>
        /// <returns>
        ///		The configured response message.
        /// </returns>
        public static HttpResponseMessage CreateResponse(this HttpRequestMessage request, HttpStatusCode statusCode)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            HttpResponseMessage response = new HttpResponseMessage(statusCode);
            try
            {
                response.RequestMessage = request;
            }
            catch
            {
                using (response)
                {
                    throw;
                }
            }

            return response;
        }

        /// <summary>
        /// 	Create a <see cref="HttpResponseMessage">response message</see>.
        /// </summary>
        /// <param name="request">
        ///		The <see cref="HttpRequestMessage">response message</see>.
        /// </param>
        /// <param name="statusCode">
        ///		The response status code.
        /// </param>
        /// <param name="responseBody">
        ///		The response body (media type will be "text/plain").
        /// </param>
        /// <returns>
        ///		The configured response message.
        /// </returns>
        public static HttpResponseMessage CreateResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string responseBody)
        {
            return request.CreateResponse(statusCode, responseBody, WellKnownMediaTypes.PlainText);
        }

        /// <summary>
        /// 	Create a <see cref="HttpResponseMessage">response message</see>.
        /// </summary>
        /// <param name="request">
        ///		The <see cref="HttpRequestMessage">response message</see>.
        /// </param>
        /// <param name="statusCode">
        ///		The response status code.
        /// </param>
        /// <param name="responseBody">
        ///		The response body.
        /// </param>
        /// <param name="mediaType">
        ///		The response media type.
        /// </param>
        /// <returns>
        ///		The configured response message.
        /// </returns>
        public static HttpResponseMessage CreateResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string responseBody, string mediaType)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (responseBody != null && String.IsNullOrWhiteSpace(mediaType))
                throw new ArgumentException("Must specify a valid media type if specifying a response body.", nameof(mediaType));

            HttpResponseMessage response = request.CreateResponse(statusCode);
            if (responseBody == null)
                return response;

            try
            {
                response.Content = new StringContent(responseBody, OutputEncoding.UTF8, mediaType);
            }
            catch
            {
                using (response)
                {
                    throw;
                }
            }

            return response;
        }
    }
}
