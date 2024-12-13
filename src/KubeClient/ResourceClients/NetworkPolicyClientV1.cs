using System;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.ResourceClients
{
    using Http;
    using Models;

    /// <summary>
    ///     A client for the Kubernetes NetworkPolicy resources (v1) API.
    /// </summary>
    public class NetworkPolicyClientV1
        : KubeResourceClient, INetworkPolicyClientV1
    {
        /// <summary>
        ///     Create a new <see cref="NetworkPolicyClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public NetworkPolicyClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the NetworkPolicy with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the NetworkPolicy to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NetworkPolicyV1"/> representing the current state for the NetworkPolicy, or <c>null</c> if no NetworkPolicy was found with the specified name and namespace.
        /// </returns>
        public async Task<NetworkPolicyV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<NetworkPolicyV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all NetworkPolicy resources in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the NetworkPolicy resources.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NetworkPolicyListV1"/> containing the NetworkPolicys.
        /// </returns>
        public async Task<NetworkPolicyListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<NetworkPolicyListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to a specific NetworkPolicy.
        /// </summary>
        /// <param name="name">
        ///     The name of the NetworkPolicy to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<NetworkPolicyV1>> Watch(string name, string kubeNamespace = null)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return ObserveEvents<NetworkPolicyV1>(
                Requests.WatchByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                operationDescription: $"watch v1/NetworkPolicy '{name}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Watch for events relating to NetworkPolicy resources.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the NetworkPolicy resources.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<NetworkPolicyV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<NetworkPolicyV1>(
                Requests.WatchCollection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                operationDescription: $"watch all v1/NetworkPolicy with label selector '{labelSelector ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="NetworkPolicyV1"/>.
        /// </summary>
        /// <param name="newNetworkPolicy">
        ///     A <see cref="NetworkPolicyV1"/> representing the NetworkPolicy to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NetworkPolicyV1"/> representing the current state for the newly-created NetworkPolicy.
        /// </returns>
        public async Task<NetworkPolicyV1> Create(NetworkPolicyV1 newNetworkPolicy, CancellationToken cancellationToken = default)
        {
            if (newNetworkPolicy == null)
                throw new ArgumentNullException(nameof(newNetworkPolicy));

            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newNetworkPolicy?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newNetworkPolicy,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<NetworkPolicyV1>("create v1/NetworkPolicy resource");
        }

        /// <summary>
        ///     Request deletion of the specified NetworkPolicy.
        /// </summary>
        /// <param name="name">
        ///     The name of the NetworkPolicy to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The Kubernetes namespace containing the NetworkPolicy to delete.
        /// </param>
        /// <param name="propagationPolicy">
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NetworkPolicyV1"/> representing the NetworkPolicy's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public Task<KubeResourceResultV1<NetworkPolicyV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteResource<NetworkPolicyV1>(Requests.ByName, name, kubeNamespace, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the NetworkPolicy (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level NetworkPolicy (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = KubeRequest.Create("apis/networking.k8s.io/v1/namespaces/{Namespace}/networkpolicies?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A get-by-name NetworkPolicy (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = KubeRequest.Create("apis/networking.k8s.io/v1/namespaces/{Namespace}/networkpolicies/{Name}");

            /// <summary>
            ///     A collection-level NetworkPolicy watch (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchCollection = KubeRequest.Create("apis/networking.k8s.io/v1/watch/namespaces/{Namespace}/networkpolicies?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A watch-by-name NetworkPolicy (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchByName = KubeRequest.Create("apis/networking.k8s.io/v1/watch/namespaces/{Namespace}/networkpolicies/{Name}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes NetworkPolicy resources (v1) API.
    /// </summary>
    public interface INetworkPolicyClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the NetworkPolicy with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the NetworkPolicy to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NetworkPolicyV1"/> representing the current state for the NetworkPolicy, or <c>null</c> if no NetworkPolicy was found with the specified name and namespace.
        /// </returns>
        Task<NetworkPolicyV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all NetworkPolicy resources in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the NetworkPolicy resources.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NetworkPolicyListV1"/> containing the NetworkPolicys.
        /// </returns>
        Task<NetworkPolicyListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to a specific NetworkPolicy.
        /// </summary>
        /// <param name="name">
        ///     The name of the NetworkPolicy to watch.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<NetworkPolicyV1>> Watch(string name, string kubeNamespace = null);

        /// <summary>
        ///     Watch for events relating to NetworkPolicy resources.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the NetworkPolicy resources.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<NetworkPolicyV1>> WatchAll(string labelSelector = null, string kubeNamespace = null);

        /// <summary>
        ///     Request creation of a <see cref="NetworkPolicyV1"/>.
        /// </summary>
        /// <param name="newNetworkPolicy">
        ///     A <see cref="NetworkPolicyV1"/> representing the NetworkPolicy to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NetworkPolicyV1"/> representing the current state for the newly-created NetworkPolicy.
        /// </returns>
        Task<NetworkPolicyV1> Create(NetworkPolicyV1 newNetworkPolicy, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified NetworkPolicy.
        /// </summary>
        /// <param name="name">
        ///     The name of the NetworkPolicy to delete.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The Kubernetes namespace containing the NetworkPolicy to delete.
        /// </param>
        /// <param name="propagationPolicy">
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NetworkPolicyV1"/> representing the NetworkPolicy's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        Task<KubeResourceResultV1<NetworkPolicyV1>> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
