using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IPAddress represents a single IP of a single IP Family. The object is designed to be used by APIs that operate on IP addresses. The object is used by the Service core API for allocation of IP addresses. An IP address can be represented in different formats, to guarantee the uniqueness of the IP, the name of the object is the IP address in canonical format, four decimal digits separated by dots suppressing leading zeros for IPv4 and the representation defined by RFC 5952 for IPv6. Valid: 192.168.1.5 or 2001:db8::1 or 2001:db8:aaaa:bbbb:cccc:dddd:eeee:1 Invalid: 10.01.2.3 or 2001:db8:0:0:0::1
    /// </summary>
    [KubeObject("IPAddress", "networking.k8s.io/v1beta1")]
    [KubeApi(KubeAction.List, "apis/networking.k8s.io/v1beta1/ipaddresses")]
    [KubeApi(KubeAction.Create, "apis/networking.k8s.io/v1beta1/ipaddresses")]
    [KubeApi(KubeAction.Get, "apis/networking.k8s.io/v1beta1/ipaddresses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/networking.k8s.io/v1beta1/ipaddresses/{name}")]
    [KubeApi(KubeAction.Delete, "apis/networking.k8s.io/v1beta1/ipaddresses/{name}")]
    [KubeApi(KubeAction.Update, "apis/networking.k8s.io/v1beta1/ipaddresses/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/networking.k8s.io/v1beta1/watch/ipaddresses")]
    [KubeApi(KubeAction.DeleteCollection, "apis/networking.k8s.io/v1beta1/ipaddresses")]
    [KubeApi(KubeAction.Watch, "apis/networking.k8s.io/v1beta1/watch/ipaddresses/{name}")]
    public partial class IPAddressV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     spec is the desired state of the IPAddress. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public IPAddressSpecV1Beta1 Spec { get; set; }
    }
}
