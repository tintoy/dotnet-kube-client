using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressLoadBalancerIngress represents the status of a load-balancer ingress point.
    /// </summary>
    public partial class IngressLoadBalancerIngressV1
    {
        /// <summary>
        ///     hostname is set for load-balancer ingress points that are DNS based.
        /// </summary>
        [YamlMember(Alias = "hostname")]
        [JsonProperty("hostname", NullValueHandling = NullValueHandling.Ignore)]
        public string Hostname { get; set; }

        /// <summary>
        ///     ip is set for load-balancer ingress points that are IP based.
        /// </summary>
        [YamlMember(Alias = "ip")]
        [JsonProperty("ip", NullValueHandling = NullValueHandling.Ignore)]
        public string Ip { get; set; }

        /// <summary>
        ///     ports provides information about the ports exposed by this LoadBalancer.
        /// </summary>
        [YamlMember(Alias = "ports")]
        [JsonProperty("ports", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<IngressPortStatusV1> Ports { get; } = new List<IngressPortStatusV1>();

        /// <summary>
        ///     Determine whether the <see cref="Ports"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePorts() => Ports.Count > 0;
    }
}
