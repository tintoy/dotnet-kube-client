using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     configuration of a horizontal pod autoscaler.
    /// </summary>
    [KubeObject("HorizontalPodAutoscaler", "autoscaling/v1")]
    [KubeApi(KubeAction.List, "apis/autoscaling/v1/horizontalpodautoscalers")]
    [KubeApi(KubeAction.WatchList, "apis/autoscaling/v1/watch/horizontalpodautoscalers")]
    [KubeApi(KubeAction.List, "apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.Create, "apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.Get, "apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Patch, "apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Delete, "apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Update, "apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/autoscaling/v1/watch/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.DeleteCollection, "apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.Get, "apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/autoscaling/v1/watch/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Patch, "apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers/{name}/status")]
    public partial class HorizontalPodAutoscalerV1 : KubeResourceV1
    {
        /// <summary>
        ///     behaviour of autoscaler. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public HorizontalPodAutoscalerSpecV1 Spec { get; set; }

        /// <summary>
        ///     current information about the autoscaler.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public HorizontalPodAutoscalerStatusV1 Status { get; set; }
    }
}
