using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ContainerResourceMetricStatus indicates the current value of a resource metric known to Kubernetes, as specified in requests and limits, describing a single container in each pod in the current scale target (e.g. CPU or memory).  Such metrics are built in to Kubernetes, and have special scaling options on top of those available to normal per-pod metrics using the "pods" source.
    /// </summary>
    public partial class ContainerResourceMetricStatusV2
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
        ///     current contains the current value for the given metric
        /// </summary>
        [YamlMember(Alias = "current")]
        [JsonProperty("current", NullValueHandling = NullValueHandling.Include)]
        public MetricValueStatusV2 Current { get; set; }
    }
}
