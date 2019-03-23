using HTTPlease;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes Nodes (v1) API.
    /// </summary>
    public class NodeClientV1
        : KubeResourceClient, INodeClientV1
    {
        /// <summary>
        ///     Create a new <see cref="NodeClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public NodeClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the Node with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Node to retrieve.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NodeV1"/> representing the current state for the Node, or <c>null</c> if no Node was found with the specified name.
        /// </returns>
        public async Task<NodeV1> Get(string name, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<NodeV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all Nodes, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Nodes.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NodeListV1"/> containing the Nodes.
        /// </returns>
        public async Task<NodeListV1> List(string labelSelector = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<NodeListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to Nodes.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Nodes.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<NodeV1>> WatchAll(string labelSelector = null)
        {
            return ObserveEvents<NodeV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    LabelSelector = labelSelector,
                    Watch = true
                }),
                operationDescription: $"watch all v1/nodes with label selector '{labelSelector ?? "<none>"}'"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="NodeV1"/>.
        /// </summary>
        /// <param name="newNode">
        ///     A <see cref="NodeV1"/> representing the Node to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NodeV1"/> representing the current state for the newly-created Node.
        /// </returns>
        public async Task<NodeV1> Create(NodeV1 newNode, CancellationToken cancellationToken = default)
        {
            if (newNode == null)
                throw new ArgumentNullException(nameof(newNode));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection,
                    postBody: newNode,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<NodeV1>("create v1/node resource");
        }

        /// <summary>
        ///     Request deletion of the specified Node.
        /// </summary>
        /// <param name="name">
        ///     The name of the Node to delete.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     An <see cref="StatusV1"/> indicating the result of the request.
        /// </returns>
        public async Task<StatusV1> Delete(string name, CancellationToken cancellationToken = default)
        {
            return await Http
                .DeleteAsync(
                    Requests.ByName.WithTemplateParameters(new
                    {
                        Name = name
                    }),
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<StatusV1>("delete v1/node resource", HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the Nodes (v1) API.
        /// </summary>
        public static class Requests
        {
            /// <summary>
            ///     A collection-level Node (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection   = KubeRequest.Create("api/v1/nodes?labelSelector={LabelSelector?}&watch={Watch?}");

            /// <summary>
            ///     A get-by-name Node (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName       = KubeRequest.Create("api/v1/nodes/{Name}");
        }
    }

    /// <summary>
    ///     A client for the Kubernetes Nodes (v1) API.
    /// </summary>
    public interface INodeClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the Node with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Node to retrieve.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NodeV1"/> representing the current state for the Node, or <c>null</c> if no Node was found with the specified name.
        /// </returns>
        Task<NodeV1> Get(string name, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all Nodes, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Nodes.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NodeListV1"/> containing the Nodes.
        /// </returns>
        Task<NodeListV1> List(string labelSelector = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to Nodes.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Nodes.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<NodeV1>> WatchAll(string labelSelector = null);

        /// <summary>
        ///     Request creation of a <see cref="NodeV1"/>.
        /// </summary>
        /// <param name="newNode">
        ///     A <see cref="NodeV1"/> representing the Node to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NodeV1"/> representing the current state for the newly-created Node.
        /// </returns>
        Task<NodeV1> Create(NodeV1 newNode, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified Node.
        /// </summary>
        /// <param name="name">
        ///     The name of the Node to delete.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     An <see cref="StatusV1"/> indicating the result of the request.
        /// </returns>
        Task<StatusV1> Delete(string name, CancellationToken cancellationToken = default);
    }
}
