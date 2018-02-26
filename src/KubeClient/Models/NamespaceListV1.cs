using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NamespaceList is a list of Namespaces.
    /// </summary>
    [KubeObject("NamespaceList", "v1")]
    public class NamespaceListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is the list of Namespace objects in the list. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<NamespaceV1> Items { get; set; } = new List<NamespaceV1>();
    }
}
