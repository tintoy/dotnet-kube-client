using System;
using System.Collections.Immutable;

namespace KubeClient.Extensions.Configuration.Settings
{
    /// <summary>
    /// Settings for a single instance of the <see cref="SecretConfigurationProvider"/>.
    /// </summary>
    public class SecretConfigurationSettings
    {
        /// <summary>
        ///     The default character(s) that represent delimiters between segments of key paths (e.g. '.' in "foo.bar.baz").
        /// </summary>
        public static readonly IImmutableSet<char> DefaultKeyPathDelimiters = ImmutableHashSet.Create('.');

        /// <summary>
        ///     Do not treat any characters as key-path delimiters.
        /// </summary>
        /// <seealso cref="DefaultKeyPathDelimiters"/>
        public static readonly IImmutableSet<char> NoKeyPathDelimiters = ImmutableHashSet<char>.Empty;

        /// <summary>
        ///     Create new <see cref="SecretConfigurationSettings"/>.
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
            : this(client, secretName, kubeNamespace, sectionName, watch, throwOnNotFound, keyPathDelimiters: DefaultKeyPathDelimiters)
        {
        }

        /// <summary>
        ///     Create new <see cref="SecretConfigurationSettings"/>.
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
        /// <param name="keyPathDelimiters">
        ///     Characters (if any) that represent delimiters between segments of key paths (e.g. '.' in "foo.bar.baz").
        /// </param>
        public SecretConfigurationSettings(IKubeApiClient client, string secretName, string kubeNamespace, string sectionName, bool watch, bool throwOnNotFound, IImmutableSet<char> keyPathDelimiters)
        {
            if ( client == null )
                throw new ArgumentNullException(nameof(client));

            if ( String.IsNullOrWhiteSpace(secretName) )
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'secretName'.", nameof(secretName));

            if (keyPathDelimiters == null)
                throw new ArgumentNullException(nameof(keyPathDelimiters));

            Client = client;
            SecretName = secretName;
            KubeNamespace = kubeNamespace;
            SectionName = sectionName;
            Watch = watch;
            ThrowOnNotFound = throwOnNotFound;
            KeyPathDelimiters = keyPathDelimiters;
        }

        /// <summary>
        ///     The <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </summary>
        public IKubeApiClient Client { get; }

        /// <summary>
        ///     The name of the target Secret.
        /// </summary>
        public string SecretName { get; }

        /// <summary>
        ///     The Kubernetes namespace that contains the target Secret.
        /// </summary>
        public string KubeNamespace { get; }

        /// <summary>
        ///     The name of the target configuration section (if any).
        /// </summary>
        public string SectionName { get; }

        /// <summary>
        ///     Watch the Secret for changes?
        /// </summary>
        public bool Watch { get; }

        /// <summary>
        ///     Throw an exception if the Secret was not found?
        /// </summary>
        public bool ThrowOnNotFound { get; }

        /// <summary>
        ///     Characters (if any) that represent delimiters between segments of key paths (e.g. '.' in "foo.bar.baz").
        /// </summary>
        public IImmutableSet<char> KeyPathDelimiters { get; }
    }
}
