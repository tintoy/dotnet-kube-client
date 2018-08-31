using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     LoadBalancerIngress represents the status of a load-balancer ingress point: traffic intended for the service should be sent to an ingress point.
    /// </summary>
    public partial class LoadBalancerIngressV1 : Models.LoadBalancerIngressV1, ITracked
    {
        /// <summary>
        ///     Hostname is set for load-balancer ingress points that are DNS based (typically AWS load-balancers)
        /// </summary>
        [JsonProperty("hostname")]
        [YamlMember(Alias = "hostname")]
        public override string Hostname
        {
            get
            {
                return base.Hostname;
            }
            set
            {
                base.Hostname = value;

                __ModifiedProperties__.Add("Hostname");
            }
        }


        /// <summary>
        ///     IP is set for load-balancer ingress points that are IP based (typically GCE or OpenStack load-balancers)
        /// </summary>
        [JsonProperty("ip")]
        [YamlMember(Alias = "ip")]
        public override string Ip
        {
            get
            {
                return base.Ip;
            }
            set
            {
                base.Ip = value;

                __ModifiedProperties__.Add("Ip");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
