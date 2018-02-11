using HTTPlease;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace KubeClient
{
    /// <summary>
    ///     Extensions for streaming from <see cref="HttpClient"/>.
    /// </summary>
    static class HttpClientStreamedExtensions
    {
        /// <summary>
        ///     Asynchronously execute a request as a streamed HTTP GET.
        /// </summary>
        /// <param name="httpClient">
        ///     The HTTP client.
        /// </param>
        /// <param name="request">
        ///     The <see cref="HttpRequest"/> to execute.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     An <see cref="HttpResponseMessage"/> representing the response.
        /// </returns>
        public static async Task<HttpResponseMessage> GetStreamedAsync(this HttpClient httpClient, HttpRequest request, CancellationToken cancellationToken = default)
        {
            using (HttpRequestMessage requestMessage = request.BuildRequestMessage(HttpMethod.Get, baseUri: httpClient.BaseAddress))
            {
                return await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
            }
        }
    }
}
