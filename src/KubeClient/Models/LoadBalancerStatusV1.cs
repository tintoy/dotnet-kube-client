using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     LoadBalancerStatus represents the status of a load-balancer.
    /// </summary>
    [KubeResource("LoadBalancerStatus", "v1")]
    public class LoadBalancerStatusV1
    {
        /// <summary>
        ///     Ingress is a list containing ingress points for the load-balancer. Traffic intended for the service should be sent to these ingress points.
        /// </summary>
        [JsonProperty("ingress", NullValueHandling = NullValueHandling.Ignore)]
        public List<LoadBalancerIngressV1> Ingress { get; set; } = new List<LoadBalancerIngressV1>();
    }
}
