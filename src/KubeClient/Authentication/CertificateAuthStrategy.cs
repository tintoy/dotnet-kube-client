using HTTPlease;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace KubeClient.Authentication
{
    /// <summary>
    ///     A Kubernetes API authentication strategy that uses an X.509 client certificate.
    /// </summary>
    public class CertificateAuthStrategy
        : KubeAuthStrategy
    {
        /// <summary>
        ///     Create a new <see cref="CertificateAuthStrategy"/>.
        /// </summary>
        public CertificateAuthStrategy()
        {
        }

        /// <summary>
        ///     The X.509 client certificate to use for authentication to the Kubernetes API.
        /// </summary>
        public X509Certificate2 Certificate { get; set; }

        /// <summary>
        ///     Validate the authentication strategy's configuration.
        /// </summary>
        /// <exception cref="KubeClientException">
        ///     The authentication strategy's configuration is incomplete or invalid.
        /// </exception>
        public override void Validate()
        {
            if (Certificate == null)
                throw new KubeClientException("The X.509 client certificate used to authenticate to the Kubernetes API has not been configured.");

            if (!Certificate.HasPrivateKey)
                throw new KubeClientException("The private key for the X.509 client certificate used to authenticate to the Kubernetes API is not available.");
        }

        /// <summary>
        ///     Configure the <see cref="ClientBuilder"/> used to create <see cref="HttpClient"/>s used by the API client.
        /// </summary>
        /// <param name="clientBuilder">
        ///     The <see cref="ClientBuilder"/> to configure.
        /// </param>
        /// <returns>
        ///     The configured <see cref="ClientBuilder"/>.
        /// </returns>
        public override ClientBuilder Configure(ClientBuilder clientBuilder)
        {
            if (clientBuilder == null)
                throw new ArgumentNullException(nameof(clientBuilder));

            Validate();

            return clientBuilder.WithClientCertificate(Certificate);
        }

        /// <summary>
        ///     Create a deep clone of the authentication strategy.
        /// </summary>
        /// <returns>
        ///     The cloned <see cref="KubeAuthStrategy"/>.
        /// </returns>
        public override KubeAuthStrategy Clone()
        {
            return new CertificateAuthStrategy
            {
                Certificate = Certificate
            };
        }
    }
}
