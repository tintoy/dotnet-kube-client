using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CSINodeList is a collection of CSINode objects.
    /// </summary>
    [KubeListItem("CSINode", "storage.k8s.io/v1")]
    [KubeObject("CSINodeList", "storage.k8s.io/v1")]
    public partial class CSINodeListV1 : KubeResourceListV1<CSINodeV1>
    {
        /// <summary>
        ///     items is the list of CSINode
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<CSINodeV1> Items { get; } = new List<CSINodeV1>();
    }
}
