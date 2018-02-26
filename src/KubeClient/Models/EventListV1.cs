using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     EventList is a list of events.
    /// </summary>
    [KubeObject("EventList", "v1")]
    public class EventListV1 : KubeResourceListV1
    {
        /// <summary>
        ///     List of events
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<EventV1> Items { get; set; } = new List<EventV1>();
    }
}
