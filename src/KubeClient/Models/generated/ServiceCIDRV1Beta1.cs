using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceCIDR defines a range of IP addresses using CIDR format (e.g. 192.168.0.0/24 or 2001:db2::/64). This range is used to allocate ClusterIPs to Service objects.
    /// </summary>
    [KubeObject("ServiceCIDR", "networking.k8s.io/v1beta1")]
    [KubeApi(KubeAction.List, "apis/networking.k8s.io/v1beta1/servicecidrs")]
    [KubeApi(KubeAction.Create, "apis/networking.k8s.io/v1beta1/servicecidrs")]
    [KubeApi(KubeAction.Get, "apis/networking.k8s.io/v1beta1/servicecidrs/{name}")]
    [KubeApi(KubeAction.Patch, "apis/networking.k8s.io/v1beta1/servicecidrs/{name}")]
    [KubeApi(KubeAction.Delete, "apis/networking.k8s.io/v1beta1/servicecidrs/{name}")]
    [KubeApi(KubeAction.Update, "apis/networking.k8s.io/v1beta1/servicecidrs/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/networking.k8s.io/v1beta1/watch/servicecidrs")]
    [KubeApi(KubeAction.DeleteCollection, "apis/networking.k8s.io/v1beta1/servicecidrs")]
    [KubeApi(KubeAction.Get, "apis/networking.k8s.io/v1beta1/servicecidrs/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/networking.k8s.io/v1beta1/watch/servicecidrs/{name}")]
    [KubeApi(KubeAction.Patch, "apis/networking.k8s.io/v1beta1/servicecidrs/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/networking.k8s.io/v1beta1/servicecidrs/{name}/status")]
    public partial class ServiceCIDRV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     spec is the desired state of the ServiceCIDR. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceCIDRSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     status represents the current state of the ServiceCIDR. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceCIDRStatusV1Beta1 Status { get; set; }
    }
}
