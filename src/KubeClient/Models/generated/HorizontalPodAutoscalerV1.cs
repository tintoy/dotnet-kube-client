using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     configuration of a horizontal pod autoscaler.
    /// </summary>
    [KubeObject("HorizontalPodAutoscaler", "v1")]
    [KubeApi("apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers/{name}", KubeAction.Delete)]
    [KubeApi("apis/autoscaling/v1/namespaces/{namespace}/horizontalpodautoscalers/{name}/status", KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("apis/autoscaling/v1/watch/namespaces/{namespace}/horizontalpodautoscalers", KubeAction.WatchList)]
    [KubeApi("apis/autoscaling/v1/watch/namespaces/{namespace}/horizontalpodautoscalers/{name}", KubeAction.Watch)]
    public partial class HorizontalPodAutoscalerV1 : KubeResourceV1
    {
        /// <summary>
        ///     behaviour of autoscaler. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public HorizontalPodAutoscalerSpecV1 Spec { get; set; }

        /// <summary>
        ///     current information about the autoscaler.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public HorizontalPodAutoscalerStatusV1 Status { get; set; }
    }
}
