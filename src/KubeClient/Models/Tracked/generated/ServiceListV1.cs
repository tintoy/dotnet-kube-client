using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ServiceList holds a list of services.
    /// </summary>
    [KubeListItem("Service", "v1")]
    [KubeObject("ServiceList", "v1")]
    public partial class ServiceListV1 : Models.ServiceListV1, ITracked
    {
        /// <summary>
        ///     List of services
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.ServiceV1> Items { get; } = new List<Models.ServiceV1>();
    }
}
