using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KubeClient.Models
{
    public class ResourceEventV1<TResource>
        where TResource : KubeResourceV1
    {
        [JsonProperty("type")]
        public string EventType { get; set; }

        [JsonProperty("object")]
        public TResource Resource { get; set; }
    }
}
