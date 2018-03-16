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
    ///     A client for the Kubernetes ConfigMaps (v1) API.
    /// </summary>
    public class ConfigMapClientV1
        : KubeResourceClient
    {
        /// <summary>
        ///     Create a new <see cref="ConfigMapClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public ConfigMapClientV1(KubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get all ConfigMaps in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ConfigMaps.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ConfigMapListV1"/> containing the ConfigMaps.
        /// </returns>
        public async Task<ConfigMapListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<ConfigMapListV1, ConfigMapV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get the ConfigMap with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the ConfigMap to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ConfigMapV1"/> representing the current state for the ConfigMap, or <c>null</c> if no ConfigMap was found with the specified name and namespace.
        /// </returns>
        public async Task<ConfigMapV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<ConfigMapV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="ConfigMapV1"/>.
        /// </summary>
        /// <param name="newConfigMap">
        ///     A <see cref="ConfigMapV1"/> representing the ConfigMap to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ConfigMapV1"/> representing the current state for the newly-created ConfigMap.
        /// </returns>
        public async Task<ConfigMapV1> Create(ConfigMapV1 newConfigMap, CancellationToken cancellationToken = default)
        {
            if (newConfigMap == null)
                throw new ArgumentNullException(nameof(newConfigMap));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newConfigMap?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newConfigMap,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<ConfigMapV1, StatusV1>();
        }

        /// <summary>
        ///     Request deletion of the specified ConfigMap.
        /// </summary>
        /// <param name="name">
        ///     The name of the ConfigMap to delete.
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
                        Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                    }),
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<StatusV1, StatusV1>(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the ConfigMap (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level ConfigMap (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/configmaps?labelSelector={LabelSelector?}", SerializerSettings);

            /// <summary>
            ///     A get-by-name ConfigMap (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/configmaps/{Name}", SerializerSettings);
        }
    }
}
