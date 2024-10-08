using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HorizontalPodAutoscalerStatus describes the current status of a horizontal pod autoscaler.
    /// </summary>
    public partial class HorizontalPodAutoscalerStatusV2
    {
        /// <summary>
        ///     lastScaleTime is the last time the HorizontalPodAutoscaler scaled the number of pods, used by the autoscaler to control how often the number of pods is changed.
        /// </summary>
        [YamlMember(Alias = "lastScaleTime")]
        [JsonProperty("lastScaleTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastScaleTime { get; set; }

        /// <summary>
        ///     observedGeneration is the most recent generation observed by this autoscaler.
        /// </summary>
        [YamlMember(Alias = "observedGeneration")]
        [JsonProperty("observedGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObservedGeneration { get; set; }

        /// <summary>
        ///     conditions is the set of conditions required for this autoscaler to scale its target, and indicates whether or not those conditions are met.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<HorizontalPodAutoscalerConditionV2> Conditions { get; } = new List<HorizontalPodAutoscalerConditionV2>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;

        /// <summary>
        ///     currentMetrics is the last read state of the metrics used by this autoscaler.
        /// </summary>
        [YamlMember(Alias = "currentMetrics")]
        [JsonProperty("currentMetrics", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<MetricStatusV2> CurrentMetrics { get; } = new List<MetricStatusV2>();

        /// <summary>
        ///     Determine whether the <see cref="CurrentMetrics"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeCurrentMetrics() => CurrentMetrics.Count > 0;

        /// <summary>
        ///     currentReplicas is current number of replicas of pods managed by this autoscaler, as last seen by the autoscaler.
        /// </summary>
        [YamlMember(Alias = "currentReplicas")]
        [JsonProperty("currentReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentReplicas { get; set; }

        /// <summary>
        ///     desiredReplicas is the desired number of replicas of pods managed by this autoscaler, as last calculated by the autoscaler.
        /// </summary>
        [YamlMember(Alias = "desiredReplicas")]
        [JsonProperty("desiredReplicas", NullValueHandling = NullValueHandling.Include)]
        public int DesiredReplicas { get; set; }
    }
}
