using System;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.ResourceClients
{
    using Http;
    using Models;

    /// <summary>
    ///     A client for the Kubernetes CustomResourceDefinitions (v1) API.
    /// </summary>
    public class CustomResourceDefinitionClientV1
        : KubeResourceClient, ICustomResourceDefinitionClientV1
    {
        /// <summary>
        ///     Create a new <see cref="CustomResourceDefinitionClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public CustomResourceDefinitionClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the CustomResourceDefinition with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the CustomResourceDefinition to retrieve.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionV1"/> representing the current state for the CustomResourceDefinition, or <c>null</c> if no CustomResourceDefinition was found with the specified name and namespace.
        /// </returns>
        public async Task<CustomResourceDefinitionV1> Get(string name, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<CustomResourceDefinitionV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all CustomResourceDefinitions in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the CustomResourceDefinitions.
        /// </param>
        /// <param name="limit">
        ///     The maximum number of results to return.
        ///     
        ///     <para>
        ///         If specified, <see cref="ListMetaV1.Continue"/> will be non-<c>null</c> if there are more results available.
        ///     </para>
        /// </param>
        /// <param name="continue">
        ///     The result of <see cref="ListMetaV1.Continue"/> from the previous call.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionListV1"/> containing the jobs.
        /// </returns>
        public async Task<CustomResourceDefinitionListV1> List(string? labelSelector = null, int? limit = null, string? @continue = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<CustomResourceDefinitionListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    LabelSelector = labelSelector,
                    Limit = limit,
                    Continue = @continue,
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to a specific CustomResourceDefinition.
        /// </summary>
        /// <param name="name">
        ///     The name of the job to watch.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<CustomResourceDefinitionV1>> Watch(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return ObserveEvents<CustomResourceDefinitionV1>(
                Requests.WatchByName.WithTemplateParameters(new
                {
                    Name = name
                }),
                operationDescription: $"watch v1/CustomResourceDefintion '{name}'"
            );
        }

        /// <summary>
        ///     Watch for events relating to CustomResourceDefinitions.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the CustomResourceDefinitions.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<CustomResourceDefinitionV1>> WatchAll(string? labelSelector = null)
        {
            return ObserveEvents<CustomResourceDefinitionV1>(
                Requests.WatchCollection.WithTemplateParameters(new
                {
                    LabelSelector = labelSelector
                }),
                operationDescription: $"watch all v1/CustomResourceDefintions with label selector '{labelSelector ?? "<none>"}'"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="CustomResourceDefinitionV1"/>.
        /// </summary>
        /// <param name="newCustomResourceDefinition">
        ///     A <see cref="CustomResourceDefinitionV1"/> representing the CustomResourceDefinition to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionV1"/> representing the current state for the newly-created CustomResourceDefinition.
        /// </returns>
        public async Task<CustomResourceDefinitionV1> Create(CustomResourceDefinitionV1 newCustomResourceDefinition, CancellationToken cancellationToken = default)
        {
            if (newCustomResourceDefinition == null)
                throw new ArgumentNullException(nameof(newCustomResourceDefinition));

            return await Http
                .PostAsJsonAsync(Requests.Collection,
                    postBody: newCustomResourceDefinition,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<CustomResourceDefinitionV1>();
        }

        /// <summary>
        ///     Request deletion of the specified CustomResourceDefinition.
        /// </summary>
        /// <param name="name">
        ///     The name of the CustomResourceDefinition to delete.
        /// </param>
        /// <param name="propagationPolicy">
        ///     An optional <see cref="DeletePropagationPolicy"/> value indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionV1"/> representing the job's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public Task<KubeResourceResultV1<CustomResourceDefinitionV1>> Delete(string name, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteGlobalResource<CustomResourceDefinitionV1>(Requests.ByName, name, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the CustomResourceDefinition (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level CustomResourceDefinition (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = KubeRequest.Create("apis/apiextensions.k8s.io/v1/customresourcedefinitions?labelSelector={LabelSelector?}&limit={Limit?}&continue={Continue?}");

            /// <summary>
            ///     A get-by-name CustomResourceDefinition (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = KubeRequest.Create("apis/apiextensions.k8s.io/v1/customresourcedefinitions/{Name}");

            /// <summary>
            ///     A collection-level CustomResourceDefinition watch (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchCollection = KubeRequest.Create("/apis/apiextensions.k8s.io/v1/watch/customresourcedefinitions?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A watch-by-name CustomResourceDefinition (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchByName = KubeRequest.Create("/apis/apiextensions.k8s.io/v1/watch/customresourcedefinitions/{Name}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes CustomResourceDefinitions (v1) API.
    /// </summary>
    public interface ICustomResourceDefinitionClientV1
    {
        /// <summary>
        ///     Get the CustomResourceDefinition with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the CustomResourceDefinition to retrieve.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionV1"/> representing the current state for the CustomResourceDefinition, or <c>null</c> if no CustomResourceDefinition was found with the specified name and namespace.
        /// </returns>
        Task<CustomResourceDefinitionV1> Get(string name, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all CustomResourceDefinitions in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the CustomResourceDefinitions.
        /// </param>
        /// <param name="limit">
        ///     The maximum number of results to return.
        ///     
        ///     <para>
        ///         If specified, <see cref="ListMetaV1.Continue"/> will be non-<c>null</c> if there are more results available.
        ///     </para>
        /// </param>
        /// <param name="continue">
        ///     The result of <see cref="ListMetaV1.Continue"/> from the previous call.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionListV1"/> containing the jobs.
        /// </returns>
        Task<CustomResourceDefinitionListV1> List(string? labelSelector = null, int? limit = null, string? @continue = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to a specific CustomResourceDefinition.
        /// </summary>
        /// <param name="name">
        ///     The name of the job to watch.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<CustomResourceDefinitionV1>> Watch(string name);

        /// <summary>
        ///     Watch for events relating to CustomResourceDefinitions.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the CustomResourceDefinitions.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<CustomResourceDefinitionV1>> WatchAll(string? labelSelector = null);

        /// <summary>
        ///     Request creation of a <see cref="CustomResourceDefinitionV1"/>.
        /// </summary>
        /// <param name="newCustomResourceDefinition">
        ///     A <see cref="CustomResourceDefinitionV1"/> representing the CustomResourceDefinition to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionV1"/> representing the current state for the newly-created CustomResourceDefinition.
        /// </returns>
        Task<CustomResourceDefinitionV1> Create(CustomResourceDefinitionV1 newCustomResourceDefinition, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified CustomResourceDefinition.
        /// </summary>
        /// <param name="name">
        ///     The name of the CustomResourceDefinition to delete.
        /// </param>
        /// <param name="propagationPolicy">
        ///     A <see cref="DeletePropagationPolicy"/> indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionV1"/> representing the job's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        Task<KubeResourceResultV1<CustomResourceDefinitionV1>> Delete(string name, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
