using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.Authentication
{
    /// <summary>
    ///     Represents configuration for client authentication to the Kubernetes API.
    /// </summary>
    public interface IClientAuthenticationConfig
    {
        /// <summary>
        ///     Add a client certificate for authentication to the Kubernetes API.
        /// </summary>
        /// <param name="certificate">
        ///     An <see cref="X509Certificate2"/> representing the client certificate to use.
        /// </param>
        void AddClientCertificate(X509Certificate2 certificate);

        /// <summary>
        ///     Configure the HTTP "Authorization" header for authentication to the Kubernetes API.
        /// </summary>
        /// <param name="scheme">
        ///     The authentication scheme (e.g. "Basic", "Bearer", etc).
        /// </param>
        /// <param name="value">
        ///     The authentication value.
        /// </param>
        void SetAuthorizationHeader(string scheme, string value);
    }
}
