using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DEPRECATED 1.9 - This group version of NetworkPolicyPeer is deprecated by networking/v1/NetworkPolicyPeer.
    /// </summary>
    public partial class NetworkPolicyPeerV1Beta1
    {
        /// <summary>
        ///     IPBlock defines policy on a particular IPBlock. If this field is set then neither of the other fields can be.
        /// </summary>
        [YamlMember(Alias = "ipBlock")]
        [JsonProperty("ipBlock", NullValueHandling = NullValueHandling.Ignore)]
        public IPBlockV1Beta1 IpBlock { get; set; }

        /// <summary>
        ///     Selects Namespaces using cluster-scoped labels. This field follows standard label selector semantics; if present but empty, it selects all namespaces.
        ///     
        ///     If PodSelector is also set, then the NetworkPolicyPeer as a whole selects the Pods matching PodSelector in the Namespaces selected by NamespaceSelector. Otherwise it selects all Pods in the Namespaces selected by NamespaceSelector.
        /// </summary>
        [YamlMember(Alias = "namespaceSelector")]
        [JsonProperty("namespaceSelector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 NamespaceSelector { get; set; }

        /// <summary>
        ///     This is a label selector which selects Pods. This field follows standard label selector semantics; if present but empty, it selects all pods.
        ///     
        ///     If NamespaceSelector is also set, then the NetworkPolicyPeer as a whole selects the Pods matching PodSelector in the Namespaces selected by NamespaceSelector. Otherwise it selects the Pods matching PodSelector in the policy's own Namespace.
        /// </summary>
        [YamlMember(Alias = "podSelector")]
        [JsonProperty("podSelector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 PodSelector { get; set; }
    }
}
