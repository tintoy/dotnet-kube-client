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
        [JsonProperty("component")]
        [YamlMember(Alias = "component")]
        public string Component { get; set; }

        /// <summary>
        ///     Node name on which the event is generated.
        /// </summary>
        [JsonProperty("host")]
        [YamlMember(Alias = "host")]
        public string Host { get; set; }
    }
}
