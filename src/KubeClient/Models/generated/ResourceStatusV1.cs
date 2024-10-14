using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class ResourceStatusV1
    {
        /// <summary>
        ///     Name of the resource. Must be unique within the pod and match one of the resources from the pod spec.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     List of unique Resources health. Each element in the list contains an unique resource ID and resource health. At a minimum, ResourceID must uniquely identify the Resource allocated to the Pod on the Node for the lifetime of a Pod. See ResourceID type for it's definition.
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ResourceHealthV1> Resources { get; } = new List<ResourceHealthV1>();

        /// <summary>
        ///     Determine whether the <see cref="Resources"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeResources() => Resources.Count > 0;
    }
}
