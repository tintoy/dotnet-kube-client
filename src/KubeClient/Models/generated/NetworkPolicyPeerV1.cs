using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NetworkPolicyPeer describes a peer to allow traffic to/from. Only certain combinations of fields are allowed
    /// </summary>
    public partial class NetworkPolicyPeerV1
    {
        /// <summary>
        ///     ipBlock defines policy on a particular IPBlock. If this field is set then neither of the other fields can be.
        /// </summary>
        [YamlMember(Alias = "ipBlock")]
        [JsonProperty("ipBlock", NullValueHandling = NullValueHandling.Ignore)]
        public IPBlockV1 IpBlock { get; set; }

        /// <summary>
        ///     namespaceSelector selects namespaces using cluster-scoped labels. This field follows standard label selector semantics; if present but empty, it selects all namespaces.
        ///     
        ///     If podSelector is also set, then the NetworkPolicyPeer as a whole selects the pods matching podSelector in the namespaces selected by namespaceSelector. Otherwise it selects all pods in the namespaces selected by namespaceSelector.
        /// </summary>
        [YamlMember(Alias = "namespaceSelector")]
        [JsonProperty("namespaceSelector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 NamespaceSelector { get; set; }

        /// <summary>
        ///     podSelector is a label selector which selects pods. This field follows standard label selector semantics; if present but empty, it selects all pods.
        ///     
        ///     If namespaceSelector is also set, then the NetworkPolicyPeer as a whole selects the pods matching podSelector in the Namespaces selected by NamespaceSelector. Otherwise it selects the pods matching podSelector in the policy's own namespace.
        /// </summary>
        [YamlMember(Alias = "podSelector")]
        [JsonProperty("podSelector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 PodSelector { get; set; }
    }
}
