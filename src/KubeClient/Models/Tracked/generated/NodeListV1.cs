using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NodeList is the whole list of all Nodes which have been registered with master.
    /// </summary>
    [KubeListItem("Node", "v1")]
    [KubeObject("NodeList", "v1")]
    public partial class NodeListV1 : Models.NodeListV1
    {
        /// <summary>
        ///     List of nodes
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.NodeV1> Items { get; } = new List<Models.NodeV1>();
    }
}
