using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ConfigMapList is a resource containing a list of ConfigMap objects.
    /// </summary>
    [KubeListItem("ConfigMap", "v1")]
    [KubeObject("ConfigMapList", "v1")]
    public partial class ConfigMapListV1 : Models.ConfigMapListV1, ITracked
    {
        /// <summary>
        ///     Items is the list of ConfigMaps.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.ConfigMapV1> Items { get; } = new List<Models.ConfigMapV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
