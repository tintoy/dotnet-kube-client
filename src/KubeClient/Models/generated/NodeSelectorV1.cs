using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A node selector represents the union of the results of one or more label queries over a set of nodes; that is, it represents the OR of the selectors represented by the node selector terms.
    /// </summary>
    public partial class NodeSelectorV1
    {
        /// <summary>
        ///     Required. A list of node selector terms. The terms are ORed.
        /// </summary>
        [YamlMember(Alias = "nodeSelectorTerms")]
        [JsonProperty("nodeSelectorTerms", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<NodeSelectorTermV1> NodeSelectorTerms { get; } = new List<NodeSelectorTermV1>();
    }
}
