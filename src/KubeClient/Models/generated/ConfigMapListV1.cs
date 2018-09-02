using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ConfigMapList is a resource containing a list of ConfigMap objects.
    /// </summary>
    [KubeListItem("ConfigMap", "v1")]
    [KubeObject("ConfigMapList", "v1")]
    public partial class ConfigMapListV1 : KubeResourceListV1<ConfigMapV1>
    {
        /// <summary>
        ///     Items is the list of ConfigMaps.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ConfigMapV1> Items { get; } = new List<ConfigMapV1>();
    }
}
