using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a Quobyte mount that lasts the lifetime of a pod. Quobyte volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    public partial class QuobyteVolumeSourceV1
    {
        /// <summary>
        ///     volume is a string that references an already created Quobyte volume by name.
        /// </summary>
        [YamlMember(Alias = "volume")]
        [JsonProperty("volume", NullValueHandling = NullValueHandling.Include)]
        public string Volume { get; set; }

        /// <summary>
        ///     group to map volume access to Default is no group
        /// </summary>
        [YamlMember(Alias = "group")]
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }

        /// <summary>
        ///     user to map volume access to Defaults to serivceaccount user
        /// </summary>
        [YamlMember(Alias = "user")]
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }

        /// <summary>
        ///     tenant owning the given Quobyte volume in the Backend Used with dynamically provisioned Quobyte volumes, value is set by the plugin
        /// </summary>
        [YamlMember(Alias = "tenant")]
        [JsonProperty("tenant", NullValueHandling = NullValueHandling.Ignore)]
        public string Tenant { get; set; }

        /// <summary>
        ///     readOnly here will force the Quobyte volume to be mounted with read-only permissions. Defaults to false.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }

        /// <summary>
        ///     registry represents a single or multiple Quobyte Registry services specified as a string as host:port pair (multiple entries are separated with commas) which acts as the central registry for volumes
        /// </summary>
        [YamlMember(Alias = "registry")]
        [JsonProperty("registry", NullValueHandling = NullValueHandling.Include)]
        public string Registry { get; set; }
    }
}
