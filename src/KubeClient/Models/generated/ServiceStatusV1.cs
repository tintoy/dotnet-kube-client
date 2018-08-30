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
        [JsonProperty("loadBalancer")]
        [YamlMember(Alias = "loadBalancer")]
        public virtual LoadBalancerStatusV1 LoadBalancer { get; set; }
    }
}
