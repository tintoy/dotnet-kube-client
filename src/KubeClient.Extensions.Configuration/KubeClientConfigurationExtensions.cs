using Microsoft.Extensions.Configuration;
using System;

namespace KubeClient.Extensions.Configuration
{
    using Settings;

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
        /// <param name="throwOnNotFound">
        ///     Throw an exception if the ConfigMap was not found when the configuration is first loaded?
        /// </param>
        /// <returns>
        ///     The configured <see cref="IConfigurationBuilder"/>.
        /// </returns>
        public static IConfigurationBuilder AddKubeConfigMap(this IConfigurationBuilder configurationBuilder,
            KubeClientOptions clientOptions, string configMapName, string kubeNamespace = null,
            string sectionName = null, bool reloadOnChange = false, bool throwOnNotFound = false)
        {
            if (configurationBuilder == null)
                throw new ArgumentNullException(nameof(configurationBuilder));

            KubeApiClient client = KubeApiClient.Create(clientOptions);

            return configurationBuilder.AddKubeConfigMap(client, configMapName, kubeNamespace, sectionName, reloadOnChange, throwOnNotFound);
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
        /// <param name="throwOnNotFound">
        ///    Throw an exception if the ConfigMap was not found when the configuration is first loaded?
        /// </param>
        /// <returns>
        ///     The configured <see cref="IConfigurationBuilder"/>.
        /// </returns>
        public static IConfigurationBuilder AddKubeConfigMap(this IConfigurationBuilder configurationBuilder,
            KubeApiClient client, string configMapName, string kubeNamespace = null, string sectionName = null,
            bool reloadOnChange = false, bool throwOnNotFound = false)
        {
            if (configurationBuilder == null)
                throw new ArgumentNullException(nameof(configurationBuilder));

            return configurationBuilder.Add(new ConfigMapConfigurationSource(
                new ConfigMapConfigurationSettings(client, configMapName, kubeNamespace, sectionName, reloadOnChange, throwOnNotFound)
            ));
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
        public static IConfigurationBuilder AddKubeSecret(this IConfigurationBuilder configurationBuilder,
            KubeClientOptions clientOptions, string secretName, string kubeNamespace = null, string sectionName = null,
            bool reloadOnChange = false)
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
        public static IConfigurationBuilder AddKubeSecret(this IConfigurationBuilder configurationBuilder,
            KubeApiClient client, string secretName, string kubeNamespace = null, string sectionName = null,
            bool reloadOnChange = false)
        {
            if (configurationBuilder == null)
                throw new ArgumentNullException(nameof(configurationBuilder));

            return configurationBuilder.Add(new SecretConfigurationSource(
                new SecretConfigurationSettings(client, secretName, kubeNamespace, sectionName, reloadOnChange, throwOnNotFound: false /* not implemented yet */)
            ));
        }
    }
}