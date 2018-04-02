using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     A null or empty node selector term matches no objects.
    /// </summary>
    [KubeObject("NodeSelectorTerm", "v1")]
    public partial class NodeSelectorTermV1
    {
        /// <summary>
        ///     Required. A list of node selector requirements. The requirements are ANDed.
        /// </summary>
        [JsonProperty("matchExpressions", NullValueHandling = NullValueHandling.Ignore)]
        public List<NodeSelectorRequirementV1> MatchExpressions { get; set; } = new List<NodeSelectorRequirementV1>();
    }
}
