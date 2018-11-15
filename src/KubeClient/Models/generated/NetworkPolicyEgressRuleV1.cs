using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NetworkPolicyEgressRule describes a particular set of traffic that is allowed out of pods matched by a NetworkPolicySpec's podSelector. The traffic must match both ports and to. This type is beta-level in 1.8
    /// </summary>
    public partial class NetworkPolicyEgressRuleV1
    {
        /// <summary>
        ///     List of destinations for outgoing traffic of pods selected for this rule. Items in this list are combined using a logical OR operation. If this field is empty or missing, this rule matches all destinations (traffic not restricted by destination). If this field is present and contains at least one item, this rule allows traffic only if the traffic matches at least one item in the to list.
        /// </summary>
        [YamlMember(Alias = "to")]
        [JsonProperty("to", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<NetworkPolicyPeerV1> To { get; } = new List<NetworkPolicyPeerV1>();

        /// <summary>
        ///     Determine whether the <see cref="To"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeTo() => To.Count > 0;

        /// <summary>
        ///     List of destination ports for outgoing traffic. Each item in this list is combined using a logical OR. If this field is empty or missing, this rule matches all ports (traffic not restricted by port). If this field is present and contains at least one item, then this rule allows traffic only if the traffic matches at least one port in the list.
        /// </summary>
        [YamlMember(Alias = "ports")]
        [JsonProperty("ports", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<NetworkPolicyPortV1> Ports { get; } = new List<NetworkPolicyPortV1>();

        /// <summary>
        ///     Determine whether the <see cref="Ports"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePorts() => Ports.Count > 0;
    }
}
