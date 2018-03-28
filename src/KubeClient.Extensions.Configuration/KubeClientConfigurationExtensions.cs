using System;
using Microsoft.Extensions.Configuration;

namespace KubeClient.Extensions.Configuration
{
    /// <summary>
    ///     <see cref="IConfigurationBuilder"/> extension methods for Kubernetes ConfigMaps and Secrets.
    /// </summary>
    public static class KubeClientConfigurationExtensions
    {
        /// <summary>
        ///     Add the specified Kubernetes ConfigMap as a configuration source.
        /// </summary>
        /// <param name="configurationBuilder">
        ///     The <see cref="IConfigurationBuilder"/> to configure.
        /// </param>
        /// <param name="clientOptions">
        ///     <see cref="KubeClientOptions"/> for the <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </param>
        /// <param name="configMapName">
        ///     The name of the target ConfigMap.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The Kubernetes namespace that contains the target ConfigMap.
        /// </param>
        /// <param name="reloadOnChange">
        ///     Reload the configuration if the ConfigMap changes?
        /// </param>
        /// <returns>
        ///     The configured <see cref="IConfigurationBuilder"/>.
        /// </returns>
        public static IConfigurationBuilder AddKubeConfigMap(this IConfigurationBuilder configurationBuilder, KubeClientOptions clientOptions, string configMapName, string kubeNamespace = null, bool reloadOnChange = false)
        {
            if (configurationBuilder == null)
                throw new ArgumentNullException(nameof(configurationBuilder));
            
            KubeApiClient client = KubeApiClient.Create(clientOptions);

            return configurationBuilder.AddKubeConfigMap(client, configMapName, kubeNamespace, reloadOnChange);
        }

        /// <summary>
        ///     Add the specified Kubernetes ConfigMap as a configuration source.
        /// </summary>
        /// <param name="configurationBuilder">
        ///     The <see cref="IConfigurationBuilder"/> to configure.
        /// </param>
        /// <param name="client">
        ///     The <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </param>
        /// <param name="configMapName">
        ///     The name of the target ConfigMap.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The Kubernetes namespace that contains the target ConfigMap.
        /// </param>
        /// <param name="reloadOnChange">
        ///     Reload the configuration if the ConfigMap changes?
        /// </param>
        /// <returns>
        ///     The configured <see cref="IConfigurationBuilder"/>.
        /// </returns>
        public static IConfigurationBuilder AddKubeConfigMap(this IConfigurationBuilder configurationBuilder, KubeApiClient client, string configMapName, string kubeNamespace = null, bool reloadOnChange = false)
        {
            if (configurationBuilder == null)
                throw new ArgumentNullException(nameof(configurationBuilder));

            configurationBuilder.Properties["KubeClient_ConfigMap_Client"] = client;
            configurationBuilder.Properties["KubeClient_ConfigMap_Name"] = configMapName;
            configurationBuilder.Properties["KubeClient_ConfigMap_Namespace"] = kubeNamespace;
            configurationBuilder.Properties["KubeClient_ConfigMap_Watch"] = reloadOnChange;

            return configurationBuilder.Add(
                new ConfigMapConfigurationSource()
            );
        }
    }
}
