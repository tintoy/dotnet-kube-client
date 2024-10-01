using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ObjectMetricSource indicates how to scale on a metric describing a kubernetes object (for example, hits-per-second on an Ingress object).
    /// </summary>
    public partial class ObjectMetricSourceV2
    {
        /// <summary>
        ///     metric identifies the target metric by name and selector
        /// </summary>
        [YamlMember(Alias = "metric")]
        [JsonProperty("metric", NullValueHandling = NullValueHandling.Include)]
        public MetricIdentifierV2 Metric { get; set; }

        /// <summary>
        ///     describedObject specifies the descriptions of a object,such as kind,name apiVersion
        /// </summary>
        [YamlMember(Alias = "describedObject")]
        [JsonProperty("describedObject", NullValueHandling = NullValueHandling.Include)]
        public CrossVersionObjectReferenceV2 DescribedObject { get; set; }

        /// <summary>
        ///     target specifies the target value for the given metric
        /// </summary>
        [YamlMember(Alias = "target")]
        [JsonProperty("target", NullValueHandling = NullValueHandling.Include)]
        public MetricTargetV2 Target { get; set; }
    }
}
