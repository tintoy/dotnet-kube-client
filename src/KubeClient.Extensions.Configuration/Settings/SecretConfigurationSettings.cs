using System;

namespace KubeClient.Extensions.Configuration.Settings
{
    /// <summary>
    /// Settings for a single instance of the <see cref="SecretConfigurationProvider"/>.
    /// </summary>
    public class SecretConfigurationSettings
    {
        /// <summary>
        /// Create new <see cref="SecretConfigurationSettings"/>.
        /// </summary>
        /// <param name="client">
        ///     The <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        ///     
        ///     Note: this client will be disposed by the provider.
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
        /// <param name="watch">
        ///     Watch the Secret for changes?
        /// </param>
        /// <param name="throwOnNotFound">
        ///    Throw an exception if the Secret was not found?
        /// </param>
        public SecretConfigurationSettings(IKubeApiClient client, string secretName, string kubeNamespace, string sectionName, bool watch, bool throwOnNotFound)
        {
            if ( client == null )
                throw new ArgumentNullException(nameof(client));

            if ( String.IsNullOrWhiteSpace(secretName) )
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'secretName'.", nameof(secretName));

            Client = client;
            SecretName = secretName;
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
        /// The name of the target Secret.
        /// </summary>
        public string SecretName { get; }

        /// <summary>
        /// The Kubernetes namespace that contains the target Secret.
        /// </summary>
        public string KubeNamespace { get; }

        /// <summary>
        /// The name of the target configuration section (if any).
        /// </summary>
        public string SectionName { get; }

        /// <summary>
        /// Watch the Secret for changes?
        /// </summary>
        public bool Watch { get; }

        /// <summary>
        /// Throw an exception if the Secret was not found?
        /// </summary>
        public bool ThrowOnNotFound { get; }
    }
}
