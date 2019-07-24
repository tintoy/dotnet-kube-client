using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
        ///     The name of the target configuration section (if any).
        /// </summary>
        readonly string _sectionName;

        /// <summary>
        ///     Watch the ConfigMap for changes?
        /// </summary>
        readonly bool _watch;

        /// <summary>
        ///     Throw an exception if the ConfigMap is not found?
        /// </summary>
        readonly bool _throwOnNotFound;

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
        /// <param name="sectionName">
        ///     The name of the target configuration section (if any).
        /// </param>
        /// <param name="watch">
        ///     Watch the ConfigMap for changes?
        /// </param>
        /// <param name="throwOnNotFound">
        ///    Throw an exception if the ConfigMap was not found?
        /// </param>
        public ConfigMapConfigurationProvider(KubeApiClient client, string configMapName, string kubeNamespace, string sectionName, bool watch, bool throwOnNotFound)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (String.IsNullOrWhiteSpace(configMapName))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'configMapName'.", nameof(configMapName));
            
            _client = client;
            Log = _client.LoggerFactory.CreateLogger<ConfigMapConfigurationProvider>();
            _configMapName = configMapName;
            _kubeNamespace = kubeNamespace;
            _sectionName = sectionName;
            _watch = watch;
            _throwOnNotFound = throwOnNotFound;
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
        ///     Load configuration entries from the ConfigMap.
        /// </summary>
        public override void Load()
        {
            Log.LogTrace("Attempting to load ConfigMap {ConfigMapName} in namespace {KubeNamespace}...", _configMapName, _kubeNamespace ?? _client.DefaultNamespace);

            ConfigMapV1 configMap = _client.ConfigMapsV1().Get(_configMapName, _kubeNamespace).GetAwaiter().GetResult();
            Load(configMap);

            if (_watch && _watchSubscription == null)
            {
                Log.LogTrace("Creating watch-event stream for ConfigMap {ConfigMapName} in namespace {KubeNamespace}...", _configMapName, _kubeNamespace ?? _client.DefaultNamespace);

                _watchSubscription = _client.ConfigMapsV1()
                    .Watch(_configMapName, _kubeNamespace)
                    .Subscribe(OnConfigMapChanged);

                Log.LogTrace("Watch-event stream created for ConfigMap {ConfigMapName} in namespace {KubeNamespace}.", _configMapName, _kubeNamespace ?? _client.DefaultNamespace);
            }
        }

        /// <summary>
        ///     Load data from the specified ConfigMap.
        /// </summary>
        /// <param name="configMap">
        ///     A <see cref="ConfigMapV1"/> representing the ConfigMap's current state, or <c>null</c> if the ConfigMap was not found.
        /// </param>
        /// <param name="isReload">
        ///     Is the ConfigMap is being reloaded? If <c>false</c>, then this is the initial load (and may throw an exception).
        /// </param>
        void Load(ConfigMapV1 configMap, bool isReload = false)
        {
            if (configMap != null)
            {
                Log.LogTrace("Found ConfigMap {ConfigMapName} in namespace {KubeNamespace} (isReload: {isReload}).", _configMapName, _kubeNamespace ?? _client.DefaultNamespace, isReload);

                string sectionNamePrefix = !String.IsNullOrWhiteSpace(_sectionName) ? _sectionName + ":" : String.Empty;

                Data = configMap.Data.ToDictionary(
                    entry => sectionNamePrefix + entry.Key.Replace('.', ':'),
                    entry => entry.Value,
                    StringComparer.OrdinalIgnoreCase
                );
            }
            else
            {
                Data = new Dictionary<string, string>();
                
                Log.LogTrace("ConfigMap {ConfigMapName} was not found in namespace {KubeNamespace} (isReload: {isReload}).", _configMapName, _kubeNamespace ?? _client.DefaultNamespace, isReload);
                
                if (!isReload && _throwOnNotFound)
                    throw new KubeClientException($"ConfigMap {_configMapName} was not found in namespace {_kubeNamespace}.");
            }
        }

        /// <summary>
        ///     Called when the target ConfigMap is created, modified, or deleted.
        /// </summary>
        /// <param name="configMapEvent">
        ///     The change-notification event data.
        /// </param>
        private void OnConfigMapChanged(IResourceEventV1<ConfigMapV1> configMapEvent)
        {
            if (configMapEvent == null)
                throw new ArgumentNullException(nameof(configMapEvent));

            Log.LogTrace("Observed {EventType} watch-event for ConfigMap {ConfigMapName} in namespace {KubeNamespace}.", configMapEvent.EventType, _configMapName, _kubeNamespace ?? _client.DefaultNamespace);

            switch (configMapEvent.EventType)
            {
                case ResourceEventType.Deleted:
                {
                    // Clear out configuration if the ConfigMap is has been deleted.
                    Log.LogTrace("ConfigMap {ConfigMapName} in namespace {KubeNamespace} has been deleted.", _configMapName, _kubeNamespace ?? _client.DefaultNamespace);

                    Load(null, isReload: true);

                    break;
                }
                case ResourceEventType.Error:
                {
                    // Clear out configuration if the ConfigMap is missing or invalid.
                    Log.LogTrace("ConfigMap {ConfigMapName} in namespace {KubeNamespace} is currently in an invalid state.", _configMapName, _kubeNamespace ?? _client.DefaultNamespace);
                    Load(null, isReload: true);

                    break;
                }
                default:
                {
                    Load(configMapEvent.Resource, isReload: true);

                    break;
                }
            }

            Log.LogTrace("Triggering config change-token for ConfigMap {ConfigMapName} in namespace {KubeNamespace}...", _configMapName, _kubeNamespace ?? _client.DefaultNamespace);

            OnReload();

            Log.LogTrace("Config change-token triggered for ConfigMap {ConfigMapName} in namespace {KubeNamespace}.", _configMapName, _kubeNamespace ?? _client.DefaultNamespace);
        }
    }
}
