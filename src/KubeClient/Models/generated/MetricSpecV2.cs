using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     MetricSpec specifies how to scale based on a single metric (only `type` and one other matching field should be set at once).
    /// </summary>
    public partial class MetricSpecV2
    {
        /// <summary>
        ///     containerResource refers to a resource metric (such as those specified in requests and limits) known to Kubernetes describing a single container in each pod of the current scale target (e.g. CPU or memory). Such metrics are built in to Kubernetes, and have special scaling options on top of those available to normal per-pod metrics using the "pods" source. This is an alpha feature and can be enabled by the HPAContainerMetrics feature flag.
        /// </summary>
        [YamlMember(Alias = "containerResource")]
        [JsonProperty("containerResource", NullValueHandling = NullValueHandling.Ignore)]
        public ContainerResourceMetricSourceV2 ContainerResource { get; set; }

        /// <summary>
        ///     resource refers to a resource metric (such as those specified in requests and limits) known to Kubernetes describing each pod in the current scale target (e.g. CPU or memory). Such metrics are built in to Kubernetes, and have special scaling options on top of those available to normal per-pod metrics using the "pods" source.
        /// </summary>
        [YamlMember(Alias = "resource")]
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceMetricSourceV2 Resource { get; set; }

        /// <summary>
        ///     type is the type of metric source.  It should be one of "ContainerResource", "External", "Object", "Pods" or "Resource", each mapping to a matching field in the object. Note: "ContainerResource" type is available on when the feature-gate HPAContainerMetrics is enabled
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     external refers to a global metric that is not associated with any Kubernetes object. It allows autoscaling based on information coming from components running outside of cluster (for example length of queue in cloud messaging service, or QPS from loadbalancer running outside of cluster).
        /// </summary>
        [YamlMember(Alias = "external")]
        [JsonProperty("external", NullValueHandling = NullValueHandling.Ignore)]
        public ExternalMetricSourceV2 External { get; set; }

        /// <summary>
        ///     pods refers to a metric describing each pod in the current scale target (for example, transactions-processed-per-second).  The values will be averaged together before being compared to the target value.
        /// </summary>
        [YamlMember(Alias = "pods")]
        [JsonProperty("pods", NullValueHandling = NullValueHandling.Ignore)]
        public PodsMetricSourceV2 Pods { get; set; }

        /// <summary>
        ///     object refers to a metric describing a single kubernetes object (for example, hits-per-second on an Ingress object).
        /// </summary>
        [YamlMember(Alias = "object")]
        [JsonProperty("object", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectMetricSourceV2 Object { get; set; }
    }
}
