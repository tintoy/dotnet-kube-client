using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     list of horizontal pod autoscaler objects.
    /// </summary>
    [KubeObject("HorizontalPodAutoscalerList", "autoscaling/v1")]
    public class HorizontalPodAutoscalerListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     list of horizontal pod autoscaler objects.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<HorizontalPodAutoscalerV1> Items { get; set; } = new List<HorizontalPodAutoscalerV1>();
    }
}
