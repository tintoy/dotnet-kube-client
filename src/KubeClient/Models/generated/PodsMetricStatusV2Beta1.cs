using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodsMetricStatus indicates the current value of a metric describing each pod in the current scale target (for example, transactions-processed-per-second).
    /// </summary>
    public partial class PodsMetricStatusV2Beta1
    {
        /// <summary>
        ///     currentAverageValue is the current value of the average of the metric across all relevant pods (as a quantity)
        /// </summary>
        [YamlMember(Alias = "currentAverageValue")]
        [JsonProperty("currentAverageValue", NullValueHandling = NullValueHandling.Include)]
        public string CurrentAverageValue { get; set; }

        /// <summary>
        ///     metricName is the name of the metric in question
        /// </summary>
        [YamlMember(Alias = "metricName")]
        [JsonProperty("metricName", NullValueHandling = NullValueHandling.Include)]
        public string MetricName { get; set; }
    }
}
