using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace KubeClient.Extensions.Configuration
{
    using Models;

    /// <summary>
    ///     Provider for configuration that comes from a Kubernetes ConfigMap.
    /// </summary>
    sealed class ConfigMapConfigurationProvider
        : ConfigurationProvider, IDisposable
    {
        /// <summary>
        ///     The <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </summary>
        readonly KubeApiClient _client;

        /// <summary>
        ///     The name of the target ConfigMap.
        /// </summary>
        readonly string _configMapName;

        /// <summary>
        ///     The Kubernetes namespace that contains the target ConfigMap.
        /// </summary>
        readonly string _kubeNamespace;

        /// <summary>
        ///     Watch the ConfigMap for changes?
        /// </summary>
        readonly bool _watch;

        /// <summary>
        ///     An <see cref="IDisposable"/> representing the subscription to events for the watched ConfigMap.
        /// </summary>
        IDisposable _watchSubscription;

        /// <summary>
        ///     Create a new <see cref="ConfigMapConfigurationProvider"/>.
        /// </summary>
        /// <param name="client">
        ///     The <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </param>
        /// <param name="configMapName">
        ///     The name of the target ConfigMap.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The Kubernetes namespace that contains the target ConfigMap.
        /// </param>
        /// <param name="watch">
        ///     Watch the ConfigMap for changes?
        /// </param>
        public ConfigMapConfigurationProvider(KubeApiClient client, string configMapName, string kubeNamespace, bool watch)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (String.IsNullOrWhiteSpace(configMapName))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'configMapName'.", nameof(configMapName));
            
            _client = client;
            _configMapName = configMapName;
            _kubeNamespace = kubeNamespace;
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
        ///     Load configuration entries from the ConfigMap.
        /// </summary>
        public override void Load()
        {
            ConfigMapV1 configMap = _client.ConfigMapsV1().Get(_configMapName, _kubeNamespace).GetAwaiter().GetResult();
            if (configMap != null)
                Data = new Dictionary<string, string>(configMap.Data);
            else
                Data = new Dictionary<string, string>();

            if (_watch && _watchSubscription == null)
            {
                _watchSubscription = _client.ConfigMapsV1()
                    .Watch(_configMapName, _kubeNamespace)
                    .Subscribe(configMapEvent =>
                    {
                        if (configMapEvent.EventType == ResourceEventType.Modified)
                            OnReload();
                    });
            }
        }
    }
}
