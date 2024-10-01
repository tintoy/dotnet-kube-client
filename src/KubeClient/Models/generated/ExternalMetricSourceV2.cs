using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ExternalMetricSource indicates how to scale on a metric not associated with any Kubernetes object (for example length of queue in cloud messaging service, or QPS from loadbalancer running outside of cluster).
    /// </summary>
    public partial class ExternalMetricSourceV2
    {
        /// <summary>
        ///     metric identifies the target metric by name and selector
        /// </summary>
        [YamlMember(Alias = "metric")]
        [JsonProperty("metric", NullValueHandling = NullValueHandling.Include)]
        public MetricIdentifierV2 Metric { get; set; }

        /// <summary>
        ///     target specifies the target value for the given metric
        /// </summary>
        [YamlMember(Alias = "target")]
        [JsonProperty("target", NullValueHandling = NullValueHandling.Include)]
        public MetricTargetV2 Target { get; set; }
    }
}
