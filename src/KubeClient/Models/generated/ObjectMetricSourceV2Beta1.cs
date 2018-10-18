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
        ///     metricName is the name of the metric in question.
        /// </summary>
        [YamlMember(Alias = "metricName")]
        [JsonProperty("metricName", NullValueHandling = NullValueHandling.Include)]
        public string MetricName { get; set; }

        /// <summary>
        ///     targetValue is the target value of the metric (as a quantity).
        /// </summary>
        [YamlMember(Alias = "targetValue")]
        [JsonProperty("targetValue", NullValueHandling = NullValueHandling.Include)]
        public string TargetValue { get; set; }

        /// <summary>
        ///     target is the described Kubernetes object.
        /// </summary>
        [YamlMember(Alias = "target")]
        [JsonProperty("target", NullValueHandling = NullValueHandling.Include)]
        public CrossVersionObjectReferenceV2Beta1 Target { get; set; }
    }
}
