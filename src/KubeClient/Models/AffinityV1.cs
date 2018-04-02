using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Affinity is a group of affinity scheduling rules.
    /// </summary>
    [KubeObject("Affinity", "v1")]
    public partial class AffinityV1
    {
        /// <summary>
        ///     Describes node affinity scheduling rules for the pod.
        /// </summary>
        [JsonProperty("nodeAffinity")]
        public NodeAffinityV1 NodeAffinity { get; set; }

        /// <summary>
        ///     Describes pod affinity scheduling rules (e.g. co-locate this pod in the same node, zone, etc. as some other pod(s)).
        /// </summary>
        [JsonProperty("podAffinity")]
        public PodAffinityV1 PodAffinity { get; set; }

        /// <summary>
        ///     Describes pod anti-affinity scheduling rules (e.g. avoid putting this pod in the same node, zone, etc. as some other pod(s)).
        /// </summary>
        [JsonProperty("podAntiAffinity")]
        public PodAntiAffinityV1 PodAntiAffinity { get; set; }
    }
}
