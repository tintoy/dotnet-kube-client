using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Affinity is a group of affinity scheduling rules.
    /// </summary>
    public partial class AffinityV1
    {
        /// <summary>
        ///     Describes pod anti-affinity scheduling rules (e.g. avoid putting this pod in the same node, zone, etc. as some other pod(s)).
        /// </summary>
        [JsonProperty("podAntiAffinity")]
        [YamlMember(Alias = "podAntiAffinity")]
        public PodAntiAffinityV1 PodAntiAffinity { get; set; }

        /// <summary>
        ///     Describes node affinity scheduling rules for the pod.
        /// </summary>
        [JsonProperty("nodeAffinity")]
        [YamlMember(Alias = "nodeAffinity")]
        public NodeAffinityV1 NodeAffinity { get; set; }

        /// <summary>
        ///     Describes pod affinity scheduling rules (e.g. co-locate this pod in the same node, zone, etc. as some other pod(s)).
        /// </summary>
        [JsonProperty("podAffinity")]
        [YamlMember(Alias = "podAffinity")]
        public PodAffinityV1 PodAffinity { get; set; }
    }
}
