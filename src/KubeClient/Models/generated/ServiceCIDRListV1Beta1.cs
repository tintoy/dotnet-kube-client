using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceCIDRList contains a list of ServiceCIDR objects.
    /// </summary>
    [KubeListItem("ServiceCIDR", "networking.k8s.io/v1beta1")]
    [KubeObject("ServiceCIDRList", "networking.k8s.io/v1beta1")]
    public partial class ServiceCIDRListV1Beta1 : KubeResourceListV1<ServiceCIDRV1Beta1>
    {
        /// <summary>
        ///     items is the list of ServiceCIDRs.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ServiceCIDRV1Beta1> Items { get; } = new List<ServiceCIDRV1Beta1>();
    }
}
