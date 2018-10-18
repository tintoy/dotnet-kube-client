using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ExternalMetricSource indicates how to scale on a metric not associated with any Kubernetes object (for example length of queue in cloud messaging service, or QPS from loadbalancer running outside of cluster). Exactly one "target" type should be set.
    /// </summary>
    public partial class ExternalMetricSourceV2Beta1
    {
        /// <summary>
        ///     metricName is the name of the metric in question.
        /// </summary>
        [YamlMember(Alias = "metricName")]
        [JsonProperty("metricName", NullValueHandling = NullValueHandling.Include)]
        public string MetricName { get; set; }

        /// <summary>
        ///     targetAverageValue is the target per-pod value of global metric (as a quantity). Mutually exclusive with TargetValue.
        /// </summary>
        [YamlMember(Alias = "targetAverageValue")]
        [JsonProperty("targetAverageValue", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetAverageValue { get; set; }

        /// <summary>
        ///     targetValue is the target value of the metric (as a quantity). Mutually exclusive with TargetAverageValue.
        /// </summary>
        [YamlMember(Alias = "targetValue")]
        [JsonProperty("targetValue", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetValue { get; set; }

        /// <summary>
        ///     metricSelector is used to identify a specific time series within a given metric.
        /// </summary>
        [YamlMember(Alias = "metricSelector")]
        [JsonProperty("metricSelector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 MetricSelector { get; set; }
    }
}
