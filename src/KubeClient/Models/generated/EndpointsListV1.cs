using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EndpointsList is a list of endpoints.
    /// </summary>
    [KubeListItem("Endpoints", "v1")]
    [KubeObject("EndpointsList", "v1")]
    public partial class EndpointsListV1 : KubeResourceListV1<EndpointsV1>
    {
        /// <summary>
        ///     List of endpoints.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<EndpointsV1> Items { get; } = new List<EndpointsV1>();
    }
}
