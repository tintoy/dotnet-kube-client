namespace KubeClient.Models
{
    /// <summary>
    ///     Represents the payload for a Kubernetes resource event.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of resource to which the event relates.
    /// </typeparam>
    public interface IResourceEventV1<out TResource>
        where TResource : KubeResourceV1
    {
        /// <summary>
        ///     The event type.
        /// </summary>
        ResourceEventType EventType { get; set; }

        /// <summary>
        ///     The resource to which the event relates.
        /// </summary>
        TResource Resource { get; }
    }
}
