using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EventSource contains information for an event.
    /// </summary>
    public partial class EventSourceV1
    {
        /// <summary>
        ///     Component from which the event is generated.
        /// </summary>
        [YamlMember(Alias = "component")]
        [JsonProperty("component", NullValueHandling = NullValueHandling.Ignore)]
        public string Component { get; set; }

        /// <summary>
        ///     Node name on which the event is generated.
        /// </summary>
        [YamlMember(Alias = "host")]
        [JsonProperty("host", NullValueHandling = NullValueHandling.Ignore)]
        public string Host { get; set; }
    }
}
