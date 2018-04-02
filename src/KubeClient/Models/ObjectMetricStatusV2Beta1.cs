using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ObjectMetricStatus indicates the current value of a metric describing a kubernetes object (for example, hits-per-second on an Ingress object).
    /// </summary>
    [KubeObject("ObjectMetricStatus", "v2beta1")]
    public partial class ObjectMetricStatusV2Beta1
    {
        /// <summary>
        ///     currentValue is the current value of the metric (as a quantity).
        /// </summary>
        [JsonProperty("currentValue")]
        public string CurrentValue { get; set; }

        /// <summary>
        ///     metricName is the name of the metric in question.
        /// </summary>
        [JsonProperty("metricName")]
        public string MetricName { get; set; }

        /// <summary>
        ///     target is the described Kubernetes object.
        /// </summary>
        [JsonProperty("target")]
        public CrossVersionObjectReferenceV2Beta1 Target { get; set; }
    }
}
