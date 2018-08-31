using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Status of all the conditions for the component as a list of ComponentStatus objects.
    /// </summary>
    [KubeListItem("ComponentStatus", "v1")]
    [KubeObject("ComponentStatusList", "v1")]
    public partial class ComponentStatusListV1 : Models.ComponentStatusListV1, ITracked
    {
        /// <summary>
        ///     List of ComponentStatus objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.ComponentStatusV1> Items { get; } = new List<Models.ComponentStatusV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
