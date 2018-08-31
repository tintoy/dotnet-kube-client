using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     APIServiceList is a list of APIService objects.
    /// </summary>
    [KubeListItem("APIService", "v1beta1")]
    [KubeObject("APIServiceList", "v1beta1")]
    public partial class APIServiceListV1Beta1 : Models.APIServiceListV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.APIServiceV1Beta1> Items { get; } = new List<Models.APIServiceV1Beta1>();
    }
}
