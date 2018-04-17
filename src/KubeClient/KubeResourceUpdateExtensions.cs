using System;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient
{
    using Models;
    using ResourceClients;

    /// <summary>
    ///     Extension methods for updating Kubernetes resources.
    /// </summary>
    public static class KubeResourceUpdateExtensions
    {
        /// <summary>
        ///     Update the specified ConfigMap.
        /// </summary>
        /// <param name="client">
        ///     The <see cref="ConfigMapV1"/> resource client.
        /// </param>
        /// <param name="configMap">
        ///     A <see cref="ConfigMapV1"/> representing the new state for the ConfigMap.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="ConfigMapV1"/> representing the updated ConfigMap.
        /// </returns>
        /// <remarks>
        ///     Updates all mutable fields (if specified on <paramref name="configMap"/>).
        /// </remarks>
        public static Task<ConfigMapV1> Update(this ConfigMapClientV1 client, ConfigMapV1 configMap, CancellationToken cancellationToken = default)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (String.IsNullOrWhiteSpace(configMap?.Metadata?.Name))
                throw new ArgumentException("Cannot update a ConfigMap if its metadata does not specify a name.", nameof(configMap));

            if (String.IsNullOrWhiteSpace(configMap?.Metadata?.Namespace))
                throw new ArgumentException("Cannot update a ConfigMap if its metadata does not specify a namespace.", nameof(configMap));
            
            return client.Update(
                name: configMap.Metadata.Name,
                kubeNamespace: configMap.Metadata.Namespace,
                patchAction: patch =>
                {
                    if (configMap.Metadata.Labels != null)
                    {
                        patch.Replace(patchConfigMap => patchConfigMap.Metadata.Labels,
                            value: configMap.Metadata.Labels
                        );
                    }

                    if (configMap.Metadata.Annotations != null)
                    {
                        patch.Replace(patchConfigMap => patchConfigMap.Metadata.Annotations,
                            value: configMap.Metadata.Annotations
                        );
                    }

                    if (configMap.Data != null)
                    {
                        patch.Replace(patchConfigMap => patchConfigMap.Data,
                            value: configMap.Data
                        );
                    }
                },
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Update the specified Secret.
        /// </summary>
        /// <param name="client">
        ///     The <see cref="SecretV1"/> resource client.
        /// </param>
        /// <param name="secret">
        ///     A <see cref="SecretV1"/> representing the new state for the Secret.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the operation.
        /// </param>
        /// <returns>
        ///     A <see cref="SecretV1"/> representing the updated Secret.
        /// </returns>
        /// <remarks>
        ///     Updates all mutable fields (if specified on <paramref name="secret"/>).
        /// </remarks>
        public static Task<SecretV1> Update(this SecretClientV1 client, SecretV1 secret, CancellationToken cancellationToken = default)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (String.IsNullOrWhiteSpace(secret?.Metadata?.Name))
                throw new ArgumentException("Cannot update a Secret if its metadata does not specify a name.", nameof(secret));

            if (String.IsNullOrWhiteSpace(secret?.Metadata?.Namespace))
                throw new ArgumentException("Cannot update a Secret if its metadata does not specify a namespace.", nameof(secret));
            
            return client.Update(
                name: secret.Metadata.Name,
                kubeNamespace: secret.Metadata.Namespace,
                patchAction: patch =>
                {
                    if (secret.Metadata.Labels != null)
                    {
                        patch.Replace(patchSecret => patchSecret.Metadata.Labels,
                            value: secret.Metadata.Labels
                        );
                    }

                    if (secret.Metadata.Annotations != null)
                    {
                        patch.Replace(patchSecret => patchSecret.Metadata.Annotations,
                            value: secret.Metadata.Annotations
                        );
                    }

                    if (secret.Data != null)
                    {
                        patch.Replace(patchSecret => patchSecret.Data,
                            value: secret.Data
                        );
                    }
                },
                cancellationToken: cancellationToken
            );
        }
    }
}