using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeList is the whole list of all Nodes which have been registered with master.
    /// </summary>
    [KubeObject("NodeList", "v1")]
    public class NodeListV1 : KubeResourceListV1<NodeV1>
    {
        /// <summary>
        ///     List of nodes
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<NodeV1> Items { get; } = new List<NodeV1>();
    }
}
