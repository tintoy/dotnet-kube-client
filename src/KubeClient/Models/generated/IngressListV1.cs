using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressList is a collection of Ingress.
    /// </summary>
    [KubeListItem("Ingress", "networking.k8s.io/v1")]
    [KubeObject("IngressList", "networking.k8s.io/v1")]
    public partial class IngressListV1 : KubeResourceListV1<IngressV1>
    {
        /// <summary>
        ///     items is the list of Ingress.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<IngressV1> Items { get; } = new List<IngressV1>();
    }
}
