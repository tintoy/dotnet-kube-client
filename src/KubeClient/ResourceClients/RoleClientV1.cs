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
    public class RoleClientV1
        : KubeResourceClient, IRoleClientV1
    {
        /// <summary>
        ///     Create a new <see cref="RoleClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public RoleClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the Role with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Role to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleV1"/> representing the current state for the Role, or <c>null</c> if no Role was found with the specified name and namespace.
        /// </returns>
        public async Task<RoleV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<RoleV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all Roles in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Roles.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleListV1"/> containing the roles.
        /// </returns>
        public async Task<RoleListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<RoleListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to a specific Role.
        /// </summary>
        /// <param name="name">
        ///     The name of the Role to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<RoleV1>> Watch(string name, string kubeNamespace = null)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return ObserveEvents<RoleV1>(
                Requests.WatchByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                operationDescription: $"watch v1/Role '{name}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="RoleV1"/>.
        /// </summary>
        /// <param name="newRole">
        ///     A <see cref="RoleV1"/> representing the Role to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleV1"/> representing the current state for the newly-created Role.
        /// </returns>
        public async Task<RoleV1> Create(RoleV1 newRole, CancellationToken cancellationToken = default)
        {
            if (newRole == null)
                throw new ArgumentNullException(nameof(newRole));

            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newRole?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newRole,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<RoleV1>(
                    operationDescription: $"create v1/Role resource in namespace '{newRole?.Metadata?.Namespace ?? KubeClient.DefaultNamespace}'"
                );
        }

        /// <summary>
        ///     Request update (PATCH) of a <see cref="RoleV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target Role.
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
        ///     A <see cref="RoleV1"/> representing the current state for the updated Role.
        /// </returns>
        public async Task<RoleV1> Update(string name, Action<JsonPatchDocument<RoleV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default)
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
        ///     Request deletion of the specified Role.
        /// </summary>
        /// <param name="name">
        ///     The name of the Role to delete.
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
                    $"delete v1/Role resource '{name}' in namespace '{kubeNamespace ?? KubeClient.DefaultNamespace}'",
                    HttpStatusCode.OK, HttpStatusCode.NotFound
                );
        }

        /// <summary>
        ///     Request templates for the Role (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level Role (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = KubeRequest.Create("api/v1/namespaces/{Namespace}/roles?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A get-by-name Role (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = KubeRequest.Create("api/v1/namespaces/{Namespace}/roles/{Name}");

            /// <summary>
            ///     A watch-by-name Role (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchByName = KubeRequest.Create("api/v1/watch/namespaces/{Namespace}/roles/{Name}");
        }
    }

    /// <summary>
    ///     A client for the Kubernetes Nodes (v1) API.
    /// </summary>
    public interface IRoleClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the Role with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Role to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleV1"/> representing the current state for the Role, or <c>null</c> if no Role was found with the specified name and namespace.
        /// </returns>
        Task<RoleV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all Roles in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Roles.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleListV1"/> containing the Roles.
        /// </returns>
        Task<RoleListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to a specific Role.
        /// </summary>
        /// <param name="name">
        ///     The name of the Role to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<RoleV1>> Watch(string name, string kubeNamespace = null);

        /// <summary>
        ///     Request creation of a <see cref="RoleV1"/>.
        /// </summary>
        /// <param name="newRole">
        ///     A <see cref="RoleV1"/> representing the Role to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="RoleV1"/> representing the current state for the newly-created Role.
        /// </returns>
        Task<RoleV1> Create(RoleV1 newRole, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request update (PATCH) of a <see cref="RoleV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target Role.
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
        ///     A <see cref="RoleV1"/> representing the current state for the updated Role.
        /// </returns>
        Task<RoleV1> Update(string name, Action<JsonPatchDocument<RoleV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified Role.
        /// </summary>
        /// <param name="name">
        ///     The name of the Role to delete.
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