using Microsoft.Extensions.Configuration;

namespace KubeClient.Extensions.Configuration
{
    /// <summary>
    ///     Source for configuration that comes from a Kubernetes ConfigMap.
    /// </summary>
    sealed class ConfigMapConfigurationSource
        : IConfigurationSource
    {

        /// <summary>
        ///     Create a new <see cref="ConfigMapConfigurationSource"/>.
        /// </summary>
        public ConfigMapConfigurationSource()
        {
        }

        /// <summary>
        ///     Build a configuration provider with configured options.
        /// </summary>
        /// <param name="configurationBuilder">
        ///     The configuration builder to retrieve options from.
        /// </param>
        /// <returns>
        ///     The new <see cref="IConfigurationProvider"/>.
        /// </returns>
        public IConfigurationProvider Build(IConfigurationBuilder configurationBuilder)
        {
            return new ConfigMapConfigurationProvider(
                client: (KubeApiClient)configurationBuilder.Properties[ConfigMapBuilderPropertyConstants.Client],
                configMapName: (string)configurationBuilder.Properties[ConfigMapBuilderPropertyConstants.Name],
                kubeNamespace: (string)configurationBuilder.Properties[ConfigMapBuilderPropertyConstants.Namespace],
                sectionName: (string)configurationBuilder.Properties[ConfigMapBuilderPropertyConstants.SectionName],
                watch: (bool)configurationBuilder.Properties[ConfigMapBuilderPropertyConstants.Watch],
                throwOnNotFound: (bool) configurationBuilder.Properties[ConfigMapBuilderPropertyConstants.ThrowOnNotFound]
            );
        }
    }
}
