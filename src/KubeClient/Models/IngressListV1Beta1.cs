using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressList is a collection of Ingress.
    /// </summary>
    [KubeObject("IngressList", "extensions/v1beta1")]
    public class IngressListV1Beta1 : KubeResourceListV1<IngressV1Beta1>
    {
        /// <summary>
        ///     Items is the list of Ingress.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<IngressV1Beta1> Items { get; } = new List<IngressV1Beta1>();
    }
}
