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
    ///     A client for the Kubernetes ReplicationControllers (v1) API.
    /// </summary>
    public class ReplicationControllerClientV1
        : KubeResourceClient, IReplicationControllerClientV1
    {
        /// <summary>
        ///     Create a new <see cref="ReplicationControllerClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public ReplicationControllerClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the ReplicationController with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the ReplicationController to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicationControllerV1"/> representing the current state for the ReplicationController, or <c>null</c> if no ReplicationController was found with the specified name and namespace.
        /// </returns>
        public async Task<ReplicationControllerV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<ReplicationControllerV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all ReplicationControllers in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ReplicationControllers.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicationControllerListV1"/> containing the ReplicationControllers.
        /// </returns>
        public async Task<ReplicationControllerListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<ReplicationControllerListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to ReplicationControllers.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ReplicationControllers.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<ReplicationControllerV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<ReplicationControllerV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                }),
                operationDescription: $"watch all v1/ReplicationControllers with label selector '{labelSelector ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="ReplicationControllerV1"/>.
        /// </summary>
        /// <param name="newController">
        ///     A <see cref="ReplicationControllerV1"/> representing the ReplicationController to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicationControllerV1"/> representing the current state for the newly-created ReplicationController.
        /// </returns>
        public async Task<ReplicationControllerV1> Create(ReplicationControllerV1 newController, CancellationToken cancellationToken = default)
        {
            if (newController == null)
                throw new ArgumentNullException(nameof(newController));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newController?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newController,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<ReplicationControllerV1>(
                    operationDescription: $"create v1/ReplicationController resource in namespace '{newController?.Metadata?.Namespace ?? KubeClient.DefaultNamespace}'"
                );
        }

        /// <summary>
        ///     Request update (PATCH) of a <see cref="ReplicationControllerV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target ReplicationController.
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
        ///     A <see cref="ReplicationControllerV1"/> representing the current state for the updated ReplicationController.
        /// </returns>
        public async Task<ReplicationControllerV1> Update(string name, Action<JsonPatchDocument<ReplicationControllerV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default)
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
        ///     Request deletion of the specified ReplicationController.
        /// </summary>
        /// <param name="name">
        ///     The name of the ReplicationController to delete.
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
        ///     A <see cref="ReplicationControllerV1"/> representing the replication controller's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public Task<KubeResourceResultV1<ReplicationControllerV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteResource<ReplicationControllerV1>(Requests.ByName, name, kubeNamespace, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the ReplicationController (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level ReplicationController (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection   = KubeRequest.Create("api/v1/namespaces/{Namespace}/replicationcontrollers?labelSelector={LabelSelector?}&watch={Watch?}");

            /// <summary>
            ///     A get-by-name ReplicationController (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName       = KubeRequest.Create("api/v1/namespaces/{Namespace}/replicationcontrollers/{Name}");
        }
    }

    /// <summary>
    ///     A client for the Kubernetes ReplicationControllers (v1) API.
    /// </summary>
    public interface IReplicationControllerClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the ReplicationController with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the ReplicationController to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicationControllerV1"/> representing the current state for the ReplicationController, or <c>null</c> if no ReplicationController was found with the specified name and namespace.
        /// </returns>
        Task<ReplicationControllerV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all ReplicationControllers in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ReplicationControllers.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicationControllerListV1"/> containing the ReplicationControllers.
        /// </returns>
        Task<ReplicationControllerListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);
        
        /// <summary>
        ///     Watch for events relating to ReplicationControllers.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ReplicationControllers.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<ReplicationControllerV1>> WatchAll(string labelSelector = null, string kubeNamespace = null);

        /// <summary>
        ///     Request creation of a <see cref="ReplicationControllerV1"/>.
        /// </summary>
        /// <param name="newReplicationController">
        ///     A <see cref="ReplicationControllerV1"/> representing the ReplicationController to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ReplicationControllerV1"/> representing the current state for the newly-created ReplicationController.
        /// </returns>
        Task<ReplicationControllerV1> Create(ReplicationControllerV1 newReplicationController, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request update (PATCH) of a <see cref="ReplicationControllerV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target ReplicationController.
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
        ///     A <see cref="ReplicationControllerV1"/> representing the current state for the updated ReplicationController.
        /// </returns>
        Task<ReplicationControllerV1> Update(string name, Action<JsonPatchDocument<ReplicationControllerV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified ReplicationController.
        /// </summary>
        /// <param name="name">
        ///     The name of the ReplicationController to delete.
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
        ///     A <see cref="ReplicationControllerV1"/> representing the replication controller's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        Task<KubeResourceResultV1<ReplicationControllerV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
