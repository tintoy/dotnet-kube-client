using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressStatus describe the current state of the Ingress.
    /// </summary>
    public partial class IngressStatusV1
    {
        /// <summary>
        ///     loadBalancer contains the current status of the load-balancer.
        /// </summary>
        [YamlMember(Alias = "loadBalancer")]
        [JsonProperty("loadBalancer", NullValueHandling = NullValueHandling.Ignore)]
        public IngressLoadBalancerStatusV1 LoadBalancer { get; set; }
    }
}
