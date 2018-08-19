using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a Quobyte mount that lasts the lifetime of a pod. Quobyte volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    public partial class QuobyteVolumeSourceV1
    {
        /// <summary>
        ///     Volume is a string that references an already created Quobyte volume by name.
        /// </summary>
        [JsonProperty("volume")]
        public string Volume { get; set; }

        /// <summary>
        ///     Group to map volume access to Default is no group
        /// </summary>
        [JsonProperty("group")]
        public string Group { get; set; }

        /// <summary>
        ///     User to map volume access to Defaults to serivceaccount user
        /// </summary>
        [JsonProperty("user")]
        public string User { get; set; }

        /// <summary>
        ///     ReadOnly here will force the Quobyte volume to be mounted with read-only permissions. Defaults to false.
        /// </summary>
        [JsonProperty("readOnly")]
        public bool ReadOnly { get; set; }

        /// <summary>
        ///     Registry represents a single or multiple Quobyte Registry services specified as a string as host:port pair (multiple entries are separated with commas) which acts as the central registry for volumes
        /// </summary>
        [JsonProperty("registry")]
        public string Registry { get; set; }
    }
}
