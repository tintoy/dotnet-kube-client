using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     MetricStatus describes the last-read state of a single metric.
    /// </summary>
    [KubeObject("MetricStatus", "v2beta1")]
    public partial class MetricStatusV2Beta1
    {
        /// <summary>
        ///     resource refers to a resource metric (such as those specified in requests and limits) known to Kubernetes describing each pod in the current scale target (e.g. CPU or memory). Such metrics are built in to Kubernetes, and have special scaling options on top of those available to normal per-pod metrics using the "pods" source.
        /// </summary>
        [JsonProperty("resource")]
        public ResourceMetricStatusV2Beta1 Resource { get; set; }

        /// <summary>
        ///     type is the type of metric source.  It will match one of the fields below.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     pods refers to a metric describing each pod in the current scale target (for example, transactions-processed-per-second).  The values will be averaged together before being compared to the target value.
        /// </summary>
        [JsonProperty("pods")]
        public PodsMetricStatusV2Beta1 Pods { get; set; }

        /// <summary>
        ///     object refers to a metric describing a single kubernetes object (for example, hits-per-second on an Ingress object).
        /// </summary>
        [JsonProperty("object")]
        public ObjectMetricStatusV2Beta1 Object { get; set; }
    }
}
