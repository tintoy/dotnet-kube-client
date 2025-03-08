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
    ///     A client for the Kubernetes Nodes (v1) API.
    /// </summary>
    public class ServiceAccountClientV1
        : KubeResourceClient, IServiceAccountClientV1
    {
        /// <summary>
        ///     Create a new <see cref="ServiceAccountClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public ServiceAccountClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the ServiceAccount with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the ServiceAccount to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ServiceAccountV1"/> representing the current state for the ServiceAccount, or <c>null</c> if no ServiceAccount was found with the specified name and namespace.
        /// </returns>
        public async Task<ServiceAccountV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<ServiceAccountV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all ServiceAccounts in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ServiceAccounts.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ServiceAccountListV1"/> containing the service accounts.
        /// </returns>
        public async Task<ServiceAccountListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<ServiceAccountListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to a specific ServiceAccount.
        /// </summary>
        /// <param name="name">
        ///     The name of the ServiceAccount to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<ServiceAccountV1>> Watch(string name, string kubeNamespace = null)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return ObserveEvents<ServiceAccountV1>(
                Requests.WatchByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                operationDescription: $"watch v1/ServiceAccount '{name}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="ServiceAccountV1"/>.
        /// </summary>
        /// <param name="newServiceAccount">
        ///     A <see cref="ServiceAccountV1"/> representing the ServiceAccount to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ServiceAccountV1"/> representing the current state for the newly-created ServiceAccount.
        /// </returns>
        public async Task<ServiceAccountV1> Create(ServiceAccountV1 newServiceAccount, CancellationToken cancellationToken = default)
        {
            if (newServiceAccount == null)
                throw new ArgumentNullException(nameof(newServiceAccount));

            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newServiceAccount?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newServiceAccount,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<ServiceAccountV1>(
                    operationDescription: $"create v1/ServiceAccount resource in namespace '{newServiceAccount?.Metadata?.Namespace ?? KubeClient.DefaultNamespace}'"
                );
        }

        /// <summary>
        ///     Request update (PATCH) of a <see cref="ServiceAccountV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target ServiceAccount.
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
        ///     A <see cref="ServiceAccountV1"/> representing the current state for the updated ServiceAccount.
        /// </returns>
        public async Task<ServiceAccountV1> Update(string name, Action<JsonPatchDocument<ServiceAccountV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default)
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
        ///     Request deletion of the specified ServiceAccount.
        /// </summary>
        /// <param name="name">
        ///     The name of the ServiceAccount to delete.
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
                    $"delete v1/ServiceAccount resource '{name}' in namespace '{kubeNamespace ?? KubeClient.DefaultNamespace}'",
                    HttpStatusCode.OK, HttpStatusCode.NotFound
                );
        }

        /// <summary>
        ///     Request templates for the ServiceAccount (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level ServiceAccount (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = KubeRequest.Create("api/v1/namespaces/{Namespace}/serviceaccounts?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A get-by-name ServiceAccount (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = KubeRequest.Create("api/v1/namespaces/{Namespace}/serviceaccounts/{Name}");

            /// <summary>
            ///     A watch-by-name ServiceAccount (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchByName = KubeRequest.Create("api/v1/namespaces/{Namespace}/serviceaccounts/{Name}");
        }
    }

    /// <summary>
    ///     A client for the Kubernetes Nodes (v1) API.
    /// </summary>
    public interface IServiceAccountClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the ServiceAccount with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the ServiceAccount to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ServiceAccountV1"/> representing the current state for the ServiceAccount, or <c>null</c> if no ServiceAccount was found with the specified name and namespace.
        /// </returns>
        Task<ServiceAccountV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all ServiceAccounts in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the ServiceAccounts.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ServiceAccountListV1"/> containing the ServiceAccounts.
        /// </returns>
        Task<ServiceAccountListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to a specific ServiceAccount.
        /// </summary>
        /// <param name="name">
        ///     The name of the ServiceAccount to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<ServiceAccountV1>> Watch(string name, string kubeNamespace = null);

        /// <summary>
        ///     Request creation of a <see cref="ServiceAccountV1"/>.
        /// </summary>
        /// <param name="newServiceAccount">
        ///     A <see cref="ServiceAccountV1"/> representing the ServiceAccount to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ServiceAccountV1"/> representing the current state for the newly-created ServiceAccount.
        /// </returns>
        Task<ServiceAccountV1> Create(ServiceAccountV1 newServiceAccount, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request update (PATCH) of a <see cref="ServiceAccountV1"/>.
        /// </summary>
        /// <param name="name">
        ///     The name of the target ServiceAccount.
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
        ///     A <see cref="ServiceAccountV1"/> representing the current state for the updated ServiceAccount.
        /// </returns>
        Task<ServiceAccountV1> Update(string name, Action<JsonPatchDocument<ServiceAccountV1>> patchAction, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified ServiceAccount.
        /// </summary>
        /// <param name="name">
        ///     The name of the ServiceAccount to delete.
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