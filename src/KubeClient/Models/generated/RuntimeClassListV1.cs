using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     RuntimeClassList is a list of RuntimeClass objects.
    /// </summary>
    [KubeListItem("RuntimeClass", "node.k8s.io/v1")]
    [KubeObject("RuntimeClassList", "node.k8s.io/v1")]
    public partial class RuntimeClassListV1 : KubeResourceListV1<RuntimeClassV1>
    {
        /// <summary>
        ///     items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<RuntimeClassV1> Items { get; } = new List<RuntimeClassV1>();
    }
}
