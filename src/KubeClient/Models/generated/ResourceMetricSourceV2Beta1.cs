using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceMetricSource indicates how to scale on a resource metric known to Kubernetes, as specified in requests and limits, describing each pod in the current scale target (e.g. CPU or memory).  The values will be averaged together before being compared to the target.  Such metrics are built in to Kubernetes, and have special scaling options on top of those available to normal per-pod metrics using the "pods" source.  Only one "target" type should be set.
    /// </summary>
    public partial class ResourceMetricSourceV2Beta1
    {
        /// <summary>
        ///     name is the name of the resource in question.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     targetAverageValue is the target value of the average of the resource metric across all relevant pods, as a raw value (instead of as a percentage of the request), similar to the "pods" metric source type.
        /// </summary>
        [YamlMember(Alias = "targetAverageValue")]
        [JsonProperty("targetAverageValue", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetAverageValue { get; set; }

        /// <summary>
        ///     targetAverageUtilization is the target value of the average of the resource metric across all relevant pods, represented as a percentage of the requested value of the resource for the pods.
        /// </summary>
        [YamlMember(Alias = "targetAverageUtilization")]
        [JsonProperty("targetAverageUtilization", NullValueHandling = NullValueHandling.Ignore)]
        public int? TargetAverageUtilization { get; set; }
    }
}
