using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Affinity is a group of affinity scheduling rules.
    /// </summary>
    public partial class AffinityV1 : Models.AffinityV1
    {
        /// <summary>
        ///     Describes node affinity scheduling rules for the pod.
        /// </summary>
        [JsonProperty("nodeAffinity")]
        [YamlMember(Alias = "nodeAffinity")]
        public override Models.NodeAffinityV1 NodeAffinity
        {
            get
            {
                return base.NodeAffinity;
            }
            set
            {
                base.NodeAffinity = value;

                __ModifiedProperties__.Add("NodeAffinity");
            }
        }


        /// <summary>
        ///     Describes pod affinity scheduling rules (e.g. co-locate this pod in the same node, zone, etc. as some other pod(s)).
        /// </summary>
        [JsonProperty("podAffinity")]
        [YamlMember(Alias = "podAffinity")]
        public override Models.PodAffinityV1 PodAffinity
        {
            get
            {
                return base.PodAffinity;
            }
            set
            {
                base.PodAffinity = value;

                __ModifiedProperties__.Add("PodAffinity");
            }
        }


        /// <summary>
        ///     Describes pod anti-affinity scheduling rules (e.g. avoid putting this pod in the same node, zone, etc. as some other pod(s)).
        /// </summary>
        [JsonProperty("podAntiAffinity")]
        [YamlMember(Alias = "podAntiAffinity")]
        public override Models.PodAntiAffinityV1 PodAntiAffinity
        {
            get
            {
                return base.PodAntiAffinity;
            }
            set
            {
                base.PodAntiAffinity = value;

                __ModifiedProperties__.Add("PodAntiAffinity");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
