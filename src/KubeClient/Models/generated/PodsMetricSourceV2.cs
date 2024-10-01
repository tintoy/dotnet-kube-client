using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodsMetricSource indicates how to scale on a metric describing each pod in the current scale target (for example, transactions-processed-per-second). The values will be averaged together before being compared to the target value.
    /// </summary>
    public partial class PodsMetricSourceV2
    {
        /// <summary>
        ///     metric identifies the target metric by name and selector
        /// </summary>
        [YamlMember(Alias = "metric")]
        [JsonProperty("metric", NullValueHandling = NullValueHandling.Include)]
        public MetricIdentifierV2 Metric { get; set; }

        /// <summary>
        ///     target specifies the target value for the given metric
        /// </summary>
        [YamlMember(Alias = "target")]
        [JsonProperty("target", NullValueHandling = NullValueHandling.Include)]
        public MetricTargetV2 Target { get; set; }
    }
}
