using Microsoft.Extensions.Configuration;

namespace KubeClient.Extensions.Configuration
{
    /// <summary>
    ///     Source for configuration that comes from a Kubernetes Secret.
    /// </summary>
    sealed class SecretConfigurationSource
        : IConfigurationSource
    {
        /// <summary>
        ///     Create a new <see cref="SecretConfigurationSource"/>.
        /// </summary>
        public SecretConfigurationSource()
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
            return new SecretConfigurationProvider(
                client: (KubeApiClient)configurationBuilder.Properties["KubeClient_Secret_Client"],
                secretName: (string)configurationBuilder.Properties["KubeClient_Secret_Name"],
                kubeNamespace: (string)configurationBuilder.Properties["KubeClient_Secret_Namespace"],
                sectionName: (string)configurationBuilder.Properties["KubeClient_Secret_SectionName"],
                watch: (bool)configurationBuilder.Properties["KubeClient_Secret_Watch"]                
            );
        }
    }
}
