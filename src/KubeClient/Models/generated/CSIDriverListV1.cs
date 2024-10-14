using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CSIDriverList is a collection of CSIDriver objects.
    /// </summary>
    [KubeListItem("CSIDriver", "storage.k8s.io/v1")]
    [KubeObject("CSIDriverList", "storage.k8s.io/v1")]
    public partial class CSIDriverListV1 : KubeResourceListV1<CSIDriverV1>
    {
        /// <summary>
        ///     items is the list of CSIDriver
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<CSIDriverV1> Items { get; } = new List<CSIDriverV1>();
    }
}
