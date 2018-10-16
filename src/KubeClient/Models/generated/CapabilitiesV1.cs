using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Adds and removes POSIX capabilities from running containers.
    /// </summary>
    public partial class CapabilitiesV1
    {
        /// <summary>
        ///     Added capabilities
        /// </summary>
        [YamlMember(Alias = "add")]
        [JsonProperty("add", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Add { get; set; } = new List<string>();

        /// <summary>
        ///     Removed capabilities
        /// </summary>
        [YamlMember(Alias = "drop")]
        [JsonProperty("drop", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Drop { get; set; } = new List<string>();
    }
}
