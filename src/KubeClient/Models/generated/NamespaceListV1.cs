using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NamespaceList is a list of Namespaces.
    /// </summary>
    [KubeListItem("Namespace", "v1")]
    [KubeObject("NamespaceList", "v1")]
    public partial class NamespaceListV1 : KubeResourceListV1<NamespaceV1>
    {
        /// <summary>
        ///     Items is the list of Namespace objects in the list. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<NamespaceV1> Items { get; } = new List<NamespaceV1>();
    }
}
