using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HorizontalPodAutoscaler is the configuration for a horizontal pod autoscaler, which automatically manages the replica count of any resource implementing the scale subresource based on the metrics specified.
    /// </summary>
    [KubeObject("HorizontalPodAutoscaler", "autoscaling/v2beta1")]
    [KubeApi(KubeAction.List, "apis/autoscaling/v2beta1/horizontalpodautoscalers")]
    [KubeApi(KubeAction.WatchList, "apis/autoscaling/v2beta1/watch/horizontalpodautoscalers")]
    [KubeApi(KubeAction.List, "apis/autoscaling/v2beta1/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.Create, "apis/autoscaling/v2beta1/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.Get, "apis/autoscaling/v2beta1/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Patch, "apis/autoscaling/v2beta1/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Delete, "apis/autoscaling/v2beta1/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Update, "apis/autoscaling/v2beta1/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/autoscaling/v2beta1/watch/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.DeleteCollection, "apis/autoscaling/v2beta1/namespaces/{namespace}/horizontalpodautoscalers")]
    [KubeApi(KubeAction.Get, "apis/autoscaling/v2beta1/namespaces/{namespace}/horizontalpodautoscalers/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/autoscaling/v2beta1/watch/namespaces/{namespace}/horizontalpodautoscalers/{name}")]
    [KubeApi(KubeAction.Patch, "apis/autoscaling/v2beta1/namespaces/{namespace}/horizontalpodautoscalers/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/autoscaling/v2beta1/namespaces/{namespace}/horizontalpodautoscalers/{name}/status")]
    public partial class HorizontalPodAutoscalerV2Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     spec is the specification for the behaviour of the autoscaler. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public HorizontalPodAutoscalerSpecV2Beta1 Spec { get; set; }

        /// <summary>
        ///     status is the current information about the autoscaler.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public HorizontalPodAutoscalerStatusV2Beta1 Status { get; set; }
    }
}
