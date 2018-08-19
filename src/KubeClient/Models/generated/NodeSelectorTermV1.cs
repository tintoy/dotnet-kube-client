using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A null or empty node selector term matches no objects.
    /// </summary>
    public partial class NodeSelectorTermV1
    {
        /// <summary>
        ///     Required. A list of node selector requirements. The requirements are ANDed.
        /// </summary>
        [YamlMember(Alias = "matchExpressions")]
        [JsonProperty("matchExpressions", NullValueHandling = NullValueHandling.Ignore)]
        public List<NodeSelectorRequirementV1> MatchExpressions { get; set; } = new List<NodeSelectorRequirementV1>();
    }
}
