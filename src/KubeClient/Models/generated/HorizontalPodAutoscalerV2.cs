using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HorizontalPodAutoscaler is the configuration for a horizontal pod autoscaler, which automatically manages the replica count of any resource implementing the scale subresource based on the metrics specified.
    /// </summary>
    [KubeObject("HorizontalPodAutoscaler", "autoscaling/v2")]
    [KubeApi(KubeAction.List, "apis/autoscaling/v2/horizontalpodautoscalers")]
    [KubeApi(KubeAction.WatchList, "apis/autoscaling/v2/watch/horizontalpodautoscalers")]
    [KubeApi(KubeAction.List, "apis/autoscaling/v2/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.Create, "apis/autoscaling/v2/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.Get, "apis/autoscaling/v2/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Patch, "apis/autoscaling/v2/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Delete, "apis/autoscaling/v2/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Update, "apis/autoscaling/v2/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/autoscaling/v2/watch/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.DeleteCollection, "apis/autoscaling/v2/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.Get, "apis/autoscaling/v2/namespaces/{namespace}/horizontalpodautoscalers/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/autoscaling/v2/watch/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Patch, "apis/autoscaling/v2/namespaces/{namespace}/horizontalpodautoscalers/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/autoscaling/v2/namespaces/{namespace}/horizontalpodautoscalers/{name}/status")]
    public partial class HorizontalPodAutoscalerV2 : KubeResourceV1
    {
        /// <summary>
        ///     spec is the specification for the behaviour of the autoscaler. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public HorizontalPodAutoscalerSpecV2 Spec { get; set; }

        /// <summary>
        ///     status is the current information about the autoscaler.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public HorizontalPodAutoscalerStatusV2 Status { get; set; }
    }
}
