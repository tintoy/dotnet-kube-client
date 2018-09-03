using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     specification of a horizontal pod autoscaler.
    /// </summary>
    public partial class HorizontalPodAutoscalerSpecV1
    {
        /// <summary>
        ///     reference to scaled resource; horizontal pod autoscaler will learn the current resource consumption and will set the desired number of pods by using its Scale subresource.
        /// </summary>
        [JsonProperty("scaleTargetRef")]
        [YamlMember(Alias = "scaleTargetRef")]
        public CrossVersionObjectReferenceV1 ScaleTargetRef { get; set; }

        /// <summary>
        ///     lower limit for the number of pods that can be set by the autoscaler, default 1.
        /// </summary>
        [JsonProperty("minReplicas")]
        [YamlMember(Alias = "minReplicas")]
        public int MinReplicas { get; set; }

        /// <summary>
        ///     target average CPU utilization (represented as a percentage of requested CPU) over all the pods; if not specified the default autoscaling policy will be used.
        /// </summary>
        [JsonProperty("targetCPUUtilizationPercentage")]
        [YamlMember(Alias = "targetCPUUtilizationPercentage")]
        public int? TargetCPUUtilizationPercentage { get; set; }

        /// <summary>
        ///     upper limit for the number of pods that can be set by the autoscaler; cannot be smaller than MinReplicas.
        /// </summary>
        [JsonProperty("maxReplicas")]
        [YamlMember(Alias = "maxReplicas")]
        public int MaxReplicas { get; set; }
    }
}
