using Microsoft.Extensions.Configuration;

namespace KubeClient.Extensions.Configuration
{
    using Settings;
    using System;

    /// <summary>
    ///     Source for configuration that comes from a Kubernetes Secret.
    /// </summary>
    sealed class SecretConfigurationSource
        : IConfigurationSource
    {
        /// <summary>
        ///     Create a new <see cref="SecretConfigurationSource"/>.
        /// </summary>
        /// <param name="settings">
        ///     The <see cref="SecretConfigurationSettings"/> used to create configuration providers.
        /// </param>
        public SecretConfigurationSource(SecretConfigurationSettings settings)
        {
            if ( settings == null )
                throw new ArgumentNullException(nameof(settings));

            Settings = settings;
        }

        /// <summary>
        ///     The <see cref="SecretConfigurationSettings"/> used to create configuration providers.
        /// </summary>
        public SecretConfigurationSettings Settings { get; }

        /// <summary>
        ///     Build a configuration provider with configured options.
        /// </summary>
        /// <param name="configurationBuilder">
        ///     The configuration builder to retrieve options from.
        /// </param>
        /// <returns>
        ///     The new <see cref="IConfigurationProvider"/>.
        /// </returns>
        public IConfigurationProvider Build(IConfigurationBuilder configurationBuilder) => new SecretConfigurationProvider(Settings);
    }
}
