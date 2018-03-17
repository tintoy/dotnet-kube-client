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
    ///     A client for the Kubernetes Services (v1) API.
    /// </summary>
    public class ServiceClientV1
        : KubeResourceClient
    {
        /// <summary>
        ///     Create a new <see cref="ServiceClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public ServiceClientV1(KubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the Service with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Service to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ServiceV1"/> representing the current state for the Service, or <c>null</c> if no Service was found with the specified name and namespace.
        /// </returns>
        public async Task<ServiceV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<ServiceV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all Services in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Services.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ServiceListV1"/> containing the Services.
        /// </returns>
        public async Task<ServiceListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<ServiceListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to Services.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Services.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<ResourceEventV1<ServiceV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<ServiceV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                })
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="ServiceV1"/>.
        /// </summary>
        /// <param name="newService">
        ///     A <see cref="ServiceV1"/> representing the Service to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="ServiceV1"/> representing the current state for the newly-created Service.
        /// </returns>
        public async Task<ServiceV1> Create(ServiceV1 newService, CancellationToken cancellationToken = default)
        {
            if (newService == null)
                throw new ArgumentNullException(nameof(newService));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newService?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newService,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<ServiceV1, StatusV1>();
        }

        /// <summary>
        ///     Request deletion of the specified Service.
        /// </summary>
        /// <param name="name">
        ///     The name of the Service to delete.
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
                .ReadContentAsAsync<StatusV1, StatusV1>(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the Service (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level Service (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/services?labelSelector={LabelSelector?}&watch={Watch?}", SerializerSettings);

            /// <summary>
            ///     A get-by-name Service (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/services/{Name}", SerializerSettings);
        }
    }
}
