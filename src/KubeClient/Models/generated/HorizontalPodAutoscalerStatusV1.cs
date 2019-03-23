using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     current status of a horizontal pod autoscaler
    /// </summary>
    public partial class HorizontalPodAutoscalerStatusV1
    {
        /// <summary>
        ///     current average CPU utilization over all pods, represented as a percentage of requested CPU, e.g. 70 means that an average pod is using now 70% of its requested CPU.
        /// </summary>
        [YamlMember(Alias = "currentCPUUtilizationPercentage")]
        [JsonProperty("currentCPUUtilizationPercentage", NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentCPUUtilizationPercentage { get; set; }

        /// <summary>
        ///     last time the HorizontalPodAutoscaler scaled the number of pods; used by the autoscaler to control how often the number of pods is changed.
        /// </summary>
        [YamlMember(Alias = "lastScaleTime")]
        [JsonProperty("lastScaleTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastScaleTime { get; set; }

        /// <summary>
        ///     most recent generation observed by this autoscaler.
        /// </summary>
        [YamlMember(Alias = "observedGeneration")]
        [JsonProperty("observedGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObservedGeneration { get; set; }

        /// <summary>
        ///     current number of replicas of pods managed by this autoscaler.
        /// </summary>
        [YamlMember(Alias = "currentReplicas")]
        [JsonProperty("currentReplicas", NullValueHandling = NullValueHandling.Include)]
        public int CurrentReplicas { get; set; }

        /// <summary>
        ///     desired number of replicas of pods managed by this autoscaler.
        /// </summary>
        [YamlMember(Alias = "desiredReplicas")]
        [JsonProperty("desiredReplicas", NullValueHandling = NullValueHandling.Include)]
        public int DesiredReplicas { get; set; }
    }
}
