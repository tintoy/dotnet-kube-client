using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressLoadBalancerStatus represents the status of a load-balancer.
    /// </summary>
    public partial class IngressLoadBalancerStatusV1
    {
        /// <summary>
        ///     ingress is a list containing ingress points for the load-balancer.
        /// </summary>
        [YamlMember(Alias = "ingress")]
        [JsonProperty("ingress", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<IngressLoadBalancerIngressV1> Ingress { get; } = new List<IngressLoadBalancerIngressV1>();

        /// <summary>
        ///     Determine whether the <see cref="Ingress"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeIngress() => Ingress.Count > 0;
    }
}
