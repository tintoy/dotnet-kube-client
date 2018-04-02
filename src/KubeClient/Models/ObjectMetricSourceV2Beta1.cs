using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ObjectMetricSource indicates how to scale on a metric describing a kubernetes object (for example, hits-per-second on an Ingress object).
    /// </summary>
    [KubeObject("ObjectMetricSource", "v2beta1")]
    public partial class ObjectMetricSourceV2Beta1
    {
        /// <summary>
        ///     metricName is the name of the metric in question.
        /// </summary>
        [JsonProperty("metricName")]
        public string MetricName { get; set; }

        /// <summary>
        ///     targetValue is the target value of the metric (as a quantity).
        /// </summary>
        [JsonProperty("targetValue")]
        public string TargetValue { get; set; }

        /// <summary>
        ///     target is the described Kubernetes object.
        /// </summary>
        [JsonProperty("target")]
        public CrossVersionObjectReferenceV2Beta1 Target { get; set; }
    }
}
