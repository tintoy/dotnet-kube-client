using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     IngressStatus describe the current state of the Ingress.
    /// </summary>
    public partial class IngressStatusV1Beta1 : Models.IngressStatusV1Beta1, ITracked
    {
        /// <summary>
        ///     LoadBalancer contains the current status of the load-balancer.
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
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
