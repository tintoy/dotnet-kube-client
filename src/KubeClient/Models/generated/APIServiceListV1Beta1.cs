using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     APIServiceList is a list of APIService objects.
    /// </summary>
    [KubeListItem("APIService", "apiregistration.k8s.io/v1beta1")]
    [KubeObject("APIServiceList", "apiregistration.k8s.io/v1beta1")]
    public partial class APIServiceListV1Beta1 : KubeResourceListV1<APIServiceV1Beta1>
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<APIServiceV1Beta1> Items { get; } = new List<APIServiceV1Beta1>();
    }
}
