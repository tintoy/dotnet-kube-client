using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ObjectMetricSource indicates how to scale on a metric describing a kubernetes object (for example, hits-per-second on an Ingress object).
    /// </summary>
    public partial class ObjectMetricSourceV2Beta1
    {
        /// <summary>
        ///     target is the described Kubernetes object.
        /// </summary>
        [JsonProperty("target")]
        [YamlMember(Alias = "target")]
        public CrossVersionObjectReferenceV2Beta1 Target { get; set; }

        /// <summary>
        ///     targetValue is the target value of the metric (as a quantity).
        /// </summary>
        [JsonProperty("targetValue")]
        [YamlMember(Alias = "targetValue")]
        public string TargetValue { get; set; }

        /// <summary>
        ///     metricName is the name of the metric in question.
        /// </summary>
        [JsonProperty("metricName")]
        [YamlMember(Alias = "metricName")]
        public string MetricName { get; set; }
    }
}
