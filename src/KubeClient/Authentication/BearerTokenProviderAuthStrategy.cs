using HTTPlease;
using System;
using System.Net.Http;

namespace KubeClient.Authentication
{
    using MessageHandlers;

    /// <summary>
    ///     A Kubernetes API authentication strategy that uses an access token provided by a legacy authentication provider ("auth-provider" in the K8s config).
    /// </summary>
    public class BearerTokenProviderAuthStrategy
        : KubeAuthStrategy
    {
        /// <summary>
        ///     Create a new <see cref="BearerTokenProviderAuthStrategy"/>.
        /// </summary>
        public BearerTokenProviderAuthStrategy()
        {
        }

        /// <summary>
        ///     The command used to generate an access token for authenticating to the Kubernetes API.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        ///     The command arguments used to generate an access token for authenticating to the Kubernetes API.
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        ///     The Go-style selector used to retrieve the access token from the command output.
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        ///     The Go-style selector used to retrieve the access token's expiry date/time from the command output.
        /// </summary>
        public string ExpirySelector { get; set; }

        /// <summary>
        ///     The initial access token (if available) used to authenticate to the Kubernetes API.
        /// </summary>
        public string InitialToken { get; set; }

        /// <summary>
        ///     The initial token expiry (if available) used to authenticate to the Kubernetes API.
        /// </summary>
        public DateTime? InitialTokenExpiryUtc { get; set; }

        /// <summary>
        ///     Validate the authentication strategy's configuration.
        /// </summary>
        /// <exception cref="KubeClientException">
        ///     The authentication strategy's configuration is incomplete or invalid.
        /// </exception>
        public override void Validate()
        {
            if (String.IsNullOrWhiteSpace(Command))
                throw new KubeClientException("The access-token provider command for authentication to the Kubernetes API has not been configured.");

            if (String.IsNullOrWhiteSpace(Selector))
                throw new KubeClientException("The access-token selector for authentication to the Kubernetes API has not been configured.");

            if (String.IsNullOrWhiteSpace(ExpirySelector))
                throw new KubeClientException("The access-token expiry selector for authentication to the Kubernetes API has not been configured.");
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
                () => new CommandBearerTokenHandler(Command, Arguments, Selector, ExpirySelector, InitialToken, InitialTokenExpiryUtc)
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
            return new BearerTokenProviderAuthStrategy
            {
                Command = Command,
                Arguments = Arguments,
                Selector = Selector,
                ExpirySelector = ExpirySelector,
                InitialToken = InitialToken,
                InitialTokenExpiryUtc = InitialTokenExpiryUtc
            };
        }
    }
}
