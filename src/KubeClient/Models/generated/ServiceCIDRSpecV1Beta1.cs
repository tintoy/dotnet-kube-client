using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceCIDRSpec define the CIDRs the user wants to use for allocating ClusterIPs for Services.
    /// </summary>
    public partial class ServiceCIDRSpecV1Beta1
    {
        /// <summary>
        ///     CIDRs defines the IP blocks in CIDR notation (e.g. "192.168.0.0/24" or "2001:db8::/64") from which to assign service cluster IPs. Max of two CIDRs is allowed, one of each IP family. This field is immutable.
        /// </summary>
        [YamlMember(Alias = "cidrs")]
        [JsonProperty("cidrs", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Cidrs { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Cidrs"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeCidrs() => Cidrs.Count > 0;
    }
}
