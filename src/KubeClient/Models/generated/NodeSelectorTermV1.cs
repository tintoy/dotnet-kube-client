using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A null or empty node selector term matches no objects. The requirements of them are ANDed. The TopologySelectorTerm type implements a subset of the NodeSelectorTerm.
    /// </summary>
    public partial class NodeSelectorTermV1
    {
        /// <summary>
        ///     A list of node selector requirements by node's labels.
        /// </summary>
        [YamlMember(Alias = "matchExpressions")]
        [JsonProperty("matchExpressions", NullValueHandling = NullValueHandling.Ignore)]
        public List<NodeSelectorRequirementV1> MatchExpressions { get; set; } = new List<NodeSelectorRequirementV1>();

        /// <summary>
        ///     A list of node selector requirements by node's fields.
        /// </summary>
        [YamlMember(Alias = "matchFields")]
        [JsonProperty("matchFields", NullValueHandling = NullValueHandling.Ignore)]
        public List<NodeSelectorRequirementV1> MatchFields { get; set; } = new List<NodeSelectorRequirementV1>();
    }
}
