using System;
using System.Threading;
using System.Threading.Tasks;

namespace KubeClient.ResourceClients
{
    using Http;
    using Models;

    /// <summary>
    ///     A client for the Kubernetes Events (v1) API.
    /// </summary>
    public class EventClientV1
        : KubeResourceClient, IEventClientV1
    {
        /// <summary>
        ///     Create a new <see cref="EventClientV1"/>.
        /// </summary>
        /// <param name="client">
        ///     The Kubernetes API client.
        /// </param>
        public EventClientV1(IKubeApiClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Get the Event with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Event to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="EventV1"/> representing the current state for the Event, or <c>null</c> if no Event was found with the specified name and namespace.
        /// </returns>
        public async Task<EventV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Argument cannot be null, empty, or entirely composed of whitespace: 'name'.", nameof(name));

            return await GetSingleResource<EventV1>(
                Requests.ByName.WithTemplateParameters(new
                {
                    Name = name,
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Get all Events in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Events.
        /// </param>
        /// <param name="fieldSelector">
        ///     An optional Kubernetes field selector expression used to filter the Events.
        /// </param>
        /// <param name="resourceVersion">
        ///     An optional Kubernetes resource version (<seealso cref="ObjectMetaV1.ResourceVersion"/>) indicating that only events for newer versions should be returned.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="EventListV1"/> containing the events.
        /// </returns>
        public async Task<EventListV1> List(string labelSelector = null, string fieldSelector = null, string resourceVersion = null, string kubeNamespace = null, CancellationToken cancellationToken = default)
        {
            return await GetResourceList<EventListV1>(
                Requests.Collection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    FieldSelector = fieldSelector,
                    ResourceVersion = resourceVersion
                }),
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        ///     Watch for events relating to Events.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Events.
        /// </param>
        /// <param name="fieldSelector">
        ///     An optional Kubernetes field selector expression used to filter the Events.
        /// </param>
        /// <param name="resourceVersion">
        ///     An optional Kubernetes resource version (<seealso cref="ObjectMetaV1.ResourceVersion"/>) indicating that only events for newer versions should be returned.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        public IObservable<IResourceEventV1<EventV1>> WatchAll(string labelSelector = null, string fieldSelector = null, string resourceVersion = null, string kubeNamespace = null)
        {
            return ObserveEvents<EventV1>(
                Requests.WatchCollection.WithTemplateParameters(new
                {
                    Namespace = kubeNamespace ?? KubeClient.DefaultNamespace,
                    LabelSelector = labelSelector,
                    FieldSelector = fieldSelector,
                    ResourceVersion = resourceVersion
                }),
                operationDescription: $"watch all v1/Events with label selector '{labelSelector ?? "<none>"}', field selector '{fieldSelector ?? "<none>"}', and resource version '{resourceVersion ?? "<none>"}' in namespace {kubeNamespace ?? KubeClient.DefaultNamespace}"
            );
        }

        /// <summary>
        ///     Request templates for the Event (v1) API.
        /// </summary>
        static class Requests
        {
            /// <summary>
            ///     A collection-level Event (v1) request.
            /// </summary>
            public static readonly HttpRequest Collection = KubeRequest.Create("/api/v1/namespaces/{Namespace}/events?labelSelector={LabelSelector?}&fieldSelector={FieldSelector?}&resourceVersion={ResourceVersion?}");

            /// <summary>
            ///     A get-by-name Event (v1) request.
            /// </summary>
            public static readonly HttpRequest ByName = KubeRequest.Create("/api/v1/namespaces/{Namespace}/events/{Name}");

            /// <summary>
            ///     A collection-level Event watch (v1) request.
            /// </summary>
            public static readonly HttpRequest WatchCollection = KubeRequest.Create("/api/v1/watch/namespaces/{Namespace}/events?labelSelector={LabelSelector?}&fieldSelector={FieldSelector?}&resourceVersion={ResourceVersion?}");
        }
    }

    /// <summary>
    ///     Represents a client for the Kubernetes Events (v1) API.
    /// </summary>
    public interface IEventClientV1
        : IKubeResourceClient
    {
        /// <summary>
        ///     Get the Event with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name of the Event to retrieve.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="EventV1"/> representing the current state for the Event, or <c>null</c> if no Event was found with the specified name and namespace.
        /// </returns>
        Task<EventV1> Get(string name, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Get all Events in the specified namespace, optionally matching a label selector.
        /// </summary>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Events.
        /// </param>
        /// <param name="fieldSelector">
        ///     An optional Kubernetes field selector expression used to filter the Events.
        /// </param>
        /// <param name="resourceVersion">
        ///     An optional Kubernetes resource version (<seealso cref="ObjectMetaV1.ResourceVersion"/>) indicating that only events for newer versions should be returned.
        /// </param>
        /// <param name="cancellationToken">
        ///     An optional <see cref="CancellationToken"/> that can be used to cancel the request.
        /// </param>
        /// <returns>
        ///     A <see cref="EventListV1"/> containing the events.
        /// </returns>
        Task<EventListV1> List(string labelSelector = null, string fieldSelector = null, string resourceVersion = null, string kubeNamespace = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Watch for events relating to Events.
        /// </summary>
        /// <param name="labelSelector">
        ///     An optional Kubernetes label selector expression used to filter the Events.
        /// </param>
        /// <param name="fieldSelector">
        ///     An optional Kubernetes field selector expression used to filter the Events.
        /// </param>
        /// <param name="resourceVersion">
        ///     An optional Kubernetes resource version (<seealso cref="ObjectMetaV1.ResourceVersion"/>) indicating that only events for newer versions should be returned.
        /// </param>
        /// <param name="kubeNamespace">
        ///     The target Kubernetes namespace (defaults to <see cref="KubeApiClient.DefaultNamespace"/>).
        /// </param>
        /// <returns>
        ///     An <see cref="IObservable{T}"/> representing the event stream.
        /// </returns>
        IObservable<IResourceEventV1<EventV1>> WatchAll(string labelSelector = null, string fieldSelector = null, string resourceVersion = null, string kubeNamespace = null);
    }
}
