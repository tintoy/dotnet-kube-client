using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceQuota sets aggregate quota restrictions enforced per namespace
    /// </summary>
    [KubeObject("ResourceQuota", "v1")]
    [KubeApi(KubeAction.List, "api/v1/resourcequotas")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/resourcequotas")]
    [KubeApi(KubeAction.List, "api/v1/namespaces/{namespace}/resourcequotas")]
    [KubeApi(KubeAction.Create, "api/v1/namespaces/{namespace}/resourcequotas")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/resourcequotas/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/resourcequotas/{name}")]
    [KubeApi(KubeAction.Delete, "api/v1/namespaces/{namespace}/resourcequotas/{name}")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/resourcequotas/{name}")]
    [KubeApi(KubeAction.WatchList, "api/v1/watch/namespaces/{namespace}/resourcequotas")]
    [KubeApi(KubeAction.DeleteCollection, "api/v1/namespaces/{namespace}/resourcequotas")]
    [KubeApi(KubeAction.Get, "api/v1/namespaces/{namespace}/resourcequotas/{name}/status")]
    [KubeApi(KubeAction.Watch, "api/v1/watch/namespaces/{namespace}/resourcequotas/{name}")]
    [KubeApi(KubeAction.Patch, "api/v1/namespaces/{namespace}/resourcequotas/{name}/status")]
    [KubeApi(KubeAction.Update, "api/v1/namespaces/{namespace}/resourcequotas/{name}/status")]
    public partial class ResourceQuotaV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the desired quota. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceQuotaSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status defines the actual enforced quota and its current usage. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceQuotaStatusV1 Status { get; set; }
    }
}
