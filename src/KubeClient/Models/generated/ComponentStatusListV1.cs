using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Status of all the conditions for the component as a list of ComponentStatus objects.
    /// </summary>
    [KubeListItem("ComponentStatus", "v1")]
    [KubeObject("ComponentStatusList", "v1")]
    public partial class ComponentStatusListV1 : KubeResourceListV1<ComponentStatusV1>
    {
        /// <summary>
        ///     List of ComponentStatus objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ComponentStatusV1> Items { get; } = new List<ComponentStatusV1>();
    }
}
