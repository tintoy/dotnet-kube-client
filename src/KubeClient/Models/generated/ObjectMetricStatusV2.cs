using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ObjectMetricStatus indicates the current value of a metric describing a kubernetes object (for example, hits-per-second on an Ingress object).
    /// </summary>
    public partial class ObjectMetricStatusV2
    {
        /// <summary>
        ///     metric identifies the target metric by name and selector
        /// </summary>
        [YamlMember(Alias = "metric")]
        [JsonProperty("metric", NullValueHandling = NullValueHandling.Include)]
        public MetricIdentifierV2 Metric { get; set; }

        /// <summary>
        ///     current contains the current value for the given metric
        /// </summary>
        [YamlMember(Alias = "current")]
        [JsonProperty("current", NullValueHandling = NullValueHandling.Include)]
        public MetricValueStatusV2 Current { get; set; }

        /// <summary>
        ///     DescribedObject specifies the descriptions of a object,such as kind,name apiVersion
        /// </summary>
        [YamlMember(Alias = "describedObject")]
        [JsonProperty("describedObject", NullValueHandling = NullValueHandling.Include)]
        public CrossVersionObjectReferenceV2 DescribedObject { get; set; }
    }
}
