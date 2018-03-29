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
        /// <param name="sectionName">
        ///     The name of the target configuration section (if any).
        /// </param>
        /// <param name="reloadOnChange">
        ///     Reload the configuration if the ConfigMap changes?
        /// </param>
        /// <returns>
        ///     The configured <see cref="IConfigurationBuilder"/>.
        /// </returns>
        public static IConfigurationBuilder AddKubeConfigMap(this IConfigurationBuilder configurationBuilder, KubeClientOptions clientOptions, string configMapName, string kubeNamespace = null, string sectionName = null, bool reloadOnChange = false)
        {
            if (configurationBuilder == null)
                throw new ArgumentNullException(nameof(configurationBuilder));
            
            KubeApiClient client = KubeApiClient.Create(clientOptions);

            return configurationBuilder.AddKubeConfigMap(client, configMapName, kubeNamespace, sectionName, reloadOnChange);
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
        /// <param name="sectionName">
        ///     The name of the target configuration section (if any).
        /// </param>
        /// <param name="reloadOnChange">
        ///     Reload the configuration if the ConfigMap changes?
        /// </param>
        /// <returns>
        ///     The configured <see cref="IConfigurationBuilder"/>.
        /// </returns>
        public static IConfigurationBuilder AddKubeConfigMap(this IConfigurationBuilder configurationBuilder, KubeApiClient client, string configMapName, string kubeNamespace = null, string sectionName = null, bool reloadOnChange = false)
        {
            if (configurationBuilder == null)
                throw new ArgumentNullException(nameof(configurationBuilder));

            configurationBuilder.Properties["KubeClient_ConfigMap_Client"] = client;
            configurationBuilder.Properties["KubeClient_ConfigMap_Name"] = configMapName;
            configurationBuilder.Properties["KubeClient_ConfigMap_Namespace"] = kubeNamespace;
            configurationBuilder.Properties["KubeClient_ConfigMap_SectionName"] = sectionName;
            configurationBuilder.Properties["KubeClient_ConfigMap_Watch"] = reloadOnChange;

            return configurationBuilder.Add(
                new ConfigMapConfigurationSource()
            );
        }

        /// <summary>
        ///     Add the specified Kubernetes Secret as a configuration source.
        /// </summary>
        /// <param name="configurationBuilder">
        ///     The <see cref="IConfigurationBuilder"/> to configure.
        /// </param>
        /// <param name="clientOptions">
        ///     <see cref="KubeClientOptions"/> for the <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </param>
        /// <param name="secretName">
        ///     The name of the target Secret.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The Kubernetes namespace that contains the target Secret.
        /// </param>
        /// <param name="sectionName">
        ///     The name of the target configuration section (if any).
        /// </param>
        /// <param name="reloadOnChange">
        ///     Reload the configuration if the Secret changes?
        /// </param>
        /// <returns>
        ///     The configured <see cref="IConfigurationBuilder"/>.
        /// </returns>
        public static IConfigurationBuilder AddKubeSecret(this IConfigurationBuilder configurationBuilder, KubeClientOptions clientOptions, string secretName, string kubeNamespace = null, string sectionName = null, bool reloadOnChange = false)
        {
            if (configurationBuilder == null)
                throw new ArgumentNullException(nameof(configurationBuilder));
            
            KubeApiClient client = KubeApiClient.Create(clientOptions);

            return configurationBuilder.AddKubeSecret(client, secretName, kubeNamespace, sectionName, reloadOnChange);
        }

        /// <summary>
        ///     Add the specified Kubernetes Secret as a configuration source.
        /// </summary>
        /// <param name="configurationBuilder">
        ///     The <see cref="IConfigurationBuilder"/> to configure.
        /// </param>
        /// <param name="client">
        ///     The <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </param>
        /// <param name="secretName">
        ///     The name of the target Secret.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The Kubernetes namespace that contains the target Secret.
        /// </param>
        /// <param name="sectionName">
        ///     The name of the target configuration section (if any).
        /// </param>
        /// <param name="reloadOnChange">
        ///     Reload the configuration if the Secret changes?
        /// </param>
        /// <returns>
        ///     The configured <see cref="IConfigurationBuilder"/>.
        /// </returns>
        public static IConfigurationBuilder AddKubeSecret(this IConfigurationBuilder configurationBuilder, KubeApiClient client, string secretName, string kubeNamespace = null, string sectionName = null, bool reloadOnChange = false)
        {
            if (configurationBuilder == null)
                throw new ArgumentNullException(nameof(configurationBuilder));

            configurationBuilder.Properties["KubeClient_Secret_Client"] = client;
            configurationBuilder.Properties["KubeClient_Secret_Name"] = secretName;
            configurationBuilder.Properties["KubeClient_Secret_Namespace"] = kubeNamespace;
            configurationBuilder.Properties["KubeClient_Secret_SectionName"] = sectionName;
            configurationBuilder.Properties["KubeClient_Secret_Watch"] = reloadOnChange;

            return configurationBuilder.Add(
                new SecretConfigurationSource()
            );
        }
    }
}
