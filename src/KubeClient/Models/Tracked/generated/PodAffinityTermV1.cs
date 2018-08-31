using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Defines a set of pods (namely those matching the labelSelector relative to the given namespace(s)) that this pod should be co-located (affinity) or not co-located (anti-affinity) with, where co-located is defined as running on a node whose value of the label with key &lt;topologyKey&gt; tches that of any node on which a pod of the set of pods is running
    /// </summary>
    public partial class PodAffinityTermV1 : Models.PodAffinityTermV1
    {
        /// <summary>
        ///     A label query over a set of resources, in this case pods.
        /// </summary>
        [JsonProperty("labelSelector")]
        [YamlMember(Alias = "labelSelector")]
        public override Models.LabelSelectorV1 LabelSelector
        {
            get
            {
                return base.LabelSelector;
            }
            set
            {
                base.LabelSelector = value;

                __ModifiedProperties__.Add("LabelSelector");
            }
        }


        /// <summary>
        ///     namespaces specifies which namespaces the labelSelector applies to (matches against); null or empty list means "this pod's namespace"
        /// </summary>
        [YamlMember(Alias = "namespaces")]
        [JsonProperty("namespaces", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Namespaces { get; set; } = new List<string>();

        /// <summary>
        ///     This pod should be co-located (affinity) or not co-located (anti-affinity) with the pods matching the labelSelector in the specified namespaces, where co-located is defined as running on a node whose value of the label with key topologyKey matches that of any node on which any of the selected pods is running. For PreferredDuringScheduling pod anti-affinity, empty topologyKey is interpreted as "all topologies" ("all topologies" here means all the topologyKeys indicated by scheduler command-line argument --failure-domains); for affinity and for RequiredDuringScheduling pod anti-affinity, empty topologyKey is not allowed.
        /// </summary>
        [JsonProperty("topologyKey")]
        [YamlMember(Alias = "topologyKey")]
        public override string TopologyKey
        {
            get
            {
                return base.TopologyKey;
            }
            set
            {
                base.TopologyKey = value;

                __ModifiedProperties__.Add("TopologyKey");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
