using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     MetricStatus describes the last-read state of a single metric.
    /// </summary>
    public partial class MetricStatusV2Beta1
    {
        /// <summary>
        ///     object refers to a metric describing a single kubernetes object (for example, hits-per-second on an Ingress object).
        /// </summary>
        [JsonProperty("object")]
        [YamlMember(Alias = "object")]
        public ObjectMetricStatusV2Beta1 Object { get; set; }

        /// <summary>
        ///     type is the type of metric source.  It will be one of "Object", "Pods" or "Resource", each corresponds to a matching field in the object.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     pods refers to a metric describing each pod in the current scale target (for example, transactions-processed-per-second).  The values will be averaged together before being compared to the target value.
        /// </summary>
        [JsonProperty("pods")]
        [YamlMember(Alias = "pods")]
        public PodsMetricStatusV2Beta1 Pods { get; set; }

        /// <summary>
        ///     resource refers to a resource metric (such as those specified in requests and limits) known to Kubernetes describing each pod in the current scale target (e.g. CPU or memory). Such metrics are built in to Kubernetes, and have special scaling options on top of those available to normal per-pod metrics using the "pods" source.
        /// </summary>
        [JsonProperty("resource")]
        [YamlMember(Alias = "resource")]
        public ResourceMetricStatusV2Beta1 Resource { get; set; }

        /// <summary>
        ///     external refers to a global metric that is not associated with any Kubernetes object. It allows autoscaling based on information coming from components running outside of cluster (for example length of queue in cloud messaging service, or QPS from loadbalancer running outside of cluster).
        /// </summary>
        [JsonProperty("external")]
        [YamlMember(Alias = "external")]
        public ExternalMetricStatusV2Beta1 External { get; set; }
    }
}
