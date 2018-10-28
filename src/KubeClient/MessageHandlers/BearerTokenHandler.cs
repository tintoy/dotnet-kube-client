using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.MessageHandlers
{
    /// <summary>
    ///     The base class for HTTP message handlers that add a bearer token to outgoing requests.
    /// </summary>
    public abstract class BearerTokenHandler
        : DelegatingHandler
    {
        /// <summary>
        ///     Create a new <see cref="BearerTokenHandler"/>.
        /// </summary>
        protected BearerTokenHandler()
        {
        }

        /// <summary>
        ///     Obtain a bearer token to use for authentication.
        /// </summary>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     The access token.
        /// </returns>
        protected abstract Task<string> GetTokenAsync(CancellationToken cancellationToken);

        /// <summary>
        ///     Asynchronously process an HTTP request.
        /// </summary>
        /// <param name="request">
        ///     The outgoing request message.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The incoming response message.
        /// </returns>
        protected sealed override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            string token = await GetTokenAsync(cancellationToken).ConfigureAwait(false);

            request.Headers.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}