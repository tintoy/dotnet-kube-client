using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Status of all the conditions for the component as a list of ComponentStatus objects.
    /// </summary>
    [KubeResource("ComponentStatusList", "v1")]
    public class ComponentStatusListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     List of ComponentStatus objects.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ComponentStatusV1> Items { get; set; } = new List<ComponentStatusV1>();
    }
}
