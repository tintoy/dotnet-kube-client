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
    ///     A client for the Kubernetes StatefulSet (v1) API.
    /// </summary>
    public class StatefulSetClientV1
        : KubeResourceClient, IStatefulSetClientV1
    {
        /// <summary>
        ///     Create a new <see cref="StatefulSetClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public StatefulSetClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the StatefulSet with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the StatefulSet to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="StatefulSetV1"/> representing the current state for the StatefulSet, or <c>null</c> if no StatefulSet was found with the specified name and namespace.
        /// </returns>
        public async Task<StatefulSetV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<StatefulSetV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all StatefulSets in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the StatefulSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="StatefulSetListV1"/> containing the StatefulSets.
        /// </returns>
        public async Task<StatefulSetListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<StatefulSetListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to StatefulSets.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the StatefulSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<StatefulSetV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<StatefulSetV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                }),
                operationDescription: $"watch all v1/StatefulSets with label selector '{labelSelector ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="StatefulSetV1"/>.
        /// </summary>
        /// <param name="newStatefulSet">
        ///     A <see cref="StatefulSetV1"/> representing the StatefulSet to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="StatefulSetV1"/> representing the current state for the newly-created StatefulSet.
        /// </returns>
        public async Task<StatefulSetV1> Create(StatefulSetV1 newStatefulSet, CancellationToken cancellationToken = default)
        {
            if (newStatefulSet == null)
                throw new ArgumentNullException(nameof(newStatefulSet));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newStatefulSet?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newStatefulSet,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<StatefulSetV1>("create v1/StatefulSet resource");
        }

        /// <summary>
        ///     Request update (PATCH) of a <see cref="StatefulSetV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target StatefulSet.
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
        ///     A <see cref="StatefulSetV1"/> representing the current state for the updated StatefulSet.
        /// </returns>
        public async Task<StatefulSetV1> Update(string name, Action<JsonPatchDocument<StatefulSetV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default)
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
        ///     Request deletion of the specified StatefulSet.
        /// </summary>
        /// <param name="name">
        ///     The name of the StatefulSet to delete.
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
        ///     A <see cref="StatefulSetV1"/> representing the StatefulSet's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public Task<KubeResourceResultV1<StatefulSetV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteResource<StatefulSetV1>(Requests.ByName, name, kubeNamespace, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the StatefulSets (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level StatefulSet (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection   = KubeRequest.Create("apis/apps/v1/namespaces/{Namespace}/statefulsets?labelSelector={LabelSelector?}&watch={Watch?}");

            /// <summary>
            ///     A get-by-name StatefulSet (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName       = KubeRequest.Create("apis/apps/v1/namespaces/{Namespace}/statefulsets/{Name}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes StatefulSets (v1) API.
    /// </summary>
    public interface IStatefulSetClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the StatefulSet with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the StatefulSet to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="StatefulSetV1"/> representing the current state for the StatefulSet, or <c>null</c> if no StatefulSet was found with the specified name and namespace.
        /// </returns>
        Task<StatefulSetV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all StatefulSets in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the StatefulSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="StatefulSetListV1"/> containing the StatefulSets.
        /// </returns>
        Task<StatefulSetListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to StatefulSets.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the StatefulSets.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<StatefulSetV1>> WatchAll(string labelSelector = null, string kubeNamespace = null);

        /// <summary>
        ///     Request creation of a <see cref="StatefulSetV1"/>.
        /// </summary>
        /// <param name="newStatefulSet">
        ///     A <see cref="StatefulSetV1"/> representing the StatefulSet to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="StatefulSetV1"/> representing the current state for the newly-created StatefulSet.
        /// </returns>
        Task<StatefulSetV1> Create(StatefulSetV1 newStatefulSet, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request update (PATCH) of a <see cref="StatefulSetV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target StatefulSet.
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
        ///     A <see cref="StatefulSetV1"/> representing the current state for the updated StatefulSet.
        /// </returns>
        Task<StatefulSetV1> Update(string name, Action<JsonPatchDocument<StatefulSetV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified StatefulSet.
        /// </summary>
        /// <param name="name">
        ///     The name of the StatefulSet to delete.
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
        ///     A <see cref="StatefulSetV1"/> representing the StatefulSet's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        Task<KubeResourceResultV1<StatefulSetV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
