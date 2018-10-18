using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SELinuxOptions are the labels to be applied to the container
    /// </summary>
    public partial class SELinuxOptionsV1
    {
        /// <summary>
        ///     Role is a SELinux role label that applies to the container.
        /// </summary>
        [YamlMember(Alias = "role")]
        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

        /// <summary>
        ///     Type is a SELinux type label that applies to the container.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        ///     Level is SELinux level label that applies to the container.
        /// </summary>
        [YamlMember(Alias = "level")]
        [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public string Level { get; set; }

        /// <summary>
        ///     User is a SELinux user label that applies to the container.
        /// </summary>
        [YamlMember(Alias = "user")]
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }
    }
}
