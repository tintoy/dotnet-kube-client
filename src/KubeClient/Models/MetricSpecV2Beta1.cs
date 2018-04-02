using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     MetricSpec specifies how to scale based on a single metric (only `type` and one other matching field should be set at once).
    /// </summary>
    [KubeObject("MetricSpec", "v2beta1")]
    public partial class MetricSpecV2Beta1
    {
        /// <summary>
        ///     resource refers to a resource metric (such as those specified in requests and limits) known to Kubernetes describing each pod in the current scale target (e.g. CPU or memory). Such metrics are built in to Kubernetes, and have special scaling options on top of those available to normal per-pod metrics using the "pods" source.
        /// </summary>
        [JsonProperty("resource")]
        public ResourceMetricSourceV2Beta1 Resource { get; set; }

        /// <summary>
        ///     type is the type of metric source.  It should match one of the fields below.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     pods refers to a metric describing each pod in the current scale target (for example, transactions-processed-per-second).  The values will be averaged together before being compared to the target value.
        /// </summary>
        [JsonProperty("pods")]
        public PodsMetricSourceV2Beta1 Pods { get; set; }

        /// <summary>
        ///     object refers to a metric describing a single kubernetes object (for example, hits-per-second on an Ingress object).
        /// </summary>
        [JsonProperty("object")]
        public ObjectMetricSourceV2Beta1 Object { get; set; }
    }
}
