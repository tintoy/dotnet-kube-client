using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     APIResourceList is a list of APIResource, it is used to expose the name of the resources supported in a specific group and version, and if the resource is namespaced.
    /// </summary>
    public partial class APIResourceListV1 : KubeObjectV1
    {
        /// <summary>
        ///     groupVersion is the group and version this APIResourceList is for.
        /// </summary>
        [YamlMember(Alias = "groupVersion")]
        [JsonProperty("groupVersion", NullValueHandling = NullValueHandling.Include)]
        public string GroupVersion { get; set; }

        /// <summary>
        ///     resources contains the name of the resources and if they are namespaced.
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<APIResourceV1> Resources { get; } = new List<APIResourceV1>();
    }
}
