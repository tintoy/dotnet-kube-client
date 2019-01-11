using HTTPlease;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes ReplicaSets (v1) API.
    /// </summary>
    public class ReplicaSetClientV1
        : KubeResourceClient, IReplicaSetClientV1
    {
        /// <summary>
        ///     Create a new <see cref="ReplicaSetClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public ReplicaSetClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the ReplicaSet with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the ReplicaSet to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicaSetV1"/> representing the current state for the ReplicaSet, or <c>null</c> if no ReplicaSet was found with the specified name and namespace.
        /// </returns>
        public async Task<ReplicaSetV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<ReplicaSetV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all ReplicaSets in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ReplicaSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicaSetListV1"/> containing the ReplicaSets.
        /// </returns>
        public async Task<ReplicaSetListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<ReplicaSetListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to ReplicaSets.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ReplicaSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<ReplicaSetV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<ReplicaSetV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                }),
                operationDescription: $"watch all v1/ReplicaSets with label selector '{labelSelector ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="ReplicaSetV1"/>.
        /// </summary>
        /// <param name="newReplicaSet">
        ///     A <see cref="ReplicaSetV1"/> representing the ReplicaSet to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicaSetV1"/> representing the current state for the newly-created ReplicaSet.
        /// </returns>
        public async Task<ReplicaSetV1> Create(ReplicaSetV1 newReplicaSet, CancellationToken cancellationToken = default)
        {
            if (newReplicaSet == null)
                throw new ArgumentNullException(nameof(newReplicaSet));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newReplicaSet?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newReplicaSet,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<ReplicaSetV1>(
                    operationDescription: $"create v1/ReplicaSet in namespace '{newReplicaSet?.Metadata?.Namespace ?? KubeClient.DefaultNamespace}'"
                );
        }

        /// <summary>
        ///     Request update (PATCH) of a <see cref="ReplicaSetV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target ReplicaSet.
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
        ///     A <see cref="ReplicaSetV1"/> representing the current state for the updated ReplicaSet.
        /// </returns>
        public async Task<ReplicaSetV1> Update(string name, Action<JsonPatchDocument<ReplicaSetV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default)
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
        ///     Request deletion of the specified ReplicaSet.
        /// </summary>
        /// <param name="name">
        ///     The name of the ReplicaSet to delete.
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
        ///     A <see cref="ReplicaSetV1"/> representing the replica set's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public Task<KubeResourceResultV1<ReplicaSetV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteResource<ReplicaSetV1>(Requests.ByName, name, kubeNamespace, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the ReplicaSet (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level ReplicaSet (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection   = KubeRequest.Create("/apis/extensions/v1beta1/namespaces/{Namespace}/replicasets?labelSelector={LabelSelector?}&watch={Watch?}");

            /// <summary>
            ///     A get-by-name ReplicaSet (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName       = KubeRequest.Create("/apis/extensions/v1beta1/namespaces/{Namespace}/replicasets/{Name}");
        }
    }

    /// <summary>
    ///     A client for the Kubernetes ReplicaSets (v1beta1) API.
    /// </summary>
    public interface IReplicaSetClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the ReplicaSet with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the ReplicaSet to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicaSetV1"/> representing the current state for the ReplicaSet, or <c>null</c> if no ReplicaSet was found with the specified name and namespace.
        /// </returns>
        Task<ReplicaSetV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all ReplicaSets in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ReplicaSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicaSetListV1"/> containing the ReplicaSets.
        /// </returns>
        Task<ReplicaSetListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        ///     Watch for events relating to ReplicaSets.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ReplicaSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<ReplicaSetV1>> WatchAll(string labelSelector = null, string kubeNamespace = null);

        /// <summary>
        ///     Request creation of a <see cref="ReplicaSetV1"/>.
        /// </summary>
        /// <param name="newReplicaSet">
        ///     A <see cref="ReplicaSetV1"/> representing the ReplicaSet to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicaSetV1"/> representing the current state for the newly-created ReplicaSet.
        /// </returns>
        Task<ReplicaSetV1> Create(ReplicaSetV1 newReplicaSet, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request update (PATCH) of a <see cref="ReplicaSetV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target ReplicaSet.
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
        ///     A <see cref="ReplicaSetV1"/> representing the current state for the updated ReplicaSet.
        /// </returns>
        Task<ReplicaSetV1> Update(string name, Action<JsonPatchDocument<ReplicaSetV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified ReplicaSet.
        /// </summary>
        /// <param name="name">
        ///     The name of the ReplicaSet to delete.
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
        ///     A <see cref="ReplicaSetV1"/> representing the replica set's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        Task<KubeResourceResultV1<ReplicaSetV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
