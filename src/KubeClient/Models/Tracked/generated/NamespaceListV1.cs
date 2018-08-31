using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     NamespaceList is a list of Namespaces.
    /// </summary>
    [KubeListItem("Namespace", "v1")]
    [KubeObject("NamespaceList", "v1")]
    public partial class NamespaceListV1 : Models.NamespaceListV1, ITracked
    {
        /// <summary>
        ///     Items is the list of Namespace objects in the list. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.NamespaceV1> Items { get; } = new List<Models.NamespaceV1>();
    }
}
