using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressClassList is a collection of IngressClasses.
    /// </summary>
    [KubeListItem("IngressClass", "networking.k8s.io/v1")]
    [KubeObject("IngressClassList", "networking.k8s.io/v1")]
    public partial class IngressClassListV1 : KubeResourceListV1<IngressClassV1>
    {
        /// <summary>
        ///     items is the list of IngressClasses.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<IngressClassV1> Items { get; } = new List<IngressClassV1>();
    }
}
