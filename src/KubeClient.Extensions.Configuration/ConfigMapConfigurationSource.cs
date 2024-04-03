using Microsoft.Extensions.Configuration;
using System;

namespace KubeClient.Extensions.Configuration
{
    using Settings;

    /// <summary>
    ///     Source for configuration that comes from a Kubernetes ConfigMap.
    /// </summary>
    sealed class ConfigMapConfigurationSource
        : IConfigurationSource
    {
        /// <summary>
        ///     Create a new <see cref="ConfigMapConfigurationSource"/>.
        /// </summary>
        /// <param name="settings">
        ///     The <see cref="ConfigMapConfigurationSettings"/> used to create configuration providers.
        /// </param>
        public ConfigMapConfigurationSource(ConfigMapConfigurationSettings settings)
        {
            if ( settings == null )
                throw new ArgumentNullException(nameof(settings));

            Settings = settings;
        }

        /// <summary>
        ///     The <see cref="ConfigMapConfigurationSettings"/> used to create configuration providers.
        /// </summary>
        public ConfigMapConfigurationSettings Settings { get; }

        /// <summary>
        ///     Build a configuration provider with configured options.
        /// </summary>
        /// <param name="configurationBuilder">
        ///     The configuration builder to retrieve options from.
        /// </param>
        /// <returns>
        ///     The new <see cref="IConfigurationProvider"/>.
        /// </returns>
        public IConfigurationProvider Build(IConfigurationBuilder configurationBuilder) => new ConfigMapConfigurationProvider(Settings);
    }
}
