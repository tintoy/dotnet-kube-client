using HTTPlease;
using System;
using System.Net.Http;

namespace KubeClient.Authentication
{
    using MessageHandlers;

    /// <summary>
    ///     A Kubernetes API authentication strategy that uses HTTP Basic (username / password) authentication.
    /// </summary>
    public class KubeBasicAuthStrategy
        : KubeAuthStrategy
    {
        /// <summary>
        ///     Create a new <see cref="KubeBasicAuthStrategy"/>.
        /// </summary>
        public KubeBasicAuthStrategy()
        {
        }

        /// <summary>
        ///     The username for authentication to the Kubernetes API.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     The password for authentication to the Kubernetes API.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Validate the authentication strategy's configuration.
        /// </summary>
        /// <exception cref="KubeClientException">
        ///     The authentication strategy's configuration is incomplete or invalid.
        /// </exception>
        public override void Validate()
        {
            if ( string.IsNullOrEmpty(UserName) )
                throw new KubeClientException("The username for for authentication to the Kubernetes API has not been configured.");

            if ( string.IsNullOrEmpty(Password) )
                throw new KubeClientException("The password for for authentication to the Kubernetes API has not been configured.");
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
            if ( clientBuilder == null )
                throw new ArgumentNullException(nameof(clientBuilder));

            Validate();

            return clientBuilder.AddHandler(
                () => new BasicAuthenticationHandler(UserName, Password)
            );
        }

        /// <summary>
        ///     Create a deep clone of the authentication strategy.
        /// </summary>
        /// <returns>
        ///     The cloned <see cref="KubeAuthStrategy"/>.
        /// </returns>
        public override KubeAuthStrategy Clone()
        {
            return new KubeBasicAuthStrategy
            {
                UserName = UserName,
                Password = Password
            };
        }
    }
}
