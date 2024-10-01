using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ExternalMetricStatus indicates the current value of a global metric not associated with any Kubernetes object.
    /// </summary>
    public partial class ExternalMetricStatusV2
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
    }
}
