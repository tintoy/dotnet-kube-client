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
    ///     A client for the Kubernetes ReplicationControllers (v1) API.
    /// </summary>
    public class ReplicationControllerClientV1
        : KubeResourceClient
    {
        /// <summary>
        ///     Create a new <see cref="ReplicationControllerClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public ReplicationControllerClientV1(KubeApiClient client)
            : base(client)
        {
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
        ///     The ReplicationControllers, as a list of <see cref="ReplicationControllerV1"/>s.
        /// </returns>
        public async Task<List<ReplicationControllerV1>> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            ReplicationControllerListV1 matchingControllers =
                await Http.GetAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                        LabelSelector = labelSelector
                    }),
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<ReplicationControllerListV1, StatusV1>();

            return matchingControllers.Items;
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
        public IObservable<ResourceEventV1<ReplicationControllerV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<ReplicationControllerV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                })
            );
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
                .ReadContentAsAsync<ReplicationControllerV1, StatusV1>();
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
        ///     A <see cref="DeletePropagationPolicy"/> indicating how child resources should be deleted (if at all).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     An <see cref="StatusV1"/> indicating the result of the request.
        /// </returns>
        public async Task<KubeObjectV1> Delete(string name, string kubeNamespace = null, DeletePropagationPolicy propagationPolicy = DeletePropagationPolicy.Background, CancellationToken cancellationToken = default)
        {
            var request = Http.DeleteAsJsonAsync(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                deleteBody: new DeleteOptionsV1
                {
                    PropagationPolicy = propagationPolicy
                },
                cancellationToken: cancellationToken
            );

            if (propagationPolicy == DeletePropagationPolicy.Foreground)
                return await request.ReadContentAsObjectV1Async<ReplicationControllerV1>(HttpStatusCode.OK);
            
            return await request.ReadContentAsObjectV1Async<StatusV1>(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the ReplicationController (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level ReplicationController (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/replicationcontrollers?labelSelector={LabelSelector?}&watch={Watch?}", SerializerSettings);

            /// <summary>
            ///     A get-by-name ReplicationController (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/replicationcontrollers/{Name}", SerializerSettings);
        }
    }
}
