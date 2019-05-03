using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace KubeClient.Extensions.Configuration
{
    using System.Linq;
    using System.Text;
    using Models;

    /// <summary>
    ///     Provider for configuration that comes from a Kubernetes Secret.
    /// </summary>
    sealed class SecretConfigurationProvider
        : ConfigurationProvider, IDisposable
    {
        /// <summary>
        ///     The <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </summary>
        readonly KubeApiClient _client;

        /// <summary>
        ///     The name of the target Secret.
        /// </summary>
        readonly string _secretName;

        /// <summary>
        ///     The Kubernetes namespace that contains the target Secret.
        /// </summary>
        readonly string _kubeNamespace;

        /// <summary>
        ///     The name of the target configuration section (if any).
        /// </summary>
        readonly string _sectionName;

        /// <summary>
        ///     Watch the Secret for changes?
        /// </summary>
        readonly bool _watch;

        /// <summary>
        ///     An <see cref="IDisposable"/> representing the subscription to events for the watched Secret.
        /// </summary>
        IDisposable _watchSubscription;

        /// <summary>
        ///     Create a new <see cref="SecretConfigurationProvider"/>.
        /// </summary>
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
        /// <param name="watch">
        ///     Watch the Secret for changes?
        /// </param>
        public SecretConfigurationProvider(KubeApiClient client, string secretName, string kubeNamespace, string sectionName, bool watch)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (String.IsNullOrWhiteSpace(secretName))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'secretName'.", nameof(secretName));
            
            _client = client;
            Log = _client.LoggerFactory.CreateLogger<SecretConfigurationProvider>();
            _secretName = secretName;
            _kubeNamespace = kubeNamespace;
            _sectionName = sectionName;
            _watch = watch;
        }

        /// <summary>
        ///     Dispose of resources being used by the provider.
        /// </summary>
        public void Dispose()
        {
            if (_watchSubscription != null)
            {
                _watchSubscription.Dispose();
                _watchSubscription = null;
            }

            _client.Dispose();
        }

        /// <summary>
        /// The configuration provider's logger.
        /// </summary>
        ILogger Log { get; }

        /// <summary>
        ///     Load configuration entries from the Secret.
        /// </summary>
        public override void Load()
        {
            Log.LogTrace("Attempting to load Secret {SecretName} in namespace {KubeNamespace}...", _secretName, _kubeNamespace ?? _client.DefaultNamespace);

            SecretV1 secret = _client.SecretsV1().Get(_secretName, _kubeNamespace).GetAwaiter().GetResult();
            
            Load(secret);

            if (_watch && _watchSubscription == null)
            {
                Log.LogTrace("Creating watch-event stream for Secret {SecretName} in namespace {KubeNamespace}...", _secretName, _kubeNamespace ?? _client.DefaultNamespace);

                _watchSubscription = _client.SecretsV1()
                    .Watch(_secretName, _kubeNamespace)
                    .Subscribe(OnSecretChanged);

                Log.LogTrace("Watch-event stream created for Secret {SecretName} in namespace {KubeNamespace}.", _secretName, _kubeNamespace ?? _client.DefaultNamespace);
            }
        }

        /// <summary>
        ///     Load data from the specified Secret.
        /// </summary>
        /// <param name="secret">
        ///     A <see cref="SecretV1"/> representing the Secret's current state, or <c>null</c> if the Secret was not found.
        /// </param>
        void Load(SecretV1 secret)
        {
            if (secret != null)
            {
                Log.LogTrace("Found Secret {SecretName} in namespace {KubeNamespace}.", _secretName, _kubeNamespace ?? _client.DefaultNamespace);

                string sectionNamePrefix = !String.IsNullOrWhiteSpace(_sectionName) ? _sectionName + ":" : String.Empty;

                Data = secret.Data.ToDictionary(
                    entry => sectionNamePrefix + entry.Key.Replace('.', ':'),
                    entry =>
                    {
                        try
                        {
                            // Will choke on binary data that doesn't represent valid UTF8 text
                            return Encoding.UTF8.GetString(
                                Convert.FromBase64String(entry.Value)
                            );
                        }
                        catch (FormatException)
                        {
                            // Not valid Base64; use raw value.

                            return entry.Value;
                        }
                        catch (ArgumentException)
                        {
                            // Not valid UTF8; use raw value.

                            return entry.Value;
                        }
                    },
                    StringComparer.OrdinalIgnoreCase
                );
            }
            else
            {
                Log.LogTrace("Secret {SecretName} was not found in namespace {KubeNamespace}.", _secretName, _kubeNamespace ?? _client.DefaultNamespace);

                Data = new Dictionary<string, string>();
            }
        }

        /// <summary>
        ///     Called when the target Secret is created, modified, or deleted.
        /// </summary>
        /// <param name="secretEvent">
        ///     The change-notification event data.
        /// </param>
        void OnSecretChanged(IResourceEventV1<SecretV1> secretEvent)
        {
            if (secretEvent == null)
                throw new ArgumentNullException(nameof(secretEvent));

            Log.LogTrace("Observed {EventType} watch-event for Secret {SecretName} in namespace {KubeNamespace}.", secretEvent.EventType, _secretName, _kubeNamespace ?? _client.DefaultNamespace);

            if (secretEvent.EventType != ResourceEventType.Error)
                Load(secretEvent.Resource);
            else
                Load(null); // Clear out configuration if the Secret is invalid

            Log.LogTrace("Triggering config change-token for Secret {SecretName} in namespace {KubeNamespace}...", _secretName, _kubeNamespace ?? _client.DefaultNamespace);

            OnReload();

            Log.LogTrace("Config change-token triggered for Secret {SecretName} in namespace {KubeNamespace}.", _secretName, _kubeNamespace ?? _client.DefaultNamespace);
        }
    }
}
