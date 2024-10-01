using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodsMetricStatus indicates the current value of a metric describing each pod in the current scale target (for example, transactions-processed-per-second).
    /// </summary>
    public partial class PodsMetricStatusV2
    {
        /// <summary>
        ///     metric identifies the target metric by name and selector
        /// </summary>
        [YamlMember(Alias = "metric")]
        [JsonProperty("metric", NullValueHandling = NullValueHandling.Include)]
        public MetricIdentifierV2 Metric { get; set; }

        /// <summary>
        ///     current contains the current value for the given metric
        /// </summary>
        [YamlMember(Alias = "current")]
        [JsonProperty("current", NullValueHandling = NullValueHandling.Include)]
        public MetricValueStatusV2 Current { get; set; }
    }
}
