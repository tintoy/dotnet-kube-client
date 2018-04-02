using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodsMetricStatus indicates the current value of a metric describing each pod in the current scale target (for example, transactions-processed-per-second).
    /// </summary>
    [KubeObject("PodsMetricStatus", "v2beta1")]
    public partial class PodsMetricStatusV2Beta1
    {
        /// <summary>
        ///     currentAverageValue is the current value of the average of the metric across all relevant pods (as a quantity)
        /// </summary>
        [JsonProperty("currentAverageValue")]
        public string CurrentAverageValue { get; set; }

        /// <summary>
        ///     metricName is the name of the metric in question
        /// </summary>
        [JsonProperty("metricName")]
        public string MetricName { get; set; }
    }
}
