using HTTPlease;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes DaemonSets (v1) API.
    /// </summary>
    public class DaemonSetClientV1
        : KubeResourceClient, IDaemonSetClientV1
    {
        /// <summary>
        ///     Create a new <see cref="DaemonSetClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public DaemonSetClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the DaemonSet with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the DaemonSet to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DaemonSetV1"/> representing the current state for the DaemonSet, or <c>null</c> if no DaemonSet was found with the specified name and namespace.
        /// </returns>
        public async Task<DaemonSetV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<DaemonSetV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all DaemonSets in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the DaemonSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DaemonSetListV1"/> containing the DaemonSets.
        /// </returns>
        public async Task<DaemonSetListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<DaemonSetListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to DaemonSets.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the DaemonSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<DaemonSetV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<DaemonSetV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                }),
                operationDescription: $"watch all v1/DaemonSets with label selector '{labelSelector ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="DaemonSetV1"/>.
        /// </summary>
        /// <param name="newDaemonSet">
        ///     A <see cref="DaemonSetV1"/> representing the DaemonSet to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DaemonSetV1"/> representing the current state for the newly-created DaemonSet.
        /// </returns>
        public async Task<DaemonSetV1> Create(DaemonSetV1 newDaemonSet, CancellationToken cancellationToken = default)
        {
            if (newDaemonSet == null)
                throw new ArgumentNullException(nameof(newDaemonSet));

            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newDaemonSet?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newDaemonSet,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<DaemonSetV1>("create v1/DaemonSet resource");
        }

        /// <summary>
        ///     Request update (PATCH) of a <see cref="DaemonSetV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target DaemonSet.
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
        ///     A <see cref="DaemonSetV1"/> representing the current state for the updated DaemonSet.
        /// </returns>
        public async Task<DaemonSetV1> Update(string name, Action<JsonPatchDocument<DaemonSetV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default)
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
        ///     Request deletion of the specified DaemonSet.
        /// </summary>
        /// <param name="name">
        ///     The name of the DaemonSet to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="propagationPolicy">
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DaemonSetV1"/> representing the DaemonSet's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public Task<KubeResourceResultV1<DaemonSetV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteResource<DaemonSetV1>(Requests.ByName, name, kubeNamespace, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the DaemonSets (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level DaemonSet (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = KubeRequest.Create("apis/apps/v1/namespaces/{Namespace}/daemonsets?labelSelector={LabelSelector?}&watch={Watch?}");

            /// <summary>
            ///     A get-by-name DaemonSet (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = KubeRequest.Create("apis/apps/v1/namespaces/{Namespace}/daemonsets/{Name}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes DaemonSets (v1) API.
    /// </summary>
    public interface IDaemonSetClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the DaemonSet with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the DaemonSet to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DaemonSetV1"/> representing the current state for the DaemonSet, or <c>null</c> if no DaemonSet was found with the specified name and namespace.
        /// </returns>
        Task<DaemonSetV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all DaemonSets in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the DaemonSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DaemonSetListV1"/> containing the DaemonSets.
        /// </returns>
        Task<DaemonSetListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to DaemonSets.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the DaemonSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<DaemonSetV1>> WatchAll(string labelSelector = null, string kubeNamespace = null);

        /// <summary>
        ///     Request creation of a <see cref="DaemonSetV1"/>.
        /// </summary>
        /// <param name="newDaemonSet">
        ///     A <see cref="DaemonSetV1"/> representing the DaemonSet to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DaemonSetV1"/> representing the current state for the newly-created DaemonSet.
        /// </returns>
        Task<DaemonSetV1> Create(DaemonSetV1 newDaemonSet, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request update (PATCH) of a <see cref="DaemonSetV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target DaemonSet.
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
        ///     A <see cref="DaemonSetV1"/> representing the current state for the updated DaemonSet.
        /// </returns>
        Task<DaemonSetV1> Update(string name, Action<JsonPatchDocument<DaemonSetV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified DaemonSet.
        /// </summary>
        /// <param name="name">
        ///     The name of the DaemonSet to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="propagationPolicy">
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="DaemonSetV1"/> representing the DaemonSet's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        Task<KubeResourceResultV1<DaemonSetV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}