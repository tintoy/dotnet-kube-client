using HTTPlease;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes Secrets (v1) API.
    /// </summary>
    public class SecretClientV1
        : KubeResourceClient
    {
        /// <summary>
        ///     Create a new <see cref="SecretClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public SecretClientV1(KubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get all Secrets in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Secrets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     The Secrets, as a list of <see cref="SecretV1"/>s.
        /// </returns>
        public async Task<List<SecretV1>> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            SecretListV1 matchingSecrets =
                await Http.GetAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = kubeNamespace ?? Client.DefaultNamespace,
                        LabelSelector = labelSelector
                    }),
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<SecretListV1, StatusV1>();

            return matchingSecrets.Items;
        }

        /// <summary>
        ///     Get the Secret with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Secret to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="SecretV1"/> representing the current state for the Secret, or <c>null</c> if no Secret was found with the specified name and namespace.
        /// </returns>
        public async Task<SecretV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<SecretV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? Client.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="Secret"/>.
        /// </summary>
        /// <param name="newSecret">
        ///     A <see cref="SecretV1"/> representing the Secret to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="SecretV1"/> representing the current state for the newly-created Secret.
        /// </returns>
        public async Task<SecretV1> Create(SecretV1 newSecret, CancellationToken cancellationToken = default)
        {
            if (newSecret == null)
                throw new ArgumentNullException(nameof(newSecret));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newSecret?.Metadata?.Namespace ?? Client.DefaultNamespace
                    }),
                    postBody: newSecret,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<SecretV1, StatusV1>();
        }

        /// <summary>
        ///     Request deletion of the specified Secret.
        /// </summary>
        /// <param name="name">
        ///     The name of the Secret to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     An <see cref="StatusV1"/> indicating the result of the request.
        /// </returns>
        public async Task<StatusV1> Delete(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await Http
                .DeleteAsync(
                    Requests.ByName.WithTemplateParameters(new
                    {
                        Name = name,
                        Namespace = kubeNamespace ?? Client.DefaultNamespace
                    }),
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<StatusV1, StatusV1>(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the Secret (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level Secret (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/secrets?labelSelector={LabelSelector?}", SerializerSettings);

            /// <summary>
            ///     A get-by-name Secret (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/secrets/{Name}", SerializerSettings);
        }
    }
}
