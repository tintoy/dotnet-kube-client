using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ExternalMetricStatus indicates the current value of a global metric not associated with any Kubernetes object.
    /// </summary>
    public partial class ExternalMetricStatusV2Beta1
    {
        /// <summary>
        ///     metricName is the name of a metric used for autoscaling in metric system.
        /// </summary>
        [JsonProperty("metricName")]
        [YamlMember(Alias = "metricName")]
        public string MetricName { get; set; }

        /// <summary>
        ///     metricSelector is used to identify a specific time series within a given metric.
        /// </summary>
        [JsonProperty("metricSelector")]
        [YamlMember(Alias = "metricSelector")]
        public LabelSelectorV1 MetricSelector { get; set; }

        /// <summary>
        ///     currentAverageValue is the current value of metric averaged over autoscaled pods.
        /// </summary>
        [JsonProperty("currentAverageValue")]
        [YamlMember(Alias = "currentAverageValue")]
        public string CurrentAverageValue { get; set; }

        /// <summary>
        ///     currentValue is the current value of the metric (as a quantity)
        /// </summary>
        [JsonProperty("currentValue")]
        [YamlMember(Alias = "currentValue")]
        public string CurrentValue { get; set; }
    }
}
