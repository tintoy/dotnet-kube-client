using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IPBlock describes a particular CIDR (Ex. "192.168.1.0/24","2001:db8::/64") that is allowed to the pods matched by a NetworkPolicySpec's podSelector. The except entry describes CIDRs that should not be included within this rule.
    /// </summary>
    public partial class IPBlockV1
    {
        /// <summary>
        ///     cidr is a string representing the IPBlock Valid examples are "192.168.1.0/24" or "2001:db8::/64"
        /// </summary>
        [YamlMember(Alias = "cidr")]
        [JsonProperty("cidr", NullValueHandling = NullValueHandling.Include)]
        public string Cidr { get; set; }

        /// <summary>
        ///     except is a slice of CIDRs that should not be included within an IPBlock Valid examples are "192.168.1.0/24" or "2001:db8::/64" Except values will be rejected if they are outside the cidr range
        /// </summary>
        [YamlMember(Alias = "except")]
        [JsonProperty("except", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Except { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Except"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeExcept() => Except.Count > 0;
    }
}
