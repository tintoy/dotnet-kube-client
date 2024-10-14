using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LoadBalancerIngress represents the status of a load-balancer ingress point: traffic intended for the service should be sent to an ingress point.
    /// </summary>
    public partial class LoadBalancerIngressV1
    {
        /// <summary>
        ///     Hostname is set for load-balancer ingress points that are DNS based (typically AWS load-balancers)
        /// </summary>
        [YamlMember(Alias = "hostname")]
        [JsonProperty("hostname", NullValueHandling = NullValueHandling.Ignore)]
        public string Hostname { get; set; }

        /// <summary>
        ///     IPMode specifies how the load-balancer IP behaves, and may only be specified when the ip field is specified. Setting this to "VIP" indicates that traffic is delivered to the node with the destination set to the load-balancer's IP and port. Setting this to "Proxy" indicates that traffic is delivered to the node or pod with the destination set to the node's IP and node port or the pod's IP and port. Service implementations may use this information to adjust traffic routing.
        /// </summary>
        [YamlMember(Alias = "ipMode")]
        [JsonProperty("ipMode", NullValueHandling = NullValueHandling.Ignore)]
        public string IpMode { get; set; }

        /// <summary>
        ///     IP is set for load-balancer ingress points that are IP based (typically GCE or OpenStack load-balancers)
        /// </summary>
        [YamlMember(Alias = "ip")]
        [JsonProperty("ip", NullValueHandling = NullValueHandling.Ignore)]
        public string Ip { get; set; }

        /// <summary>
        ///     Ports is a list of records of service ports If used, every port defined in the service should have an entry in it
        /// </summary>
        [YamlMember(Alias = "ports")]
        [JsonProperty("ports", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PortStatusV1> Ports { get; } = new List<PortStatusV1>();

        /// <summary>
        ///     Determine whether the <see cref="Ports"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePorts() => Ports.Count > 0;
    }
}
