using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Ingress is a collection of rules that allow inbound connections to reach the endpoints defined by a backend. An Ingress can be configured to give services externally-reachable urls, load balance traffic, terminate SSL, offer name based virtual hosting etc.
    /// </summary>
    [KubeObject("Ingress", "extensions/v1beta1")]
    [KubeApi(KubeAction.List, "apis/extensions/v1beta1/ingresses")]
    [KubeApi(KubeAction.WatchList, "apis/extensions/v1beta1/watch/ingresses")]
    [KubeApi(KubeAction.List, "apis/extensions/v1beta1/namespaces/{namespace}/ingresses")]
    [KubeApi(KubeAction.Create, "apis/extensions/v1beta1/namespaces/{namespace}/ingresses")]
    [KubeApi(KubeAction.Get, "apis/extensions/v1beta1/namespaces/{namespace}/ingresses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/extensions/v1beta1/namespaces/{namespace}/ingresses/{name}")]
    [KubeApi(KubeAction.Delete, "apis/extensions/v1beta1/namespaces/{namespace}/ingresses/{name}")]
    [KubeApi(KubeAction.Update, "apis/extensions/v1beta1/namespaces/{namespace}/ingresses/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/extensions/v1beta1/watch/namespaces/{namespace}/ingresses")]
    [KubeApi(KubeAction.DeleteCollection, "apis/extensions/v1beta1/namespaces/{namespace}/ingresses")]
    [KubeApi(KubeAction.Get, "apis/extensions/v1beta1/namespaces/{namespace}/ingresses/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/extensions/v1beta1/watch/namespaces/{namespace}/ingresses/{name}")]
    [KubeApi(KubeAction.Patch, "apis/extensions/v1beta1/namespaces/{namespace}/ingresses/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/extensions/v1beta1/namespaces/{namespace}/ingresses/{name}/status")]
    public partial class IngressV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec is the desired state of the Ingress. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public IngressSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Status is the current state of the Ingress. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public IngressStatusV1Beta1 Status { get; set; }
    }
}
