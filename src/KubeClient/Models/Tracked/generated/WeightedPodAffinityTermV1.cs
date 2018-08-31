using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     The weights of all of the matched WeightedPodAffinityTerm fields are added per-node to find the most preferred node(s)
    /// </summary>
    public partial class WeightedPodAffinityTermV1 : Models.WeightedPodAffinityTermV1, ITracked
    {
        /// <summary>
        ///     Required. A pod affinity term, associated with the corresponding weight.
        /// </summary>
        [JsonProperty("podAffinityTerm")]
        [YamlMember(Alias = "podAffinityTerm")]
        public override Models.PodAffinityTermV1 PodAffinityTerm
        {
            get
            {
                return base.PodAffinityTerm;
            }
            set
            {
                base.PodAffinityTerm = value;

                __ModifiedProperties__.Add("PodAffinityTerm");
            }
        }


        /// <summary>
        ///     weight associated with matching the corresponding podAffinityTerm, in the range 1-100.
        /// </summary>
        [JsonProperty("weight")]
        [YamlMember(Alias = "weight")]
        public override int Weight
        {
            get
            {
                return base.Weight;
            }
            set
            {
                base.Weight = value;

                __ModifiedProperties__.Add("Weight");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
