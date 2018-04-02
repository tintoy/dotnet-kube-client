using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     current status of a horizontal pod autoscaler
    /// </summary>
    [KubeObject("HorizontalPodAutoscalerStatus", "v1")]
    public partial class HorizontalPodAutoscalerStatusV1
    {
        /// <summary>
        ///     current average CPU utilization over all pods, represented as a percentage of requested CPU, e.g. 70 means that an average pod is using now 70% of its requested CPU.
        /// </summary>
        [JsonProperty("currentCPUUtilizationPercentage")]
        public int CurrentCPUUtilizationPercentage { get; set; }

        /// <summary>
        ///     last time the HorizontalPodAutoscaler scaled the number of pods; used by the autoscaler to control how often the number of pods is changed.
        /// </summary>
        [JsonProperty("lastScaleTime")]
        public DateTime? LastScaleTime { get; set; }

        /// <summary>
        ///     most recent generation observed by this autoscaler.
        /// </summary>
        [JsonProperty("observedGeneration")]
        public int ObservedGeneration { get; set; }

        /// <summary>
        ///     current number of replicas of pods managed by this autoscaler.
        /// </summary>
        [JsonProperty("currentReplicas")]
        public int CurrentReplicas { get; set; }

        /// <summary>
        ///     desired number of replicas of pods managed by this autoscaler.
        /// </summary>
        [JsonProperty("desiredReplicas")]
        public int DesiredReplicas { get; set; }
    }
}
