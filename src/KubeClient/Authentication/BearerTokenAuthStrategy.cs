using HTTPlease;
using System;
using System.Net.Http;

namespace KubeClient.Authentication
{
    using MessageHandlers;

    /// <summary>
    ///     A Kubernetes API authentication strategy that uses a static access token.
    /// </summary>
    public class BearerTokenAuthStrategy
        : KubeAuthStrategy
    {
        /// <summary>
        ///     Create a new <see cref="BearerTokenAuthStrategy"/>.
        /// </summary>
        public BearerTokenAuthStrategy()
        {
        }

        /// <summary>
        ///     The static access token to use.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     Validate the authentication strategy's configuration.
        /// </summary>
        /// <exception cref="KubeClientException">
        ///     The authentication strategy's configuration is incomplete or invalid.
        /// </exception>
        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Token))
                throw new KubeClientException("The access token for authentication to the Kubernetes API has not been configured.");
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

            return clientBuilder.AddHandler(
                () => new StaticBearerTokenHandler(Token)
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
            return new BearerTokenAuthStrategy
            {
                Token = Token
            };
        }
    }
}
