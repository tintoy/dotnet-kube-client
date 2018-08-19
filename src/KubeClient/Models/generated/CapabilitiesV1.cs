using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        [JsonProperty("add", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Add { get; set; } = new List<string>();

        /// <summary>
        ///     Removed capabilities
        /// </summary>
        [JsonProperty("drop", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Drop { get; set; } = new List<string>();
    }
}
