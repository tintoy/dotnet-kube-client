using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     A node selector represents the union of the results of one or more label queries over a set of nodes; that is, it represents the OR of the selectors represented by the node selector terms.
    /// </summary>
    [KubeObject("NodeSelector", "v1")]
    public class NodeSelectorV1
    {
        /// <summary>
        ///     Required. A list of node selector terms. The terms are ORed.
        /// </summary>
        [JsonProperty("nodeSelectorTerms", NullValueHandling = NullValueHandling.Ignore)]
        public List<NodeSelectorTermV1> NodeSelectorTerms { get; set; } = new List<NodeSelectorTermV1>();
    }
}
