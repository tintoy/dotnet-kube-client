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
        ///     Load configuration entries from the Secret.
        /// </summary>
        public override void Load()
        {
            SecretV1 secret = _client.SecretsV1().Get(_secretName, _kubeNamespace).GetAwaiter().GetResult();
            if (secret != null)
            {
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
                Data = new Dictionary<string, string>();

            if (_watch && _watchSubscription == null)
            {
                _watchSubscription = _client.SecretsV1()
                    .Watch(_secretName, _kubeNamespace)
                    .Subscribe(secretEvent =>
                    {
                        if (secretEvent.EventType == ResourceEventType.Modified)
                            OnReload();
                    });
            }
        }
    }
}
