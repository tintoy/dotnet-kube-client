using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     AppArmorProfile defines a pod or container's AppArmor settings.
    /// </summary>
    public partial class AppArmorProfileV1
    {
        /// <summary>
        ///     localhostProfile indicates a profile loaded on the node that should be used. The profile must be preconfigured on the node to work. Must match the loaded name of the profile. Must be set if and only if type is "Localhost".
        /// </summary>
        [YamlMember(Alias = "localhostProfile")]
        [JsonProperty("localhostProfile", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalhostProfile { get; set; }

        /// <summary>
        ///     type indicates which kind of AppArmor profile will be applied. Valid options are:
        ///       Localhost - a profile pre-loaded on the node.
        ///       RuntimeDefault - the container runtime's default profile.
        ///       Unconfined - no AppArmor enforcement.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }
    }
}
