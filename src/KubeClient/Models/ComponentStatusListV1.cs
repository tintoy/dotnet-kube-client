using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Status of all the conditions for the component as a list of ComponentStatus objects.
    /// </summary>
    [KubeObject("ComponentStatusList", "v1")]
    public class ComponentStatusListV1 : KubeResourceListV1<ComponentStatusV1>
    {
        /// <summary>
        ///     List of ComponentStatus objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ComponentStatusV1> Items { get; } = new List<ComponentStatusV1>();
    }
}
