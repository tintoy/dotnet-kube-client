using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ConfigMapList is a resource containing a list of ConfigMap objects.
    /// </summary>
    [KubeResource("ConfigMapList", "v1")]
    public class ConfigMapListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is the list of ConfigMaps.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ConfigMapV1> Items { get; set; } = new List<ConfigMapV1>();
    }
}
