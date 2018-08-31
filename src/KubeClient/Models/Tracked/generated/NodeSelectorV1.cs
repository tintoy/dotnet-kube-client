using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     A node selector represents the union of the results of one or more label queries over a set of nodes; that is, it represents the OR of the selectors represented by the node selector terms.
    /// </summary>
    public partial class NodeSelectorV1 : Models.NodeSelectorV1, ITracked
    {
        /// <summary>
        ///     Required. A list of node selector terms. The terms are ORed.
        /// </summary>
        [YamlMember(Alias = "nodeSelectorTerms")]
        [JsonProperty("nodeSelectorTerms", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.NodeSelectorTermV1> NodeSelectorTerms { get; set; } = new List<Models.NodeSelectorTermV1>();
    }
}
