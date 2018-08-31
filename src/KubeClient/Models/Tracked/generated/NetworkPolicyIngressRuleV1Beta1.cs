using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     This NetworkPolicyIngressRule matches traffic if and only if the traffic matches both ports AND from.
    /// </summary>
    public partial class NetworkPolicyIngressRuleV1Beta1 : Models.NetworkPolicyIngressRuleV1Beta1, ITracked
    {
        /// <summary>
        ///     List of sources which should be able to access the pods selected for this rule. Items in this list are combined using a logical OR operation. If this field is empty or missing, this rule matches all sources (traffic not restricted by source). If this field is present and contains at least on item, this rule allows traffic only if the traffic matches at least one item in the from list.
        /// </summary>
        [YamlMember(Alias = "from")]
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.NetworkPolicyPeerV1Beta1> From { get; set; } = new List<Models.NetworkPolicyPeerV1Beta1>();

        /// <summary>
        ///     List of ports which should be made accessible on the pods selected for this rule. Each item in this list is combined using a logical OR. If this field is empty or missing, this rule matches all ports (traffic not restricted by port). If this field is present and contains at least one item, then this rule allows traffic only if the traffic matches at least one port in the list.
        /// </summary>
        [YamlMember(Alias = "ports")]
        [JsonProperty("ports", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.NetworkPolicyPortV1Beta1> Ports { get; set; } = new List<Models.NetworkPolicyPortV1Beta1>();
    }
}
