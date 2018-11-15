using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HorizontalPodAutoscalerSpec describes the desired functionality of the HorizontalPodAutoscaler.
    /// </summary>
    public partial class HorizontalPodAutoscalerSpecV2Beta1
    {
        /// <summary>
        ///     scaleTargetRef points to the target resource to scale, and is used to the pods for which metrics should be collected, as well as to actually change the replica count.
        /// </summary>
        [YamlMember(Alias = "scaleTargetRef")]
        [JsonProperty("scaleTargetRef", NullValueHandling = NullValueHandling.Include)]
        public CrossVersionObjectReferenceV2Beta1 ScaleTargetRef { get; set; }

        /// <summary>
        ///     maxReplicas is the upper limit for the number of replicas to which the autoscaler can scale up. It cannot be less that minReplicas.
        /// </summary>
        [YamlMember(Alias = "maxReplicas")]
        [JsonProperty("maxReplicas", NullValueHandling = NullValueHandling.Include)]
        public int MaxReplicas { get; set; }

        /// <summary>
        ///     metrics contains the specifications for which to use to calculate the desired replica count (the maximum replica count across all metrics will be used).  The desired replica count is calculated multiplying the ratio between the target value and the current value by the current number of pods.  Ergo, metrics used must decrease as the pod count is increased, and vice-versa.  See the individual metric source types for more information about how each type of metric must respond.
        /// </summary>
        [YamlMember(Alias = "metrics")]
        [JsonProperty("metrics", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<MetricSpecV2Beta1> Metrics { get; } = new List<MetricSpecV2Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Metrics"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeMetrics() => Metrics.Count > 0;

        /// <summary>
        ///     minReplicas is the lower limit for the number of replicas to which the autoscaler can scale down. It defaults to 1 pod.
        /// </summary>
        [YamlMember(Alias = "minReplicas")]
        [JsonProperty("minReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinReplicas { get; set; }
    }
}
