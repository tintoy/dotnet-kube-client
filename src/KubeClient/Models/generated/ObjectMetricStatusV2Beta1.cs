using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ObjectMetricStatus indicates the current value of a metric describing a kubernetes object (for example, hits-per-second on an Ingress object).
    /// </summary>
    public partial class ObjectMetricStatusV2Beta1
    {
        /// <summary>
        ///     metricName is the name of the metric in question.
        /// </summary>
        [JsonProperty("metricName")]
        [YamlMember(Alias = "metricName")]
        public string MetricName { get; set; }

        /// <summary>
        ///     target is the described Kubernetes object.
        /// </summary>
        [JsonProperty("target")]
        [YamlMember(Alias = "target")]
        public CrossVersionObjectReferenceV2Beta1 Target { get; set; }

        /// <summary>
        ///     currentValue is the current value of the metric (as a quantity).
        /// </summary>
        [JsonProperty("currentValue")]
        [YamlMember(Alias = "currentValue")]
        public string CurrentValue { get; set; }
    }
}
