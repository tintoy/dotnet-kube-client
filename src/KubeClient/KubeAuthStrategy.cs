using HTTPlease;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace KubeClient
{
    using Authentication;
    using System.Collections.Generic;

    /// <summary>
    ///     The base class for implementations of authentication to the Kubernetes API.
    /// </summary>
    public abstract class KubeAuthStrategy
    {
        /// <summary>
        ///     Validate the authentication strategy's configuration.
        /// </summary>
        /// <exception cref="KubeClientException">
        ///     The authentication strategy's configuration is incomplete or invalid.
        /// </exception>
        public abstract void Validate();

        /// <summary>
        ///     Configure the <see cref="ClientBuilder"/> used to create <see cref="HttpClient"/>s used by the API client.
        /// </summary>
        /// <param name="clientBuilder">
        ///     The <see cref="ClientBuilder"/> to configure.
        /// </param>
        /// <returns>
        ///     The configured <see cref="ClientBuilder"/>.
        /// </returns>
        public abstract ClientBuilder Configure(ClientBuilder clientBuilder);

        /// <summary>
        ///     Create a deep clone of the authentication strategy.
        /// </summary>
        /// <returns>
        ///     The cloned <see cref="KubeAuthStrategy"/>.
        /// </returns>
        public abstract KubeAuthStrategy Clone();

        /// <summary>
        ///     Don't attempt to authenticate (i.e. anonymous access).
        /// </summary>
        public static KubeAuthStrategy None => NoneAuthStrategy.Instance;

        /// <summary>
        ///     Use an X.509 client certificate for authentication.
        /// </summary>
        /// <param name="certificate">
        ///     The X.509 certificate for authentication.
        /// </param>
        /// <returns>
        ///     The configured <see cref="CertificateAuthStrategy"/>.
        /// </returns>
        public static CertificateAuthStrategy ClientCertificate(X509Certificate2 certificate) => new CertificateAuthStrategy
        {
            Certificate = certificate
        };

        /// <summary>
        ///     Use HTTP Basic (username / password) authentication.
        /// </summary>
        /// <param name="userName">
        ///     The username for authentication.
        /// </param>
        /// <param name="password">
        ///     The password for authentication.
        /// </param>
        /// <returns>
        ///     The configured <see cref="BasicAuthStrategy"/>.
        /// </returns>
        public static BasicAuthStrategy Basic(string userName, string password) => new BasicAuthStrategy
        {
            UserName = userName,
            Password = password
        };

        /// <summary>
        ///     Use a statically-configured access token for authentication.
        /// </summary>
        /// <param name="token">
        ///     The access token for authentication.
        /// </param>
        /// <returns>
        ///     The configured <see cref="BearerTokenAuthStrategy"/>.
        /// </returns>
        public static BearerTokenAuthStrategy BearerToken(string token) => new BearerTokenAuthStrategy
        {
            Token = token
        };

        /// <summary>
        ///     Use an access token provided by a legacy authentication provider ("auth-provider" in the K8s config).
        /// </summary>
        /// <param name="command">
        ///     The command to execute that provides the access token.
        /// </param>
        /// <param name="arguments">
        ///     Arguments (if any) for the command to execute that provides the access token.
        /// </param>
        /// <param name="selector">
        ///     The Go-style selector used to retrieve the access token from the command output.
        /// </param>
        /// <param name="expirySelector">
        ///     The Go-style selector used to retrieve the access token's expiry date/time from the command output.
        /// </param>
        /// <param name="initialToken">
        ///     The initial access token (if available) used to authenticate to the Kubernetes API.
        /// </param>
        /// <param name="initialTokenExpiryUtc">
        ///     The initial token expiry (if available) used to authenticate to the Kubernetes API.
        /// </param>
        /// <returns>
        ///     The configured <see cref="BearerTokenProviderAuthStrategy"/>.
        /// </returns>
        public static BearerTokenProviderAuthStrategy BearerTokenProvider(string command, string arguments, string selector, string expirySelector, string initialToken = null, DateTime? initialTokenExpiryUtc = null) => new BearerTokenProviderAuthStrategy
        {
            Command = command,
            Arguments = arguments,
            Selector = selector,
            ExpirySelector = expirySelector,
            InitialToken = initialToken,
            InitialTokenExpiryUtc = initialTokenExpiryUtc
        };

        /// <summary>
        ///     Use a client-go credential plugin ("exec" in the K8s config).
        /// </summary>
        /// <param name="pluginApiVersion">
        ///     The plugin API version.
        ///     
        ///     <para>
        ///         The API version returned by the plugin MUST match the version specified here.
        ///     </para>
        /// </param>
        /// <param name="command">
        ///     The command used to generate an access token for authenticating to the Kubernetes API.
        /// </param>
        /// <param name="arguments">
        ///     The arguments (if any) for the command used to generate an access token for authenticating to the Kubernetes API.
        /// </param>
        /// <param name="environmentVariables">
        ///     The environment variables arguments (if any) for the command used to generate an access token for authenticating to the Kubernetes API.
        /// </param>
        /// <returns>
        ///     The configured <see cref="CredentialPluginAuthStrategy"/>.
        /// </returns>
        public static CredentialPluginAuthStrategy CredentialPlugin(string pluginApiVersion, string command, IReadOnlyList<string> arguments = null, IReadOnlyDictionary<string, string> environmentVariables = null)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            var authStrategy = new CredentialPluginAuthStrategy
            {
                PluginApiVersion = pluginApiVersion,
                Command = command,
            };

            if (arguments != null)
                authStrategy.Arguments.AddRange(arguments);

            if (environmentVariables != null)
            {
                foreach ((string variableName, string variableValue) in authStrategy.EnvironmentVariables)
                    authStrategy.EnvironmentVariables.Add(variableName, variableValue);
            }

            return authStrategy;
        }

    }
}
