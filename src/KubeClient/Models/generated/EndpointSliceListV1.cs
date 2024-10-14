using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EndpointSliceList represents a list of endpoint slices
    /// </summary>
    [KubeListItem("EndpointSlice", "discovery.k8s.io/v1")]
    [KubeObject("EndpointSliceList", "discovery.k8s.io/v1")]
    public partial class EndpointSliceListV1 : KubeResourceListV1<EndpointSliceV1>
    {
        /// <summary>
        ///     items is the list of endpoint slices
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<EndpointSliceV1> Items { get; } = new List<EndpointSliceV1>();
    }
}
