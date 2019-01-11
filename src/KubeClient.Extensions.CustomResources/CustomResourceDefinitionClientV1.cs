using HTTPlease;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes CustomResourceDefinitions (v1beta1) API.
    /// </summary>
    public class CustomResourceDefinitionClientV1Beta1
        : KubeResourceClient, ICustomResourceDefinitionClientV1Beta1
    {
        /// <summary>
        ///     Create a new <see cref="CustomResourceDefinitionClientV1Beta1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public CustomResourceDefinitionClientV1Beta1(IKubeApiClient client)
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
        ///     A <see cref="CustomResourceDefinitionV1Beta1"/> representing the current state for the CustomResourceDefinition, or <c>null</c> if no CustomResourceDefinition was found with the specified name and namespace.
        /// </returns>
        public async Task<CustomResourceDefinitionV1Beta1> Get(string name, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<CustomResourceDefinitionV1Beta1>(
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
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionListV1Beta1"/> containing the jobs.
        /// </returns>
        public async Task<CustomResourceDefinitionListV1Beta1> List(string labelSelector = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<CustomResourceDefinitionListV1Beta1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    LabelSelector = labelSelector
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
        public IObservable<IResourceEventV1<CustomResourceDefinitionV1Beta1>> Watch(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return ObserveEvents<CustomResourceDefinitionV1Beta1>(
                Requests.WatchByName.WithTemplateParameters(new
                {
                    Name = name
                }),
                operationDescription: $"watch v1beta1/CustomResourceDefintion '{name}'"
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
        public IObservable<IResourceEventV1<CustomResourceDefinitionV1Beta1>> WatchAll(string labelSelector = null)
        {
            return ObserveEvents<CustomResourceDefinitionV1Beta1>(
                Requests.WatchCollection.WithTemplateParameters(new
                {
                    LabelSelector = labelSelector
                }),
                operationDescription: $"watch all v1beta1/CustomResourceDefintions with label selector '{labelSelector ?? "<none>"}'"
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="CustomResourceDefinitionV1Beta1"/>.
        /// </summary>
        /// <param name="newCustomResourceDefinition">
        ///     A <see cref="CustomResourceDefinitionV1Beta1"/> representing the CustomResourceDefinition to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionV1Beta1"/> representing the current state for the newly-created CustomResourceDefinition.
        /// </returns>
        public async Task<CustomResourceDefinitionV1Beta1> Create(CustomResourceDefinitionV1Beta1 newCustomResourceDefinition, CancellationToken cancellationToken = default)
        {
            if (newCustomResourceDefinition == null)
                throw new ArgumentNullException(nameof(newCustomResourceDefinition));
            
            return await Http
                .PostAsJsonAsync(Requests.Collection,
                    postBody: newCustomResourceDefinition,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<CustomResourceDefinitionV1Beta1>();
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
        ///     A <see cref="CustomResourceDefinitionV1Beta1"/> representing the job's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        public Task<KubeResourceResultV1<CustomResourceDefinitionV1Beta1>> Delete(string name, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default)
        {
            return DeleteGlobalResource<CustomResourceDefinitionV1Beta1>(Requests.ByName, name, propagationPolicy, cancellationToken);
        }

        /// <summary>
        ///     Request templates for the CustomResourceDefinition (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level CustomResourceDefinition (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection       = KubeRequest.Create("apis/apiextensions.k8s.io/v1beta1/customresourcedefinitions?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A get-by-name CustomResourceDefinition (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName           = KubeRequest.Create("apis/apiextensions.k8s.io/v1beta1/customresourcedefinitions/{Name}");

            /// <summary>
            ///     A collection-level CustomResourceDefinition watch (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchCollection  = KubeRequest.Create("/apis/apiextensions.k8s.io/v1beta1/watch/customresourcedefinitions?labelSelector={LabelSelector?}");

            /// <summary>
            ///     A watch-by-name CustomResourceDefinition (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchByName      = KubeRequest.Create("/apis/apiextensions.k8s.io/v1beta1/watch/customresourcedefinitions/{Name}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes CustomResourceDefinitions (v1beta1) API.
    /// </summary>
    public interface ICustomResourceDefinitionClientV1Beta1
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
        ///     A <see cref="CustomResourceDefinitionV1Beta1"/> representing the current state for the CustomResourceDefinition, or <c>null</c> if no CustomResourceDefinition was found with the specified name and namespace.
        /// </returns>
        Task<CustomResourceDefinitionV1Beta1> Get(string name, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all CustomResourceDefinitions in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the CustomResourceDefinitions.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionListV1Beta1"/> containing the jobs.
        /// </returns>
        Task<CustomResourceDefinitionListV1Beta1> List(string labelSelector = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to a specific CustomResourceDefinition.
        /// </summary>
        /// <param name="name">
        ///     The name of the job to watch.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<CustomResourceDefinitionV1Beta1>> Watch(string name);

        /// <summary>
        ///     Watch for events relating to CustomResourceDefinitions.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the CustomResourceDefinitions.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<CustomResourceDefinitionV1Beta1>> WatchAll(string labelSelector = null);

        /// <summary>
        ///     Request creation of a <see cref="CustomResourceDefinitionV1Beta1"/>.
        /// </summary>
        /// <param name="newCustomResourceDefinition">
        ///     A <see cref="CustomResourceDefinitionV1Beta1"/> representing the CustomResourceDefinition to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="CustomResourceDefinitionV1Beta1"/> representing the current state for the newly-created CustomResourceDefinition.
        /// </returns>
        Task<CustomResourceDefinitionV1Beta1> Create(CustomResourceDefinitionV1Beta1 newCustomResourceDefinition, CancellationToken cancellationToken = default);

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
        ///     A <see cref="CustomResourceDefinitionV1Beta1"/> representing the job's most recent state before it was deleted, if <paramref name="propagationPolicy"/> is <see cref="DeletePropagationPolicy.Foreground"/>; otherwise, a <see cref="StatusV1"/>.
        /// </returns>
        Task<KubeResourceResultV1<CustomResourceDefinitionV1Beta1>> Delete(string name, DeletePropagationPolicy? propagationPolicy = null, CancellationToken cancellationToken = default);
    }
}
