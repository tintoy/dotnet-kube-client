using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Node affinity is a group of node affinity scheduling rules.
    /// </summary>
    public partial class NodeAffinityV1 : Models.NodeAffinityV1, ITracked
    {
        /// <summary>
        ///     The scheduler will prefer to schedule pods to nodes that satisfy the affinity expressions specified by this field, but it may choose a node that violates one or more of the expressions. The node that is most preferred is the one with the greatest sum of weights, i.e. for each node that meets all of the scheduling requirements (resource request, requiredDuringScheduling affinity expressions, etc.), compute a sum by iterating through the elements of this field and adding "weight" to the sum if the node matches the corresponding matchExpressions; the node(s) with the highest sum are the most preferred.
        /// </summary>
        [YamlMember(Alias = "preferredDuringSchedulingIgnoredDuringExecution")]
        [JsonProperty("preferredDuringSchedulingIgnoredDuringExecution", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.PreferredSchedulingTermV1> PreferredDuringSchedulingIgnoredDuringExecution { get; set; } = new List<Models.PreferredSchedulingTermV1>();

        /// <summary>
        ///     If the affinity requirements specified by this field are not met at scheduling time, the pod will not be scheduled onto the node. If the affinity requirements specified by this field cease to be met at some point during pod execution (e.g. due to an update), the system may or may not try to eventually evict the pod from its node.
        /// </summary>
        [JsonProperty("requiredDuringSchedulingIgnoredDuringExecution")]
        [YamlMember(Alias = "requiredDuringSchedulingIgnoredDuringExecution")]
        public override Models.NodeSelectorV1 RequiredDuringSchedulingIgnoredDuringExecution
        {
            get
            {
                return base.RequiredDuringSchedulingIgnoredDuringExecution;
            }
            set
            {
                base.RequiredDuringSchedulingIgnoredDuringExecution = value;

                __ModifiedProperties__.Add("RequiredDuringSchedulingIgnoredDuringExecution");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
