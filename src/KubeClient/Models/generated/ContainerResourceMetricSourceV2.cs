using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerResourceMetricSource indicates how to scale on a resource metric known to Kubernetes, as specified in requests and limits, describing each pod in the current scale target (e.g. CPU or memory).  The values will be averaged together before being compared to the target.  Such metrics are built in to Kubernetes, and have special scaling options on top of those available to normal per-pod metrics using the "pods" source.  Only one "target" type should be set.
    /// </summary>
    public partial class ContainerResourceMetricSourceV2
    {
        /// <summary>
        ///     name is the name of the resource in question.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     container is the name of the container in the pods of the scaling target
        /// </summary>
        [YamlMember(Alias = "container")]
        [JsonProperty("container", NullValueHandling = NullValueHandling.Include)]
        public string Container { get; set; }

        /// <summary>
        ///     target specifies the target value for the given metric
        /// </summary>
        [YamlMember(Alias = "target")]
        [JsonProperty("target", NullValueHandling = NullValueHandling.Include)]
        public MetricTargetV2 Target { get; set; }
    }
}
