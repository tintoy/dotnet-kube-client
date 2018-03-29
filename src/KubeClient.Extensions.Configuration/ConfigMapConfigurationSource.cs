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
                client: (KubeApiClient)configurationBuilder.Properties["KubeClient_ConfigMap_Client"],
                configMapName: (string)configurationBuilder.Properties["KubeClient_ConfigMap_Name"],
                kubeNamespace: (string)configurationBuilder.Properties["KubeClient_ConfigMap_Namespace"],
                sectionName: (string)configurationBuilder.Properties["KubeClient_ConfigMap_SectionName"],
                watch: (bool)configurationBuilder.Properties["KubeClient_ConfigMap_Watch"]                
            );
        }
    }
}
