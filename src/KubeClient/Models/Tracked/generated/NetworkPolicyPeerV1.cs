using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NetworkPolicyPeer describes a peer to allow traffic from. Exactly one of its fields must be specified.
    /// </summary>
    public partial class NetworkPolicyPeerV1 : Models.NetworkPolicyPeerV1
    {
        /// <summary>
        ///     Selects Namespaces using cluster scoped-labels. This matches all pods in all namespaces selected by this label selector. This field follows standard label selector semantics. If present but empty, this selector selects all namespaces.
        /// </summary>
        [JsonProperty("namespaceSelector")]
        [YamlMember(Alias = "namespaceSelector")]
        public override Models.LabelSelectorV1 NamespaceSelector
        {
            get
            {
                return base.NamespaceSelector;
            }
            set
            {
                base.NamespaceSelector = value;

                __ModifiedProperties__.Add("NamespaceSelector");
            }
        }


        /// <summary>
        ///     This is a label selector which selects Pods in this namespace. This field follows standard label selector semantics. If present but empty, this selector selects all pods in this namespace.
        /// </summary>
        [JsonProperty("podSelector")]
        [YamlMember(Alias = "podSelector")]
        public override Models.LabelSelectorV1 PodSelector
        {
            get
            {
                return base.PodSelector;
            }
            set
            {
                base.PodSelector = value;

                __ModifiedProperties__.Add("PodSelector");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
