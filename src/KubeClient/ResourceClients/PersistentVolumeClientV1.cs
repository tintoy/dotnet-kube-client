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
    ///     A client for the Kubernetes PersistentVolumes (v1) API.
    /// </summary>
    public class PersistentVolumeClientV1
        : KubeResourceClient
    {
        /// <summary>
        ///     Create a new <see cref="PersistentVolumeClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public PersistentVolumeClientV1(KubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the PersistentVolume with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the PersistentVolume to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PersistentVolumeV1"/> representing the current state for the PersistentVolume, or <c>null</c> if no PersistentVolume was found with the specified name and namespace.
        /// </returns>
        public async Task<PersistentVolumeV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<PersistentVolumeV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all PersistentVolumes in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the PersistentVolumes.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PersistentVolumeListV1"/> containing the PersistentVolumes.
        /// </returns>
        public async Task<PersistentVolumeListV1> List(string labelSelector = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<PersistentVolumeListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to PersistentVolumes.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the PersistentVolumes.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<ResourceEventV1<PersistentVolumeV1>> WatchAll(string labelSelector = null, string kubeNamespace = null)
        {
            return ObserveEvents<PersistentVolumeV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    Watch = true
                })
            );
        }

        /// <summary>
        ///     Request creation of a <see cref="PersistentVolumeV1"/>.
        /// </summary>
        /// <param name="newPersistentVolume">
        ///     A <see cref="PersistentVolumeV1"/> representing the PersistentVolume to create.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PersistentVolumeV1"/> representing the current state for the newly-created PersistentVolume.
        /// </returns>
        public async Task<PersistentVolumeV1> Create(PersistentVolumeV1 newPersistentVolume, CancellationToken cancellationToken = default)
        {
            if (newPersistentVolume == null)
                throw new ArgumentNullException(nameof(newPersistentVolume));
            
            return await Http
                .PostAsJsonAsync(
                    Requests.Collection.WithTemplateParameters(new
                    {
                        Namespace = newPersistentVolume?.Metadata?.Namespace ?? KubeClient.DefaultNamespace
                    }),
                    postBody: newPersistentVolume,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsAsync<PersistentVolumeV1, StatusV1>();
        }

        /// <summary>
        ///     Request deletion of the specified PersistentVolume.
        /// </summary>
        /// <param name="name">
        ///     The name of the PersistentVolume to delete.
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
        ///     Request templates for the PersistentVolumes (v1) API.
        /// </summary>
        public static class Requests
        {
            /// <summary>
            ///     A collection-level PersistentVolume (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/persistentvolumes?labelSelector={LabelSelector?}&watch={Watch?}", SerializerSettings);

            /// <summary>
            ///     A get-by-name PersistentVolume (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = HttpRequest.Factory.Json("api/v1/namespaces/{Namespace}/persistentvolumes/{Name}", SerializerSettings);
        }
    }
}
