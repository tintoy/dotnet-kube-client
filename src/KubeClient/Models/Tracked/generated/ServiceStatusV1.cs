using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ServiceStatus represents the current status of a service.
    /// </summary>
    public partial class ServiceStatusV1 : Models.ServiceStatusV1, ITracked
    {
        /// <summary>
        ///     LoadBalancer contains the current status of the load-balancer, if one is present.
        /// </summary>
        [JsonProperty("loadBalancer")]
        [YamlMember(Alias = "loadBalancer")]
        public override Models.LoadBalancerStatusV1 LoadBalancer
        {
            get
            {
                return base.LoadBalancer;
            }
            set
            {
                base.LoadBalancer = value;

                __ModifiedProperties__.Add("LoadBalancer");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
