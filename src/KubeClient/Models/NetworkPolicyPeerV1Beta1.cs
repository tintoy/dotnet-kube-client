using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    [KubeObject("NetworkPolicyPeer", "v1beta1")]
    public class NetworkPolicyPeerV1Beta1
    {
        /// <summary>
        ///     Selects Namespaces using cluster scoped-labels.  This matches all pods in all namespaces selected by this label selector. This field follows standard label selector semantics. If present but empty, this selector selects all namespaces.
        /// </summary>
        [JsonProperty("namespaceSelector")]
        public LabelSelectorV1 NamespaceSelector { get; set; }

        /// <summary>
        ///     This is a label selector which selects Pods in this namespace. This field follows standard label selector semantics. If present but empty, this selector selects all pods in this namespace.
        /// </summary>
        [JsonProperty("podSelector")]
        public LabelSelectorV1 PodSelector { get; set; }
    }
}
