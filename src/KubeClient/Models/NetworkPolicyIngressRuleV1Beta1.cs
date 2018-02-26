using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     This NetworkPolicyIngressRule matches traffic if and only if the traffic matches both ports AND from.
    /// </summary>
    [KubeObject("NetworkPolicyIngressRule", "v1beta1")]
    public class NetworkPolicyIngressRuleV1Beta1
    {
        /// <summary>
        ///     List of sources which should be able to access the pods selected for this rule. Items in this list are combined using a logical OR operation. If this field is empty or missing, this rule matches all sources (traffic not restricted by source). If this field is present and contains at least on item, this rule allows traffic only if the traffic matches at least one item in the from list.
        /// </summary>
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public List<NetworkPolicyPeerV1Beta1> From { get; set; } = new List<NetworkPolicyPeerV1Beta1>();

        /// <summary>
        ///     List of ports which should be made accessible on the pods selected for this rule. Each item in this list is combined using a logical OR. If this field is empty or missing, this rule matches all ports (traffic not restricted by port). If this field is present and contains at least one item, then this rule allows traffic only if the traffic matches at least one port in the list.
        /// </summary>
        [JsonProperty("ports", NullValueHandling = NullValueHandling.Ignore)]
        public List<NetworkPolicyPortV1Beta1> Ports { get; set; } = new List<NetworkPolicyPortV1Beta1>();
    }
}
