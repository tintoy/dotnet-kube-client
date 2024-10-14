using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Ingress is a collection of rules that allow inbound connections to reach the endpoints defined by a backend. An Ingress can be configured to give services externally-reachable urls, load balance traffic, terminate SSL, offer name based virtual hosting etc.
    /// </summary>
    [KubeObject("Ingress", "networking.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/networking.k8s.io/v1/ingresses")]
    [KubeApi(KubeAction.WatchList, "apis/networking.k8s.io/v1/watch/ingresses")]
    [KubeApi(KubeAction.List, "apis/networking.k8s.io/v1/namespaces/{namespace}/ingresses")]
    [KubeApi(KubeAction.Create, "apis/networking.k8s.io/v1/namespaces/{namespace}/ingresses")]
    [KubeApi(KubeAction.Get, "apis/networking.k8s.io/v1/namespaces/{namespace}/ingresses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/networking.k8s.io/v1/namespaces/{namespace}/ingresses/{name}")]
    [KubeApi(KubeAction.Delete, "apis/networking.k8s.io/v1/namespaces/{namespace}/ingresses/{name}")]
    [KubeApi(KubeAction.Update, "apis/networking.k8s.io/v1/namespaces/{namespace}/ingresses/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/networking.k8s.io/v1/watch/namespaces/{namespace}/ingresses")]
    [KubeApi(KubeAction.DeleteCollection, "apis/networking.k8s.io/v1/namespaces/{namespace}/ingresses")]
    [KubeApi(KubeAction.Get, "apis/networking.k8s.io/v1/namespaces/{namespace}/ingresses/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/networking.k8s.io/v1/watch/namespaces/{namespace}/ingresses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/networking.k8s.io/v1/namespaces/{namespace}/ingresses/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/networking.k8s.io/v1/namespaces/{namespace}/ingresses/{name}/status")]
    public partial class IngressV1 : KubeResourceV1
    {
        /// <summary>
        ///     spec is the desired state of the Ingress. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public IngressSpecV1 Spec { get; set; }

        /// <summary>
        ///     status is the current state of the Ingress. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public IngressStatusV1 Status { get; set; }
    }
}
