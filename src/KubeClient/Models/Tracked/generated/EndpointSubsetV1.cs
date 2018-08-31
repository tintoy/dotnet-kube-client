using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     EndpointSubset is a group of addresses with a common set of ports. The expanded set of endpoints is the Cartesian product of Addresses x Ports. For example, given:
    ///       {
    ///         Addresses: [{"ip": "10.10.1.1"}, {"ip": "10.10.2.2"}],
    ///         Ports:     [{"name": "a", "port": 8675}, {"name": "b", "port": 309}]
    ///       }
    ///     The resulting set of endpoints can be viewed as:
    ///         a: [ 10.10.1.1:8675, 10.10.2.2:8675 ],
    ///         b: [ 10.10.1.1:309, 10.10.2.2:309 ]
    /// </summary>
    public partial class EndpointSubsetV1 : Models.EndpointSubsetV1
    {
        /// <summary>
        ///     IP addresses which offer the related ports that are marked as ready. These endpoints should be considered safe for load balancers and clients to utilize.
        /// </summary>
        [YamlMember(Alias = "addresses")]
        [JsonProperty("addresses", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.EndpointAddressV1> Addresses { get; set; } = new List<Models.EndpointAddressV1>();

        /// <summary>
        ///     IP addresses which offer the related ports but are not currently marked as ready because they have not yet finished starting, have recently failed a readiness check, or have recently failed a liveness check.
        /// </summary>
        [YamlMember(Alias = "notReadyAddresses")]
        [JsonProperty("notReadyAddresses", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.EndpointAddressV1> NotReadyAddresses { get; set; } = new List<Models.EndpointAddressV1>();

        /// <summary>
        ///     Port numbers available on the related IP addresses.
        /// </summary>
        [YamlMember(Alias = "ports")]
        [JsonProperty("ports", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.EndpointPortV1> Ports { get; set; } = new List<Models.EndpointPortV1>();
    }
}
