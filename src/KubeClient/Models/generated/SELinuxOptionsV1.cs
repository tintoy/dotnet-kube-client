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
        [JsonProperty("role")]
        [YamlMember(Alias = "role")]
        public virtual string Role { get; set; }

        /// <summary>
        ///     Type is a SELinux type label that applies to the container.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public virtual string Type { get; set; }

        /// <summary>
        ///     Level is SELinux level label that applies to the container.
        /// </summary>
        [JsonProperty("level")]
        [YamlMember(Alias = "level")]
        public virtual string Level { get; set; }

        /// <summary>
        ///     User is a SELinux user label that applies to the container.
        /// </summary>
        [JsonProperty("user")]
        [YamlMember(Alias = "user")]
        public virtual string User { get; set; }
    }
}
