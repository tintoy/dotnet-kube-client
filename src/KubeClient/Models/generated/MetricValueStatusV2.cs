using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     MetricValueStatus holds the current value for a metric
    /// </summary>
    public partial class MetricValueStatusV2
    {
        /// <summary>
        ///     averageValue is the current value of the average of the metric across all relevant pods (as a quantity)
        /// </summary>
        [YamlMember(Alias = "averageValue")]
        [JsonProperty("averageValue", NullValueHandling = NullValueHandling.Ignore)]
        public string AverageValue { get; set; }

        /// <summary>
        ///     value is the current value of the metric (as a quantity).
        /// </summary>
        [YamlMember(Alias = "value")]
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        ///     currentAverageUtilization is the current value of the average of the resource metric across all relevant pods, represented as a percentage of the requested value of the resource for the pods.
        /// </summary>
        [YamlMember(Alias = "averageUtilization")]
        [JsonProperty("averageUtilization", NullValueHandling = NullValueHandling.Ignore)]
        public int? AverageUtilization { get; set; }
    }
}
