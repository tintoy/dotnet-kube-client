using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     LoadBalancerStatus represents the status of a load-balancer.
    /// </summary>
    public partial class LoadBalancerStatusV1 : Models.LoadBalancerStatusV1, ITracked
    {
        /// <summary>
        ///     Ingress is a list containing ingress points for the load-balancer. Traffic intended for the service should be sent to these ingress points.
        /// </summary>
        [YamlMember(Alias = "ingress")]
        [JsonProperty("ingress", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.LoadBalancerIngressV1> Ingress { get; set; } = new List<Models.LoadBalancerIngressV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
