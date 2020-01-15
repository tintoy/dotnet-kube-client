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
    /// Basic Authentication Handler for username/password authentication.
    /// </summary>
    public class BasicAuthenticationHandler : DelegatingHandler
    {
        readonly string _encoded;

        /// <summary>
        /// Create a new <see cref="BasicAuthenticationHandler"/>.
        /// </summary>
        /// <param name="username">The username to use</param>
        /// <param name="password">The password to use</param>
        public BasicAuthenticationHandler(string username, string password)
        {
            if(String.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            if(String.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            _encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", _encoded);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
