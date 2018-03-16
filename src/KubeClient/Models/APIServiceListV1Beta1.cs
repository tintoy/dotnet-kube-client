using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     APIServiceList is a list of APIService objects.
    /// </summary>
    [KubeObject("APIServiceList", "v1beta1")]
    public class APIServiceListV1Beta1 : KubeResourceListV1<APIServiceV1Beta1>
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<APIServiceV1Beta1> Items { get; } = new List<APIServiceV1Beta1>();
    }
}
