using HTTPlease;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Microsoft.AspNetCore.JsonPatch;
    using Models;

    /// <summary>
    ///     A client for the Kubernetes Nodes (v1) API.
    /// </summary>
    public class RoleBindingClientV1
        : KubeResourceClient, IRoleBindingClientV1
    {
        /// <summary>
        ///     Create a new <see cref="RoleBindingClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public RoleBindingClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the RoleBinding with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the RoleBinding to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleBindingV1"/> representing the current state for the RoleBinding, or <c>null</c> if no RoleBinding was found with the specified name and namespace.
        /// </returns>
        public async Task<RoleBindingV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<RoleBindingV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all RoleBindings in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the RoleBindings.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleBindingListV1"/> containing the role bindings.
        /// </returns>
        public async Task<RoleBindingListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<RoleBindingListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to a specific RoleBinding.
        /// </summary>
        /// <param name="name">
        ///     The name of the RoleBinding to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<RoleBindingV1>> Watch(string name, string kubeNamespace = null)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return ObserveEvents<RoleBindingV1>(
                Requests.WatchByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                operationDescription: $"watch v1/RoleBinding '{name}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="RoleBindingV1"/>.
        /// </summary>
        /// <param name="newRoleBinding">
        ///     A <see cref="RoleBindingV1"/> representing the RoleBinding to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleBindingV1"/> representing the current state for the newly-created RoleBinding.
        /// </returns>
        public async Task<RoleBindingV1> Create(RoleBindingV1 newRoleBinding, CancellationToken cancellationToken = default)
        {
            if (newRoleBinding == null)
                throw new ArgumentNullException(nameof(newRoleBinding));

            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newRoleBinding?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newRoleBinding,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<RoleBindingV1>(
                    operationDescription: $"create v1/RoleBinding resource in namespace '{newRoleBinding?.Metadata?.Namespace ?? KubeClient.DefaultNamespace}'"
                );
        }

        /// <summary>
        ///     Request update (PATCH) of a <see cref="RoleBindingV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target RoleBinding.
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
        ///     A <see cref="RoleBindingV1"/> representing the current state for the updated RoleBinding.
        /// </returns>
        public async Task<RoleBindingV1> Update(string name, Action<JsonPatchDocument<RoleBindingV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default)
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
        ///     Request deletion of the specified RoleBinding.
        /// </summary>
        /// <param name="name">
        ///     The name of the RoleBinding to delete.
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
                .ReadContentAsObjectV1Async<StatusV1>(
                    $"delete v1/RoleBinding resource '{name}' in namespace '{kubeNamespace ?? KubeClient.DefaultNamespace}'",
                    HttpStatusCode.OK, HttpStatusCode.NotFound
                );
        }

        /// <summary>
        ///     Request templates for the RoleBinding (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level RoleBinding (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = KubeRequest.Create("apis/rbac.authorization.k8s.io/v1/namespaces/{Namespace}/rolebindings?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A get-by-name RoleBinding (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = KubeRequest.Create("apis/rbac.authorization.k8s.io/v1/namespaces/{Namespace}/rolebindings/{Name}");

            /// <summary>
            ///     A watch-by-name RoleBinding (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchByName = KubeRequest.Create("apis/rbac.authorization.k8s.io/v1/namespaces/{Namespace}/rolebindings/{Name}");
        }
    }

    /// <summary>
    ///     A client for the Kubernetes Nodes (v1) API.
    /// </summary>
    public interface IRoleBindingClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the RoleBinding with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the RoleBinding to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleBindingV1"/> representing the current state for the RoleBinding, or <c>null</c> if no RoleBinding was found with the specified name and namespace.
        /// </returns>
        Task<RoleBindingV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all RoleBindings in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the RoleBindings.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleBindingListV1"/> containing the RoleBindings.
        /// </returns>
        Task<RoleBindingListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to a specific RoleBinding.
        /// </summary>
        /// <param name="name">
        ///     The name of the RoleBinding to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<RoleBindingV1>> Watch(string name, string kubeNamespace = null);

        /// <summary>
        ///     Request creation of a <see cref="RoleBindingV1"/>.
        /// </summary>
        /// <param name="newRoleBinding">
        ///     A <see cref="RoleBindingV1"/> representing the RoleBinding to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleBindingV1"/> representing the current state for the newly-created RoleBinding.
        /// </returns>
        Task<RoleBindingV1> Create(RoleBindingV1 newRoleBinding, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request update (PATCH) of a <see cref="RoleBindingV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target RoleBinding.
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
        ///     A <see cref="RoleBindingV1"/> representing the current state for the updated RoleBinding.
        /// </returns>
        Task<RoleBindingV1> Update(string name, Action<JsonPatchDocument<RoleBindingV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified RoleBinding.
        /// </summary>
        /// <param name="name">
        ///     The name of the RoleBinding to delete.
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