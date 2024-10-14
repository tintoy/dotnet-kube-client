using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     MetricTarget defines the target value, average value, or average utilization of a specific metric
    /// </summary>
    public partial class MetricTargetV2
    {
        /// <summary>
        ///     averageValue is the target value of the average of the metric across all relevant pods (as a quantity)
        /// </summary>
        [YamlMember(Alias = "averageValue")]
        [JsonProperty("averageValue", NullValueHandling = NullValueHandling.Ignore)]
        public string AverageValue { get; set; }

        /// <summary>
        ///     type represents whether the metric type is Utilization, Value, or AverageValue
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     value is the target value of the metric (as a quantity).
        /// </summary>
        [YamlMember(Alias = "value")]
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        ///     averageUtilization is the target value of the average of the resource metric across all relevant pods, represented as a percentage of the requested value of the resource for the pods. Currently only valid for Resource metric source type
        /// </summary>
        [YamlMember(Alias = "averageUtilization")]
        [JsonProperty("averageUtilization", NullValueHandling = NullValueHandling.Ignore)]
        public int? AverageUtilization { get; set; }
    }
}
