using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     IPBlock describes a particular CIDR (Ex. "192.168.1.1/24") that is allowed to the pods matched by a NetworkPolicySpec's podSelector. The except entry describes CIDRs that should not be included within this rule.
    /// </summary>
    [KubeObject("IPBlock", "v1")]
    public partial class IPBlockV1
    {
        /// <summary>
        ///     CIDR is a string representing the IP Block Valid examples are "192.168.1.1/24"
        /// </summary>
        [JsonProperty("cidr")]
        public string Cidr { get; set; }

        /// <summary>
        ///     Except is a slice of CIDRs that should not be included within an IP Block Valid examples are "192.168.1.1/24" Except values will be rejected if they are outside the CIDR range
        /// </summary>
        [JsonProperty("except", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Except { get; set; } = new List<string>();
    }
}
