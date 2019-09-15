using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace KubeClient.Extensions.DataProtection
{
    using Models;

    /// <summary>
    /// An <see cref="IXmlRepository"/> implementation that uses a Kubernetes Secret to store data-protection keys.
    /// </summary>
    public sealed class KubeSecretXmlRepository
        : IXmlRepository, IDisposable
    {
        /// <summary>
        /// <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </summary>
        readonly IKubeApiClient _client;

        /// <summary>
        ///  The name of the target Secret.
        /// </summary>
        readonly string _secretName;

        /// <summary>
        ///  The namespace of the target Secret.
        /// </summary>
        readonly string _kubeNamespace;

        /// <summary>
        /// A <see cref="SecretV1"/> representing the Secret used to store the XML keys.
        /// </summary>
        SecretV1 _keyManagementSecret;

        /// <summary>
        /// An <see cref="IDisposable"/> representing the subscription to change notifications for the key-management secret.
        /// </summary>
        IDisposable _watchSubscription;

        /// <summary>
        /// An XML repository backed by KubeClient.
        /// </summary>
        /// <param name="client">
        /// <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </param>
        /// <param name="secretName">
        ///  The name of the target Secret.
        /// </param>
        /// <param name="kubeNamespace">
        ///  The namespace of the target Secret.
        /// </param>
        public KubeSecretXmlRepository(IKubeApiClient client, string secretName, string kubeNamespace = null)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (String.IsNullOrEmpty(secretName))
                throw new ArgumentNullException(nameof(secretName));

            _client = client;
            _secretName = secretName;
            _kubeNamespace = kubeNamespace;

            Log = client.LoggerFactory.CreateLogger<KubeSecretXmlRepository>();

            // Init
            LoadOrCreateSecret().GetAwaiter().GetResult();
            AttachWatcher();
        }

        /// <summary>
        /// Dispose of resources being used by the <see cref="KubeSecretXmlRepository"/>.
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
        ///     The logger for the secret repository.
        /// </summary>
        ILogger Log { get; }

        /// <summary>
        /// Implement <see cref="IXmlRepository"/>
        /// </summary>
        public IReadOnlyCollection<XElement> GetAllElements()
        {
            return GetAllElementsCore().ToArray();
        }

        /// <summary>
        /// Implement <see cref="IXmlRepository"/>
        /// </summary>
        public void StoreElement(XElement element, string friendlyName)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (String.IsNullOrWhiteSpace(friendlyName))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'friendlyName'.", nameof(friendlyName));

            // Convert to XML String
            string xmlString = element.ToString(SaveOptions.DisableFormatting);

            // Convert to Base64 String
            string base64String = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(xmlString)
            );

            // Add XML File Extension to allow others File-Mapping
            if (String.IsNullOrEmpty(Path.GetExtension(friendlyName)))
            {
                friendlyName += ".xml";
            }

            // AF: Currently, this implementation is not thread-safe (because the change-notification handler may replace the secret while this code is running).

            // Add to Data
            _keyManagementSecret.Data[friendlyName] = base64String;

            // Patch the Secret
            _client.SecretsV1().Update(_secretName, (patch) =>
            {
                patch.Replace(e => e.Data, _keyManagementSecret.Data);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Get all Elements from Repository 
        /// </summary>
        /// <returns>A sequence of <see cref="XElement"/>s.</returns>
        public IEnumerable<XElement> GetAllElementsCore()
        {
            // AF: Currently, this implementation is not thread-safe (because the change-notification handler may replace the secret while this code is running).

            foreach (string keyName in _keyManagementSecret.Data.Keys)
            {
                string encodedKeyXml = _keyManagementSecret.Data[keyName];

                // Convert from Base64 to XMLString
                string keyXmlText;

                try
                {
                    keyXmlText = Encoding.UTF8.GetString(
                        Convert.FromBase64String(encodedKeyXml)
                    );
                }
                catch (ArgumentException cannotDecodeUtf8String)
                {
                    Log.LogError(cannotDecodeUtf8String, "Unable to decode UTF8-encoded data for key XML {KeyName} in secret {SecretName} (namespace {SecretNamespace}); this key will be ignored.",
                        keyName, _keyManagementSecret.Metadata.Name, _keyManagementSecret.Metadata.Namespace
                    );

                    continue;
                }
                catch (FormatException cannotDecodeBase64String)
                {
                    Log.LogError(cannotDecodeBase64String, "Unable to decode Base64-encoded data for key XML {KeyName} in secret {SecretName} (namespace {SecretNamespace}); this key will be ignored.",
                        keyName, _keyManagementSecret.Metadata.Name, _keyManagementSecret.Metadata.Namespace
                    );

                    continue;
                }
                catch (Exception unexpectedError)
                {
                    Log.LogError(unexpectedError, "An unexpected error occurred while decoding data for key XML {KeyName} in secret {SecretName} (namespace {SecretNamespace}); this key will be ignored.",
                        keyName, _keyManagementSecret.Metadata.Name, _keyManagementSecret.Metadata.Namespace
                    );

                    continue;
                }

                XElement keyXml;

                try
                {
                    keyXml = XElement.Parse(keyXmlText);
                }
                catch (XmlException invalidKeyXml)
                {
                    Log.LogError(invalidKeyXml, "Unable to parse XML for key {KeyName} in secret {SecretName} (namespace {SecretNamespace}); this key will be ignored.",
                        keyName, _keyManagementSecret.Metadata.Name, _keyManagementSecret.Metadata.Namespace
                    );

                    continue;
                }

                yield return keyXml;
            }
        }

        /// <summary>
        /// Load or Create the Kubernetes Secret
        /// </summary>
        async Task LoadOrCreateSecret()
        {
            // Try to get the Secret
            Log.LogDebug("Attempting to load Secret {SecretName} in namespace {SecretNamespace} for persistence of data-protection keys...",
                _secretName, _kubeNamespace
            );

            SecretV1 secret = await _client.SecretsV1().Get(_secretName, _kubeNamespace).ConfigureAwait(false);
            if (secret == null)
            {
                // Create a new Secret
                Log.LogDebug("Secret {SecretName} was not found in namespace {SecretNamespace} for persistence of data-protection keys; creating...",
                    _secretName, _kubeNamespace
                );

                secret = await _client.SecretsV1().Create(new SecretV1
                {
                    Metadata = new ObjectMetaV1
                    {
                        Name = _secretName,
                        Namespace = _kubeNamespace
                    }
                }).ConfigureAwait(false);

                Log.LogDebug("Successfully created Secret {SecretName} in namespace {SecretNamespace} for persistence of data-protection keys.",
                    _keyManagementSecret.Metadata.Name, _keyManagementSecret.Metadata.Namespace
                );
            }
            else
            {
                Log.LogDebug("Successfully loaded Secret {SecretName} in namespace {SecretNamespace} for persistence of data-protection keys.",
                    _keyManagementSecret.Metadata.Name, _keyManagementSecret.Metadata.Namespace
                );
            }

            // Use the Secret
            _keyManagementSecret = secret;
        }

        /// <summary>
        /// Attach the Watcher to the Secret for Changes
        /// </summary>
        void AttachWatcher()
        {
            _watchSubscription = _client.SecretsV1()
                .Watch(_secretName, _kubeNamespace)
                .Subscribe(OnKeyManagementSecretChanged);
        }

        /// <summary>
        /// Called when the key-management secret has changed.
        /// </summary>
        /// <param name="secretEvent">
        /// An <see cref="IResourceEventV1{TResource}"/> containing information about the changed Secret.
        /// </param>
        /// <remarks>If the Secret <see cref="ResourceEventType"/> is Modified, the internal properties will be reset.</remarks>
        void OnKeyManagementSecretChanged(IResourceEventV1<SecretV1> secretEvent)
        {
            if (secretEvent == null)
                throw new ArgumentNullException(nameof(secretEvent));

            if (secretEvent.Resource == null)
                throw new ArgumentNullException(nameof(secretEvent.Resource));

            // AF: Currently, this implementation is not thread-safe (because we may read or modify the secret while this handler is running).

            if (secretEvent.EventType == ResourceEventType.Modified)
            {
                // Attach the changed Secret
                _keyManagementSecret = secretEvent.Resource;
            }

            // AF: What happens if the secret has been deleted?
        }
    }
}
