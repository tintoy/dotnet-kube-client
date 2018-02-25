using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents the payload for a Kubernetes resource event.
    /// </summary>
    /// <typeparam name="TResource">
    ///     The type of resource to which the event relates.
    /// </typeparam>
    public class ResourceEventV1<TResource>
        where TResource : KubeResourceV1
    {
        /// <summary>
        ///     The even type.
        /// </summary>
        [JsonProperty("type")]
        public string EventType { get; set; }

        /// <summary>
        ///     The resource to which the event relates.
        /// </summary>
        [JsonProperty("object")]
        public TResource Resource { get; set; }
    }
}
