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
        ///     target average CPU utilization (represented as a percentage of requested CPU) over all the pods; if not specified the default autoscaling policy will be used.
        /// </summary>
        [YamlMember(Alias = "targetCPUUtilizationPercentage")]
        [JsonProperty("targetCPUUtilizationPercentage", NullValueHandling = NullValueHandling.Ignore)]
        public int? TargetCPUUtilizationPercentage { get; set; }

        /// <summary>
        ///     reference to scaled resource; horizontal pod autoscaler will learn the current resource consumption and will set the desired number of pods by using its Scale subresource.
        /// </summary>
        [YamlMember(Alias = "scaleTargetRef")]
        [JsonProperty("scaleTargetRef", NullValueHandling = NullValueHandling.Include)]
        public CrossVersionObjectReferenceV1 ScaleTargetRef { get; set; }

        /// <summary>
        ///     upper limit for the number of pods that can be set by the autoscaler; cannot be smaller than MinReplicas.
        /// </summary>
        [YamlMember(Alias = "maxReplicas")]
        [JsonProperty("maxReplicas", NullValueHandling = NullValueHandling.Include)]
        public int MaxReplicas { get; set; }

        /// <summary>
        ///     lower limit for the number of pods that can be set by the autoscaler, default 1.
        /// </summary>
        [YamlMember(Alias = "minReplicas")]
        [JsonProperty("minReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinReplicas { get; set; }
    }
}
