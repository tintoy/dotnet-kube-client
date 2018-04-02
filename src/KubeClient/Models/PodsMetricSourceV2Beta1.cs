using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodsMetricSource indicates how to scale on a metric describing each pod in the current scale target (for example, transactions-processed-per-second). The values will be averaged together before being compared to the target value.
    /// </summary>
    [KubeObject("PodsMetricSource", "v2beta1")]
    public partial class PodsMetricSourceV2Beta1
    {
        /// <summary>
        ///     metricName is the name of the metric in question
        /// </summary>
        [JsonProperty("metricName")]
        public string MetricName { get; set; }

        /// <summary>
        ///     targetAverageValue is the target value of the average of the metric across all relevant pods (as a quantity)
        /// </summary>
        [JsonProperty("targetAverageValue")]
        public string TargetAverageValue { get; set; }
    }
}
