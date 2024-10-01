using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HorizontalPodAutoscalerBehavior configures the scaling behavior of the target in both Up and Down directions (scaleUp and scaleDown fields respectively).
    /// </summary>
    public partial class HorizontalPodAutoscalerBehaviorV2
    {
        /// <summary>
        ///     scaleDown is scaling policy for scaling Down. If not set, the default value is to allow to scale down to minReplicas pods, with a 300 second stabilization window (i.e., the highest recommendation for the last 300sec is used).
        /// </summary>
        [YamlMember(Alias = "scaleDown")]
        [JsonProperty("scaleDown", NullValueHandling = NullValueHandling.Ignore)]
        public HPAScalingRulesV2 ScaleDown { get; set; }

        /// <summary>
        ///     scaleUp is scaling policy for scaling Up. If not set, the default value is the higher of:
        ///       * increase no more than 4 pods per 60 seconds
        ///       * double the number of pods per 60 seconds
        ///     No stabilization is used.
        /// </summary>
        [YamlMember(Alias = "scaleUp")]
        [JsonProperty("scaleUp", NullValueHandling = NullValueHandling.Ignore)]
        public HPAScalingRulesV2 ScaleUp { get; set; }
    }
}
