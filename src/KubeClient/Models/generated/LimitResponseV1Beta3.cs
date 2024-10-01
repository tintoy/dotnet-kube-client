using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LimitResponse defines how to handle requests that can not be executed right now.
    /// </summary>
    public partial class LimitResponseV1Beta3
    {
        /// <summary>
        ///     `type` is "Queue" or "Reject". "Queue" means that requests that can not be executed upon arrival are held in a queue until they can be executed or a queuing limit is reached. "Reject" means that requests that can not be executed upon arrival are rejected. Required.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     `queuing` holds the configuration parameters for queuing. This field may be non-empty only if `type` is `"Queue"`.
        /// </summary>
        [YamlMember(Alias = "queuing")]
        [JsonProperty("queuing", NullValueHandling = NullValueHandling.Ignore)]
        public QueuingConfigurationV1Beta3 Queuing { get; set; }
    }
}
