using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.ResourceClients
{
    using Http;
    using Models;

    /// <summary>
    ///     A client for the Kubernetes ConfigMaps (v1) API.
    /// </summary>
    public class ConfigMapClientV1
        : KubeResourceClient, IConfigMapClientV1
    {
        /// <summary>
        ///     Create a new <see cref="ConfigMapClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public ConfigMapClientV1(IKubeApiClient client)
            : base(client)
        {
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
            return await GetResourceList<ConfigMapListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to a specific ConfigMap.
        /// </summary>
        /// <param name="name">
        ///     The name of the ConfigMap to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<ConfigMapV1>> Watch(string name, string kubeNamespace = null)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return ObserveEvents<ConfigMapV1>(
                Requests.WatchByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                operationDescription: $"watch v1/ConfigMap '{name}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Watch for events relating to ConfigMaps.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ConfigMaps.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<ConfigMapV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<ConfigMapV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                }),
                operationDescription: $"watch all v1/ConfigMaps with label selector '{labelSelector ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
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
                .ReadContentAsObjectV1Async<ConfigMapV1>(
                    operationDescription: "create v1/ConfigMap resource"
                );
        }

        /// <summary>
        ///     Request update (PATCH) of a <see cref="ConfigMapV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target ConfigMap.
        /// </param>
        /// <param name="patchAction">
        ///     A delegate that customises the patch operation.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ConfigMapV1"/> representing the current state for the updated ConfigMap.
        /// </returns>
        public async Task<ConfigMapV1> Update(string name, Action<JsonPatchDocument<ConfigMapV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            if (patchAction == null)
                throw new ArgumentNullException(nameof(patchAction));

            return await PatchResource(patchAction,
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken
            );
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
                .ReadContentAsObjectV1Async<StatusV1>("delete v1/ConfigMap resource", HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the ConfigMap (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level ConfigMap (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = KubeRequest.Create("api/v1/namespaces/{Namespace}/configmaps?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A get-by-name ConfigMap (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = KubeRequest.Create("api/v1/namespaces/{Namespace}/configmaps/{Name}");

            /// <summary>
            ///     A watch-by-name ConfigMap (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchByName = KubeRequest.Create("api/v1/watch/namespaces/{Namespace}/configmaps/{Name}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes ConfigMaps (v1) API.
    /// </summary>
    public interface IConfigMapClientV1
        : IKubeResourceClient
    {
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
        Task<ConfigMapV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

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
        Task<ConfigMapListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to a specific ConfigMap.
        /// </summary>
        /// <param name="name">
        ///     The name of the ConfigMap to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<ConfigMapV1>> Watch(string name, string kubeNamespace = null);

        /// <summary>
        ///     Watch for events relating to ConfigMaps.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ConfigMaps.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<ConfigMapV1>> WatchAll(string labelSelector = null, string kubeNamespace = null);

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
        Task<ConfigMapV1> Create(ConfigMapV1 newConfigMap, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request update (PATCH) of a <see cref="ConfigMapV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target ConfigMap.
        /// </param>
        /// <param name="patchAction">
        ///     A delegate that customises the patch operation.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ConfigMapV1"/> representing the current state for the updated ConfigMap.
        /// </returns>
        Task<ConfigMapV1> Update(string name, Action<JsonPatchDocument<ConfigMapV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default);

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
        Task<StatusV1> Delete(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);
    }
}
