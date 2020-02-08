using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.MessageHandlers
{
    /// <summary>
    ///     Basic Authentication Handler for username/password authentication.
    /// </summary>
    public class BasicAuthenticationHandler
        : DelegatingHandler
    {
        /// <summary>
        ///     The Base64-encoded value for the Authorization header.
        /// </summary>
        readonly string _encodedAuthorizationHeader;

        /// <summary>
        ///     Create a new <see cref="BasicAuthenticationHandler"/>.
        /// </summary>
        /// <param name="username">
        ///     The username for authentication.
        /// </param>
        /// <param name="password">
        ///     The password for authentication.
        /// </param>
        public BasicAuthenticationHandler(string username, string password)
        {
            if ( String.IsNullOrEmpty(username) )
                throw new ArgumentNullException(nameof(username));

            if ( String.IsNullOrEmpty(password) )
                throw new ArgumentNullException(nameof(password));

            _encodedAuthorizationHeader = Convert.ToBase64String(
                Encoding.ASCII.GetBytes(username + ":" + password)
            );
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
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", _encodedAuthorizationHeader);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
