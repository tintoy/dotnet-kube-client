using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     EventSource contains information for an event.
    /// </summary>
    public partial class EventSourceV1
    {
        /// <summary>
        ///     Node name on which the event is generated.
        /// </summary>
        [JsonProperty("host")]
        public string Host { get; set; }

        /// <summary>
        ///     Component from which the event is generated.
        /// </summary>
        [JsonProperty("component")]
        public string Component { get; set; }
    }
}
