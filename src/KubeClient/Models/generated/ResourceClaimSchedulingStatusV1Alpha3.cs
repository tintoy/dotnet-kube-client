using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceClaimSchedulingStatus contains information about one particular ResourceClaim with "WaitForFirstConsumer" allocation mode.
    /// </summary>
    public partial class ResourceClaimSchedulingStatusV1Alpha3
    {
        /// <summary>
        ///     Name matches the pod.spec.resourceClaims[*].Name field.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     UnsuitableNodes lists nodes that the ResourceClaim cannot be allocated for.
        ///     
        ///     The size of this field is limited to 128, the same as for PodSchedulingSpec.PotentialNodes. This may get increased in the future, but not reduced.
        /// </summary>
        [YamlMember(Alias = "unsuitableNodes")]
        [JsonProperty("unsuitableNodes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> UnsuitableNodes { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="UnsuitableNodes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeUnsuitableNodes() => UnsuitableNodes.Count > 0;
    }
}
