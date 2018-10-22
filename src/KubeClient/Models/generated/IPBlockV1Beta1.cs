using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DEPRECATED 1.9 - This group version of IPBlock is deprecated by networking/v1/IPBlock. IPBlock describes a particular CIDR (Ex. "192.168.1.1/24") that is allowed to the pods matched by a NetworkPolicySpec's podSelector. The except entry describes CIDRs that should not be included within this rule.
    /// </summary>
    public partial class IPBlockV1Beta1
    {
        /// <summary>
        ///     CIDR is a string representing the IP Block Valid examples are "192.168.1.1/24"
        /// </summary>
        [YamlMember(Alias = "cidr")]
        [JsonProperty("cidr", NullValueHandling = NullValueHandling.Include)]
        public string Cidr { get; set; }

        /// <summary>
        ///     Except is a slice of CIDRs that should not be included within an IP Block Valid examples are "192.168.1.1/24" Except values will be rejected if they are outside the CIDR range
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
