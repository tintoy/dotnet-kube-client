using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceStatus represents the current status of a service.
    /// </summary>
    public partial class ServiceStatusV1
    {
        /// <summary>
        ///     LoadBalancer contains the current status of the load-balancer, if one is present.
        /// </summary>
        [YamlMember(Alias = "loadBalancer")]
        [JsonProperty("loadBalancer", NullValueHandling = NullValueHandling.Ignore)]
        public LoadBalancerStatusV1 LoadBalancer { get; set; }
    }
}
