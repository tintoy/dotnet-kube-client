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
    ///     A client for the Kubernetes Namespaces (v1) API.
    /// </summary>
    public class NamespaceClientV1
        : KubeResourceClient, INamespaceClientV1
    {
        /// <summary>
        ///     Create a new <see cref="NamespaceClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public NamespaceClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the Namespace with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Namespace to retrieve.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NamespaceV1"/> representing the current state for the Namespace, or <c>null</c> if no Namespace was found with the specified name and namespace.
        /// </returns>
        public async Task<NamespaceV1> Get(string name, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<NamespaceV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all Namespaces in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Namespaces.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NamespaceListV1"/> containing the Namespaces.
        /// </returns>
        public async Task<NamespaceListV1> List(string labelSelector = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<NamespaceListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to a specific Namespace.
        /// </summary>
        /// <param name="name">
        ///     The name of the Namespace to watch.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<NamespaceV1>> Watch(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return ObserveEvents<NamespaceV1>(
                Requests.WatchByName.WithTemplateParameters(new
                {
                    Name = name
                }),
                operationDescription: $"watch v1/Namespace '{name}'"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="NamespaceV1"/>.
        /// </summary>
        /// <param name="newNamespace">
        ///     A <see cref="NamespaceV1"/> representing the Namespace to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NamespaceV1"/> representing the current state for the newly-created Namespace.
        /// </returns>
        public async Task<NamespaceV1> Create(NamespaceV1 newNamespace, CancellationToken cancellationToken = default)
        {
            if (newNamespace == null)
                throw new ArgumentNullException(nameof(newNamespace));
            
            return await Http
                .PostAsJsonAsync(Requests.Collection,
                    postBody: newNamespace,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<NamespaceV1>("create v1/Namespace resource");
        }

        /// <summary>
        ///     Request deletion of the specified Namespace.
        /// </summary>
        /// <param name="name">
        ///     The name of the Namespace to delete.
        /// </param>
        /// <param name="propagationPolicy">
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NamespaceV1"/> representing the namespace's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public Task<KubeResourceResultV1<NamespaceV1>> Delete(string name, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteGlobalResource<NamespaceV1>(Requests.ByName, name, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the Namespace (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level Namespace (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection   = KubeRequest.Create("api/v1/namespaces?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A get-by-name Namespace (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName       = KubeRequest.Create("api/v1/namespaces/{Name}");

            /// <summary>
            ///     A watch-by-name Namespace (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchByName  = KubeRequest.Create("api/v1/watch/namespaces/{Name}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes Namespaces (v1) API.
    /// </summary>
    public interface INamespaceClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the Namespace with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Namespace to retrieve.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NamespaceV1"/> representing the current state for the Namespace, or <c>null</c> if no Namespace was found with the specified name and namespace.
        /// </returns>
        Task<NamespaceV1> Get(string name, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all Namespaces in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Namespaces.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NamespaceListV1"/> containing the Namespaces.
        /// </returns>
        Task<NamespaceListV1> List(string labelSelector = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to a specific Namespace.
        /// </summary>
        /// <param name="name">
        ///     The name of the Namespace to watch.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<NamespaceV1>> Watch(string name);

        /// <summary>
        ///     Request creation of a <see cref="NamespaceV1"/>.
        /// </summary>
        /// <param name="newNamespace">
        ///     A <see cref="NamespaceV1"/> representing the Namespace to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NamespaceV1"/> representing the current state for the newly-created Namespace.
        /// </returns>
        Task<NamespaceV1> Create(NamespaceV1 newNamespace, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified Namespace.
        /// </summary>
        /// <param name="name">
        ///     The name of the Namespace to delete.
        /// </param>
        /// <param name="propagationPolicy">
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="NamespaceV1"/> representing the namespace's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        Task<KubeResourceResultV1<NamespaceV1>> Delete(string name, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
