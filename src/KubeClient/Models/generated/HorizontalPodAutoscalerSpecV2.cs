using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HorizontalPodAutoscalerSpec describes the desired functionality of the HorizontalPodAutoscaler.
    /// </summary>
    public partial class HorizontalPodAutoscalerSpecV2
    {
        /// <summary>
        ///     scaleTargetRef points to the target resource to scale, and is used to the pods for which metrics should be collected, as well as to actually change the replica count.
        /// </summary>
        [YamlMember(Alias = "scaleTargetRef")]
        [JsonProperty("scaleTargetRef", NullValueHandling = NullValueHandling.Include)]
        public CrossVersionObjectReferenceV2 ScaleTargetRef { get; set; }

        /// <summary>
        ///     behavior configures the scaling behavior of the target in both Up and Down directions (scaleUp and scaleDown fields respectively). If not set, the default HPAScalingRules for scale up and scale down are used.
        /// </summary>
        [YamlMember(Alias = "behavior")]
        [JsonProperty("behavior", NullValueHandling = NullValueHandling.Ignore)]
        public HorizontalPodAutoscalerBehaviorV2 Behavior { get; set; }

        /// <summary>
        ///     maxReplicas is the upper limit for the number of replicas to which the autoscaler can scale up. It cannot be less that minReplicas.
        /// </summary>
        [YamlMember(Alias = "maxReplicas")]
        [JsonProperty("maxReplicas", NullValueHandling = NullValueHandling.Include)]
        public int MaxReplicas { get; set; }

        /// <summary>
        ///     metrics contains the specifications for which to use to calculate the desired replica count (the maximum replica count across all metrics will be used).  The desired replica count is calculated multiplying the ratio between the target value and the current value by the current number of pods.  Ergo, metrics used must decrease as the pod count is increased, and vice-versa.  See the individual metric source types for more information about how each type of metric must respond. If not set, the default metric will be set to 80% average CPU utilization.
        /// </summary>
        [YamlMember(Alias = "metrics")]
        [JsonProperty("metrics", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<MetricSpecV2> Metrics { get; } = new List<MetricSpecV2>();

        /// <summary>
        ///     Determine whether the <see cref="Metrics"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMetrics() => Metrics.Count > 0;

        /// <summary>
        ///     minReplicas is the lower limit for the number of replicas to which the autoscaler can scale down.  It defaults to 1 pod.  minReplicas is allowed to be 0 if the alpha feature gate HPAScaleToZero is enabled and at least one Object or External metric is configured.  Scaling is active as long as at least one metric value is available.
        /// </summary>
        [YamlMember(Alias = "minReplicas")]
        [JsonProperty("minReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinReplicas { get; set; }
    }
}
