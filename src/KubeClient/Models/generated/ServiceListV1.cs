using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceList holds a list of services.
    /// </summary>
    public partial class ServiceListV1 : KubeResourceListV1<ServiceV1>
    {
        /// <summary>
        ///     List of services
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ServiceV1> Items { get; } = new List<ServiceV1>();
    }
}
