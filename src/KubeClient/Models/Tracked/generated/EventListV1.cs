using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     EventList is a list of events.
    /// </summary>
    [KubeListItem("Event", "v1")]
    [KubeObject("EventList", "v1")]
    public partial class EventListV1 : Models.EventListV1
    {
        /// <summary>
        ///     List of events
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.EventV1> Items { get; } = new List<Models.EventV1>();
    }
}
