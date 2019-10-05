using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KubeClient
{
    using Extensions.DataProtection;

    /// <summary>
    ///     <see cref="IDataProtectionBuilder"/> extension methods to persist Keys in a Kubernetes Secret.
    /// </summary>
    public static class DataProtectionExtensions
    {
        /// <summary>
        /// Add or Create a Kubernetes Secret as a Repository. 
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IDataProtectionBuilder"/> to Configure.
        /// </param>
        /// <param name="clientOptions">
        /// <see cref="KubeClientOptions"/> for the <see cref="KubeApiClient"/> used to communicate with the Kubernetes API.
        /// </param>
        /// <param name="secretName">
        ///  The name of the target Secret.
        /// </param>
        /// <param name="kubeNamespace">
        ///  The namespace of the target Secret.
        /// </param>
        /// <returns>
        /// The configured <see cref="IDataProtectionBuilder"/>.
        /// </returns>
        public static IDataProtectionBuilder PersistKeysToKubeSecret(this IDataProtectionBuilder builder, KubeClientOptions clientOptions, string secretName, string kubeNamespace = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (clientOptions == null)
                throw new ArgumentNullException(nameof(clientOptions));

            if (String.IsNullOrWhiteSpace(secretName))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(secretName)}.", nameof(secretName));

            builder.Services.Configure<KeyManagementOptions>(options =>
            {
                KubeApiClient client = KubeApiClient.Create(clientOptions);

                // Persist secret data in the target K8s secret.
                options.XmlRepository = new KubeSecretXmlRepository(client, secretName, kubeNamespace ?? client.DefaultNamespace);
            });

            return builder;
        }

        /// <summary>
        /// Add or Create a Kubernetes Secret as a Repository. 
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IDataProtectionBuilder"/> to Configure.
        /// </param>
        /// <param name="client">
        /// The <see cref="IKubeApiClient"/> used to communicate with the Kubernetes API.
        /// </param>
        /// <param name="secretName">
        ///  The name of the target Secret.
        /// </param>
        /// <param name="kubeNamespace">
        ///  The namespace of the target Secret.
        /// </param>
        /// <returns>
        /// The configured <see cref="IDataProtectionBuilder"/>.
        /// </returns>
        public static IDataProtectionBuilder PersistKeysToKubeSecret(this IDataProtectionBuilder builder, IKubeApiClient client, string secretName, string kubeNamespace = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (String.IsNullOrWhiteSpace(secretName))
                throw new ArgumentException($"Argument cannot be null, empty, or entirely composed of whitespace: {nameof(secretName)}.", nameof(secretName));

            builder.Services.Configure<KeyManagementOptions>(options =>
            {
                // Persist secret data in the target K8s secret.
                options.XmlRepository = new KubeSecretXmlRepository(client, secretName, kubeNamespace ?? client.DefaultNamespace);
            });

            return builder;
        }
    }
}
