using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents the payload for a Kubernetes resource event.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of resource to which the event relates.
    /// </typeparam>
    public class ResourceEventV1<TResource>
        : IResourceEventV1<TResource>
        where TResource : KubeResourceV1
    {
        /// <summary>
        ///     The event type.
        /// </summary>
        [JsonProperty("type")]
        public ResourceEventType EventType { get; set; }

        /// <summary>
        ///     The resource to which the event relates.
        /// </summary>
        [JsonProperty("object")]
        public TResource Resource { get; set; }
    }
}
