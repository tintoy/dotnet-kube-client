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
    public class StaticBearerTokenHandler
        : BearerTokenHandler
    {
        /// <summary>
        ///     The bearer token added to outgoing requests.
        /// </summary>
        readonly string _token;

        /// <summary>
        ///     Create a new <see cref="StaticBearerTokenHandler"/>.
        /// </summary>
        /// <param name="token">
        ///     The bearer token added to outgoing requests.
        /// </param>
        public StaticBearerTokenHandler(string token)
        {
            if (String.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'token'.", nameof(token));
            
            _token = token;
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
        protected override Task<string> GetTokenAsync(CancellationToken cancellationToken) => Task.FromResult(_token);
    }
}