using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    [KubeResource("NetworkPolicySpec", "v1beta1")]
    public class NetworkPolicySpecV1Beta1
    {
        /// <summary>
        ///     Selects the pods to which this NetworkPolicy object applies.  The array of ingress rules is applied to any pods selected by this field. Multiple network policies can select the same set of pods.  In this case, the ingress rules for each are combined additively. This field is NOT optional and follows standard label selector semantics. An empty podSelector matches all pods in this namespace.
        /// </summary>
        [JsonProperty("podSelector")]
        public LabelSelectorV1 PodSelector { get; set; }

        /// <summary>
        ///     List of ingress rules to be applied to the selected pods. Traffic is allowed to a pod if there are no NetworkPolicies selecting the pod OR if the traffic source is the pod's local node, OR if the traffic matches at least one ingress rule across all of the NetworkPolicy objects whose podSelector matches the pod. If this field is empty then this NetworkPolicy does not allow any traffic (and serves solely to ensure that the pods it selects are isolated by default).
        /// </summary>
        [JsonProperty("ingress", NullValueHandling = NullValueHandling.Ignore)]
        public List<NetworkPolicyIngressRuleV1Beta1> Ingress { get; set; } = new List<NetworkPolicyIngressRuleV1Beta1>();
    }
}
