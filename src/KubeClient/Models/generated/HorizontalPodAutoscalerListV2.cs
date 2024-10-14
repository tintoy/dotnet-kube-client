using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HorizontalPodAutoscalerList is a list of horizontal pod autoscaler objects.
    /// </summary>
    [KubeListItem("HorizontalPodAutoscaler", "autoscaling/v2")]
    [KubeObject("HorizontalPodAutoscalerList", "autoscaling/v2")]
    public partial class HorizontalPodAutoscalerListV2 : KubeResourceListV1<HorizontalPodAutoscalerV2>
    {
        /// <summary>
        ///     items is the list of horizontal pod autoscaler objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<HorizontalPodAutoscalerV2> Items { get; } = new List<HorizontalPodAutoscalerV2>();
    }
}
