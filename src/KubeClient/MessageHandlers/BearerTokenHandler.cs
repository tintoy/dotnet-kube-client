using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.MessageHandlers
{
    /// <summary>
    ///     HTTP message handler that adds a bearer token to outgoing requests.
    /// </summary>
    public class BearerTokenHandler
        : DelegatingHandler
    {
        /// <summary>
        ///     The bearer token added to outgoing requests.
        /// </summary>
        readonly string _token;

        /// <summary>
        ///     Create a new <see cref="BearerTokenHandler"/>.
        /// </summary>
        /// <param name="token">
        ///     The bearer token added to outgoing requests.
        /// </param>
        public BearerTokenHandler(string token)
        {
            if (String.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'token'.", nameof(token));
            
            _token = token;
        }

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
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            request.Headers.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: _token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}