using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Adds and removes POSIX capabilities from running containers.
    /// </summary>
    public partial class CapabilitiesV1 : Models.CapabilitiesV1, ITracked
    {
        /// <summary>
        ///     Added capabilities
        /// </summary>
        [YamlMember(Alias = "add")]
        [JsonProperty("add", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Add { get; set; } = new List<string>();

        /// <summary>
        ///     Removed capabilities
        /// </summary>
        [YamlMember(Alias = "drop")]
        [JsonProperty("drop", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Drop { get; set; } = new List<string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
