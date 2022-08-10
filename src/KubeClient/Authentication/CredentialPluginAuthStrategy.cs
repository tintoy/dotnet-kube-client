using HTTPlease;
using KubeClient.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace KubeClient.Authentication
{
    /// <summary>
    ///     A Kubernetes API authentication strategy that uses an access token provided by a client-go credential plugin ("exec" in the K8s config).
    /// </summary>
    public class CredentialPluginAuthStrategy
        : KubeAuthStrategy
    {
        /// <summary>
        ///     The supported version of the client-go plugin API used by the <see cref="CredentialPluginAuthStrategy"/>.
        /// </summary>
        /// <remarks>
        ///     Currently, this is the only version of the API supported by KubeClient.
        /// </remarks>
        public const string SupportedPluginApiVersion = "client.authentication.k8s.io/v1beta1";

        /// <summary>
        ///     Create a new <see cref="CredentialPluginAuthStrategy"/>.
        /// </summary>
        public CredentialPluginAuthStrategy()
        {
        }

        /// <summary>
        ///     The credential plugin API version.
        /// </summary>
        /// <remarks>
        ///      The API version returned by the plugin MUST match the version specified here.
        /// </remarks>
        public string PluginApiVersion { get; set; } = SupportedPluginApiVersion;

        /// <summary>
        ///     The command used to generate an access token for authenticating to the Kubernetes API.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        ///     The arguments (if any) for the command used to generate an access token for authenticating to the Kubernetes API.
        /// </summary>
        public List<string> Arguments { get; } = new List<string>();

        /// <summary>
        ///     The environment variables arguments (if any) for the command used to generate an access token for authenticating to the Kubernetes API.
        /// </summary>
        public Dictionary<string, string> EnvironmentVariables { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Validate the authentication strategy's configuration.
        /// </summary>
        /// <exception cref="KubeClientException">
        ///     The authentication strategy's configuration is incomplete or invalid.
        /// </exception>
        public override void Validate()
        {
            if ( String.IsNullOrWhiteSpace(PluginApiVersion) )
                throw new KubeClientException("The credential plug-in version for authentication to the Kubernetes API has not been configured.");

            if ( !String.Equals(PluginApiVersion, SupportedPluginApiVersion, StringComparison.OrdinalIgnoreCase) )
                throw new KubeClientException($"Unsupported credential plug-in version '{PluginApiVersion}' (only '{SupportedPluginApiVersion}' is currently supported).");

            if ( String.IsNullOrWhiteSpace(Command) )
                throw new KubeClientException("The credential plug-in command for authentication to the Kubernetes API has not been configured.");
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
                () => new CredentialPluginBearerTokenHandler(PluginApiVersion, Command, Arguments, EnvironmentVariables)
            );
        }

        /// <summary>
        ///     Configure client authentication.
        /// </summary>
        /// <param name="clientAuthenticationConfig">
        ///     The client authentication configuration.
        /// </param>
        public override void Configure(IClientAuthenticationConfig clientAuthenticationConfig)
        {
            if (clientAuthenticationConfig == null)
                throw new ArgumentNullException(nameof(clientAuthenticationConfig));

            throw new NotImplementedException(); // TODO: Figure out how to implement this (especially given that we would need to do it synchronously).
        }

        /// <summary>
        ///     Create a deep clone of the authentication strategy.
        /// </summary>
        /// <returns>
        ///     The cloned <see cref="KubeAuthStrategy"/>.
        /// </returns>
        public override KubeAuthStrategy Clone()
        {
            var clonedStrategy = new CredentialPluginAuthStrategy
            {
                PluginApiVersion = PluginApiVersion,
                Command = Command
            };

            clonedStrategy.Arguments.AddRange(Arguments);

            foreach ((string variableName, string variableValue) in EnvironmentVariables)
                clonedStrategy.EnvironmentVariables.Add(variableName, variableValue);
            
            return clonedStrategy;
        }
    }
}
