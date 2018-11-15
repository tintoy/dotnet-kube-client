using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceMetricStatus indicates the current value of a resource metric known to Kubernetes, as specified in requests and limits, describing each pod in the current scale target (e.g. CPU or memory).  Such metrics are built in to Kubernetes, and have special scaling options on top of those available to normal per-pod metrics using the "pods" source.
    /// </summary>
    public partial class ResourceMetricStatusV2Beta1
    {
        /// <summary>
        ///     currentAverageValue is the current value of the average of the resource metric across all relevant pods, as a raw value (instead of as a percentage of the request), similar to the "pods" metric source type. It will always be set, regardless of the corresponding metric specification.
        /// </summary>
        [YamlMember(Alias = "currentAverageValue")]
        [JsonProperty("currentAverageValue", NullValueHandling = NullValueHandling.Include)]
        public string CurrentAverageValue { get; set; }

        /// <summary>
        ///     name is the name of the resource in question.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     currentAverageUtilization is the current value of the average of the resource metric across all relevant pods, represented as a percentage of the requested value of the resource for the pods.  It will only be present if `targetAverageValue` was set in the corresponding metric specification.
        /// </summary>
        [YamlMember(Alias = "currentAverageUtilization")]
        [JsonProperty("currentAverageUtilization", NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentAverageUtilization { get; set; }
    }
}
