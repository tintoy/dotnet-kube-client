using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     The weights of all of the matched WeightedPodAffinityTerm fields are added per-node to find the most preferred node(s)
    /// </summary>
    public partial class WeightedPodAffinityTermV1
    {
        /// <summary>
        ///     Required. A pod affinity term, associated with the corresponding weight.
        /// </summary>
        [YamlMember(Alias = "podAffinityTerm")]
        [JsonProperty("podAffinityTerm", NullValueHandling = NullValueHandling.Include)]
        public PodAffinityTermV1 PodAffinityTerm { get; set; }

        /// <summary>
        ///     weight associated with matching the corresponding podAffinityTerm, in the range 1-100.
        /// </summary>
        [YamlMember(Alias = "weight")]
        [JsonProperty("weight", NullValueHandling = NullValueHandling.Include)]
        public int Weight { get; set; }
    }
}
