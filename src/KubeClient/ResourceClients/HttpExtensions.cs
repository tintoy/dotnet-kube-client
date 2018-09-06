using HTTPlease;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     Extension methods for HTTPlease types.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        ///     Read response content as a <see cref="KubeObjectV1"/>.
        /// </summary>
        /// <param name="response">
        ///     The HTTP response.
        /// </param>
        /// <param name="successStatusCodes">
        ///     Optional <see cref="HttpStatusCode"/>s that should be treated as representing a successful response.
        /// </param>
        /// <returns>
        ///     The response content, as a <see cref="KubeObjectV1"/>.
        /// </returns>
        /// <exception cref="HttpRequestException{TResponse}">
        ///     The response status code was unexpected or did not represent success.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     No formatters were configured for the request, or an appropriate formatter could not be found in the request's list of formatters.
        /// </exception>
        public static async Task<TObject> ReadContentAsObjectV1Async<TObject>(this Task<HttpResponseMessage> response, params HttpStatusCode[] successStatusCodes)
            where TObject : KubeObjectV1
        {
            try
            {
                return await response.ReadContentAsAsync<TObject, StatusV1>(successStatusCodes);
            }
            catch (HttpRequestException<StatusV1> requestError)
            {
                throw new KubeClientException(requestError.Response, requestError);
            }
        }

        /// <summary>
        ///     Read response content as a <see cref="KubeObjectV1"/>.
        /// </summary>
        /// <param name="response">
        ///     The HTTP response.
        /// </param>
        /// <param name="operationDescription">
        ///     A short description of the operation represented by the request (used in exception message if request was not successful).
        /// </param>
        /// <param name="successStatusCodes">
        ///     Optional <see cref="HttpStatusCode"/>s that should be treated as representing a successful response.
        /// </param>
        /// <returns>
        ///     The response content, as a <see cref="KubeObjectV1"/>.
        /// </returns>
        /// <exception cref="HttpRequestException{TResponse}">
        ///     The response status code was unexpected or did not represent success.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     No formatters were configured for the request, or an appropriate formatter could not be found in the request's list of formatters.
        /// </exception>
        public static async Task<TObject> ReadContentAsObjectV1Async<TObject>(this Task<HttpResponseMessage> response, string operationDescription, params HttpStatusCode[] successStatusCodes)
            where TObject : KubeObjectV1
        {
            try
            {
                return await response.ReadContentAsAsync<TObject, StatusV1>(successStatusCodes);
            }
            catch (HttpRequestException<StatusV1> requestError)
            {
                throw new KubeClientException($"Unable to {operationDescription}.", requestError);
            }
        }
    }
}