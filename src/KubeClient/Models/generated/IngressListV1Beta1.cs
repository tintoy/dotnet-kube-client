using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressList is a collection of Ingress.
    /// </summary>
    [KubeListItem("Ingress", "extensions/v1beta1")]
    [KubeObject("IngressList", "extensions/v1beta1")]
    public partial class IngressListV1Beta1 : KubeResourceListV1<IngressV1Beta1>
    {
        /// <summary>
        ///     Items is the list of Ingress.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<IngressV1Beta1> Items { get; } = new List<IngressV1Beta1>();
    }
}
