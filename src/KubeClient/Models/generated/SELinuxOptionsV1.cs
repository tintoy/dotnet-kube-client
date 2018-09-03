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
        ///     User is a SELinux user label that applies to the container.
        /// </summary>
        [JsonProperty("user")]
        [YamlMember(Alias = "user")]
        public string User { get; set; }

        /// <summary>
        ///     Role is a SELinux role label that applies to the container.
        /// </summary>
        [JsonProperty("role")]
        [YamlMember(Alias = "role")]
        public string Role { get; set; }

        /// <summary>
        ///     Level is SELinux level label that applies to the container.
        /// </summary>
        [JsonProperty("level")]
        [YamlMember(Alias = "level")]
        public string Level { get; set; }

        /// <summary>
        ///     Type is a SELinux type label that applies to the container.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public string Type { get; set; }
    }
}
