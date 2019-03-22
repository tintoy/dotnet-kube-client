using HTTPlease;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace KubeClient.ResourceClients
{
    using Models;

    /// <summary>
    ///     A client for the Kubernetes PersistentVolumes (v1) API.
    /// </summary>
    public class PersistentVolumeClientV1
        : KubeResourceClient, IPersistentVolumeClientV1
    {
        /// <summary>
        ///     Create a new <see cref="PersistentVolumeClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public PersistentVolumeClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the PersistentVolume with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the PersistentVolume to retrieve.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PersistentVolumeV1"/> representing the current state for the PersistentVolume, or <c>null</c> if no PersistentVolume was found with the specified name.
        /// </returns>
        public async Task<PersistentVolumeV1> Get(string name, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));
            
            return await GetSingleResource<PersistentVolumeV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all PersistentVolumes, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the PersistentVolumes.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PersistentVolumeListV1"/> containing the PersistentVolumes.
        /// </returns>
        public async Task<PersistentVolumeListV1> List(string labelSelector = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<PersistentVolumeListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
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
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<PersistentVolumeV1>> WatchAll(string labelSelector = null)
        {
            return ObserveEvents<PersistentVolumeV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    LabelSelector = labelSelector,
                    Watch = true
                }),
                operationDescription: $"watch all v1/PersistentVolumes with label selector '{labelSelector ?? "<none>"}'"
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
                    Requests.Collection,
                    postBody: newPersistentVolume,
                    cancellationToken: cancellationToken
                )
                .ReadContentAsObjectV1Async<PersistentVolumeV1>("create v1/PersistentVolume resource");
        }

        /// <summary>
        ///     Request deletion of the specified PersistentVolume.
        /// </summary>
        /// <param name="name">
        ///     The name of the PersistentVolume to delete.
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
                .ReadContentAsObjectV1Async<StatusV1>("delete v1/PersistentVolume resource", HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        /// <summary>
        ///     Request templates for the PersistentVolumes (v1) API.
        /// </summary>
        public static class Requests
        {
            /// <summary>
            ///     A collection-level PersistentVolume (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection   = KubeRequest.Create("api/v1/persistentvolumes?labelSelector={LabelSelector?}&watch={Watch?}");

            /// <summary>
            ///     A get-by-name PersistentVolume (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName       = KubeRequest.Create("api/v1/persistentvolumes/{Name}");
        }
    }

    /// <summary>
    ///     A client for the Kubernetes PersistentVolumes (v1) API.
    /// </summary>
    public interface IPersistentVolumeClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the PersistentVolume with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the PersistentVolume to retrieve.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PersistentVolumeV1"/> representing the current state for the PersistentVolume, or <c>null</c> if no PersistentVolume was found with the specified name.
        /// </returns>
        Task<PersistentVolumeV1> Get(string name, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all PersistentVolumes, optionally matching a label selector.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the PersistentVolumes.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="PersistentVolumeListV1"/> containing the PersistentVolumes.
        /// </returns>
        Task<PersistentVolumeListV1> List(string labelSelector = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to PersistentVolumes.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the PersistentVolumes.
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<PersistentVolumeV1>> WatchAll(string labelSelector = null);

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
        Task<PersistentVolumeV1> Create(PersistentVolumeV1 newPersistentVolume, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Request deletion of the specified PersistentVolume.
        /// </summary>
        /// <param name="name">
        ///     The name of the PersistentVolume to delete.
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
