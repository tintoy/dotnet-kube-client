using System;

namespace KubeClient.Extensions.Configuration.Settings
{
    /// <summary>
    /// Settings for a single instance of the <see cref="ConfigMapConfigurationProvider"/>.
    /// </summary>
    public class ConfigMapConfigurationSettings
    {
        /// <summary>
        /// Create new <see cref="ConfigMapConfigurationSettings"/>.
        /// </summary>
        /// <param name="client">
        ///     The <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        ///     
        ///     Note: this client will be disposed by the provider.
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
        /// <param name="watch">
        ///     Watch the ConfigMap for changes?
        /// </param>
        /// <param name="throwOnNotFound">
        ///    Throw an exception if the ConfigMap was not found?
        /// </param>
        public ConfigMapConfigurationSettings(IKubeApiClient client, string configMapName, string kubeNamespace, string sectionName, bool watch, bool throwOnNotFound)
        {
            if ( client == null )
                throw new ArgumentNullException(nameof(client));

            if ( String.IsNullOrWhiteSpace(configMapName) )
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'configMapName'.", nameof(configMapName));

            Client = client;
            ConfigMapName = configMapName;
            KubeNamespace = kubeNamespace;
            SectionName = sectionName;
            Watch = watch;
            ThrowOnNotFound = throwOnNotFound;
        }

        /// <summary>
        /// The <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </summary>
        public IKubeApiClient Client { get; }

        /// <summary>
        /// The name of the target ConfigMap.
        /// </summary>
        public string ConfigMapName { get; }

        /// <summary>
        /// The Kubernetes namespace that contains the target ConfigMap.
        /// </summary>
        public string KubeNamespace { get; }

        /// <summary>
        /// The name of the target configuration section (if any).
        /// </summary>
        public string SectionName { get; }

        /// <summary>
        /// Watch the ConfigMap for changes?
        /// </summary>
        public bool Watch { get; }

        /// <summary>
        /// Throw an exception if the ConfigMap was not found?
        /// </summary>
        public bool ThrowOnNotFound { get; }
    }
}
