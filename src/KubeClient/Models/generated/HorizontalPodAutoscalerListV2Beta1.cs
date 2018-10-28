using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HorizontalPodAutoscaler is a list of horizontal pod autoscaler objects.
    /// </summary>
    [KubeListItem("HorizontalPodAutoscaler", "autoscaling/v2beta1")]
    [KubeObject("HorizontalPodAutoscalerList", "autoscaling/v2beta1")]
    public partial class HorizontalPodAutoscalerListV2Beta1 : KubeResourceListV1<HorizontalPodAutoscalerV2Beta1>
    {
        /// <summary>
        ///     items is the list of horizontal pod autoscaler objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<HorizontalPodAutoscalerV2Beta1> Items { get; } = new List<HorizontalPodAutoscalerV2Beta1>();
    }
}
