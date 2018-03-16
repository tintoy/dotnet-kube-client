using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     list of horizontal pod autoscaler objects.
    /// </summary>
    [KubeObject("HorizontalPodAutoscalerList", "autoscaling/v1")]
    public class HorizontalPodAutoscalerListV1 : KubeResourceListV1<HorizontalPodAutoscalerV1>
    {
        /// <summary>
        ///     list of horizontal pod autoscaler objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<HorizontalPodAutoscalerV1> Items { get; } = new List<HorizontalPodAutoscalerV1>();
    }
}
