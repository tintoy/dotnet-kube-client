using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceList holds a list of services.
    /// </summary>
    [KubeObject("ServiceList", "v1")]
    public class ServiceListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     List of services
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ServiceV1> Items { get; set; } = new List<ServiceV1>();
    }
}
