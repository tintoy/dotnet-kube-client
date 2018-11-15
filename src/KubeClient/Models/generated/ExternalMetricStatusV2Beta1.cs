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
        ///     currentAverageValue is the current value of metric averaged over autoscaled pods.
        /// </summary>
        [YamlMember(Alias = "currentAverageValue")]
        [JsonProperty("currentAverageValue", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrentAverageValue { get; set; }

        /// <summary>
        ///     currentValue is the current value of the metric (as a quantity)
        /// </summary>
        [YamlMember(Alias = "currentValue")]
        [JsonProperty("currentValue", NullValueHandling = NullValueHandling.Include)]
        public string CurrentValue { get; set; }

        /// <summary>
        ///     metricName is the name of a metric used for autoscaling in metric system.
        /// </summary>
        [YamlMember(Alias = "metricName")]
        [JsonProperty("metricName", NullValueHandling = NullValueHandling.Include)]
        public string MetricName { get; set; }

        /// <summary>
        ///     metricSelector is used to identify a specific time series within a given metric.
        /// </summary>
        [YamlMember(Alias = "metricSelector")]
        [JsonProperty("metricSelector", NullValueHandling = NullValueHandling.Ignore)]
        public LabelSelectorV1 MetricSelector { get; set; }
    }
}
