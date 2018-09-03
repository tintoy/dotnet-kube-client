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
        [JsonProperty("scaleTargetRef")]
        [YamlMember(Alias = "scaleTargetRef")]
        public CrossVersionObjectReferenceV2Beta1 ScaleTargetRef { get; set; }

        /// <summary>
        ///     maxReplicas is the upper limit for the number of replicas to which the autoscaler can scale up. It cannot be less that minReplicas.
        /// </summary>
        [JsonProperty("maxReplicas")]
        [YamlMember(Alias = "maxReplicas")]
        public int MaxReplicas { get; set; }

        /// <summary>
        ///     metrics contains the specifications for which to use to calculate the desired replica count (the maximum replica count across all metrics will be used).  The desired replica count is calculated multiplying the ratio between the target value and the current value by the current number of pods.  Ergo, metrics used must decrease as the pod count is increased, and vice-versa.  See the individual metric source types for more information about how each type of metric must respond.
        /// </summary>
        [YamlMember(Alias = "metrics")]
        [JsonProperty("metrics", NullValueHandling = NullValueHandling.Ignore)]
        public List<MetricSpecV2Beta1> Metrics { get; set; } = new List<MetricSpecV2Beta1>();

        /// <summary>
        ///     minReplicas is the lower limit for the number of replicas to which the autoscaler can scale down. It defaults to 1 pod.
        /// </summary>
        [JsonProperty("minReplicas")]
        [YamlMember(Alias = "minReplicas")]
        public int MinReplicas { get; set; }
    }
}
