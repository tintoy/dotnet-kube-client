using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
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
    ///     An <see cref="IXmlRepository"/> implementation that uses a Kubernetes Secret to store data-protection keys.
    /// </summary>
    public sealed class KubeSecretXmlRepository
        : IXmlRepository, IDisposable
    {
        /// <summary>
        ///     The default "friendly" name used for top-level elements in the repository when a friendly name is not supplied.
        /// </summary>
        public static readonly string DefaultElementFriendlyName = "KeyElement";

        /// <summary>
        ///     An async-friendly lock used to synchronise access to repository state.
        /// </summary>
        readonly AsyncLock _stateLock = new AsyncLock();

        /// <summary>
        ///     <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </summary>
        readonly IKubeApiClient _client;

        /// <summary>
        ///     The name of the target Secret.
        /// </summary>
        readonly string _secretName;

        /// <summary>
        ///     The namespace of the target Secret.
        /// </summary>
        readonly string _kubeNamespace;

        /// <summary>
        ///     A <see cref="SecretV1"/> representing the Secret used to store the XML keys.
        /// </summary>
        SecretV1 _keyManagementSecret;

        /// <summary>
        ///     An <see cref="IDisposable"/> representing the subscription to change notifications for the key-management secret.
        /// </summary>
        IDisposable _watchSubscription;

        /// <summary>
        ///     An XML repository backed by KubeClient.
        /// </summary>
        /// <param name="client">
        ///     An <see cref="IKubeApiClient"/> used to communicate with the Kubernetes API.
        /// </param>
        /// <param name="secretName">
        ///     The name of the target Secret.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The namespace of the target Secret.
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
        ///     Dispose of resources being used by the <see cref="KubeSecretXmlRepository"/>.
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
        ///     Get all top-level XML elements in the repository.
        /// </summary>
        /// <returns>
        ///     A sequence of <see cref="XElement"/>s representing the top-level elements.
        /// </returns>
        public IReadOnlyCollection<XElement> GetAllElements() => GetAllElementsCore().ToArray();

        /// <summary>
        ///     Add a top-level XML element to the repository.
        /// </summary>
        /// <param name="element">
        ///     An <see cref="XElement"/> representing the element to add.
        /// </param>
        /// <param name="friendlyName">
        ///     An optional name to be associated with the XML element.
        /// </param>
        /// <remarks>
        ///     Element friendly names must be unique per Secret.
        /// </remarks>
        public void StoreElement(XElement element, string friendlyName) => StoreElementCore(element, friendlyName).GetAwaiter().GetResult();

        /// <summary>
        ///     Load or Create the Kubernetes Secret used for persistence.
        /// </summary>
        async Task LoadOrCreateSecret()
        {
            using (await _stateLock.LockAsync())
            {
                Log.LogDebug("Attempting to load Secret {SecretName} in namespace {SecretNamespace} for persistence of data-protection keys...",
                    _secretName, _kubeNamespace
                );

                SecretV1 secret = await _client.SecretsV1().Get(_secretName, _kubeNamespace);
                if (secret == null)
                {
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
                    });

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

                _keyManagementSecret = secret;
            }
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
        ///     Called by the watch subscription when the key-management secret has changed.
        /// </summary>
        /// <param name="secretEvent">
        ///     An <see cref="IResourceEventV1{TResource}"/> containing information about the changed Secret.
        /// </param>
        /// <remarks>
        ///     If the Secret <see cref="ResourceEventType"/> is Modified, the internal properties will be reset.
        /// </remarks>
        void OnKeyManagementSecretChanged(IResourceEventV1<SecretV1> secretEvent)
        {
            if (secretEvent == null)
                throw new ArgumentNullException(nameof(secretEvent));

            if (secretEvent.Resource == null)
                throw new ArgumentNullException(nameof(secretEvent.Resource));

            switch (secretEvent.EventType)
            {
                case ResourceEventType.Added:
                case ResourceEventType.Modified:
                {
                    // Attach the changed Secret
                    using (_stateLock.Lock())
                    {
                        _keyManagementSecret = secretEvent.Resource;
                    }

                    break;
                }
                case ResourceEventType.Deleted:
                {
                    // TODO: How do we handle the underlying Secret being deleted?

                    Log.LogWarning("Secret {SecretName} in namespace {SecretNamespace} (which is used for persistence of data-protection key material) has been deleted.", _secretName, _kubeNamespace);

                    break;
                }
            }
        }

        /// <summary>
        ///     Add a top-level XML element to the repository.
        /// </summary>
        /// <param name="element">
        ///     An <see cref="XElement"/> representing the element to add.
        /// </param>
        /// <param name="friendlyName">
        ///     An optional name to be associated with the XML element.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        /// <remarks>
        ///     Element friendly names must be unique per Secret.
        /// </remarks>
        async Task StoreElementCore(XElement element, string friendlyName)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (String.IsNullOrWhiteSpace(friendlyName))
                friendlyName = DefaultElementFriendlyName;

            // Convert to XML String
            string elementXml = element.ToString(SaveOptions.DisableFormatting);

            // Convert to Base64 String
            string encodedElementXml = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(elementXml)
            );

            // Add a file extension to simplify mounting of Secret as a volume (some consumers may want to do this).
            const string xmlExtension = ".xml";
            if (!String.Equals(Path.GetExtension(friendlyName), xmlExtension, StringComparison.OrdinalIgnoreCase))
                friendlyName += xmlExtension;

            using (await _stateLock.LockAsync())
            {
                _keyManagementSecret.Data[friendlyName] = encodedElementXml;

                await _client.SecretsV1().Update(_secretName, patch =>
                {
                    patch.Replace(secret => secret.Data, _keyManagementSecret.Data);
                });
            }
        }

        /// <summary>
        /// Get all top-level elements from the repository 
        /// </summary>
        /// <returns>A sequence of <see cref="XElement"/>s.</returns>
        IEnumerable<XElement> GetAllElementsCore()
        {
            // Use a snapshot of the underlying secret data so we don't tie up the state lock while consumers are enumerating elements.
            (string elementFriendlyName, string encodedElementXml)[] elements;

            using (_stateLock.Lock())
            {
                elements = _keyManagementSecret.Data
                    .Select(
                        item => (elementFriendlyName: item.Key, encodedElementXml: item.Value)
                    )
                    .ToArray();
            }

            foreach (string elementFriendlyName in _keyManagementSecret.Data.Keys)
            {
                string encodedElementXml = _keyManagementSecret.Data[elementFriendlyName];

                // Convert from Base64 to XMLString
                string elementXml;

                try
                {
                    elementXml = Encoding.UTF8.GetString(
                        Convert.FromBase64String(encodedElementXml)
                    );
                }
                catch (ArgumentException cannotDecodeUtf8String)
                {
                    Log.LogError(cannotDecodeUtf8String, "Unable to decode UTF8-encoded data for key XML {KeyName} in secret {SecretName} (namespace {SecretNamespace}); this key will be ignored.",
                        elementFriendlyName, _keyManagementSecret.Metadata.Name, _keyManagementSecret.Metadata.Namespace
                    );

                    continue;
                }
                catch (FormatException cannotDecodeBase64String)
                {
                    Log.LogError(cannotDecodeBase64String, "Unable to decode Base64-encoded data for key XML {KeyName} in secret {SecretName} (namespace {SecretNamespace}); this key will be ignored.",
                        elementFriendlyName, _keyManagementSecret.Metadata.Name, _keyManagementSecret.Metadata.Namespace
                    );

                    continue;
                }
                catch (Exception unexpectedError)
                {
                    Log.LogError(unexpectedError, "An unexpected error occurred while decoding data for key XML {KeyName} in secret {SecretName} (namespace {SecretNamespace}); this key will be ignored.",
                        elementFriendlyName, _keyManagementSecret.Metadata.Name, _keyManagementSecret.Metadata.Namespace
                    );

                    continue;
                }

                XElement element;

                try
                {
                    element = XElement.Parse(elementXml);
                }
                catch (XmlException invalidKeyXml)
                {
                    Log.LogError(invalidKeyXml, "Unable to parse XML for key {KeyName} in secret {SecretName} (namespace {SecretNamespace}); this key will be ignored.",
                        elementFriendlyName, _keyManagementSecret.Metadata.Name, _keyManagementSecret.Metadata.Namespace
                    );

                    continue;
                }

                yield return element;
            }
        }
    }
}
