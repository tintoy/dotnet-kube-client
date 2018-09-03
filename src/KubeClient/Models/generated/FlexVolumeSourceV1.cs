using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     FlexVolume represents a generic volume resource that is provisioned/attached using an exec based plugin.
    /// </summary>
    public partial class FlexVolumeSourceV1
    {
        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". The default filesystem depends on FlexVolume script.
        /// </summary>
        [JsonProperty("fsType")]
        [YamlMember(Alias = "fsType")]
        public string FsType { get; set; }

        /// <summary>
        ///     Optional: SecretRef is reference to the secret object containing sensitive information to pass to the plugin scripts. This may be empty if no secret object is specified. If the secret object contains more than one secret, all secrets are passed to the plugin scripts.
        /// </summary>
        [JsonProperty("secretRef")]
        [YamlMember(Alias = "secretRef")]
        public LocalObjectReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     Driver is the name of the driver to use for this volume.
        /// </summary>
        [JsonProperty("driver")]
        [YamlMember(Alias = "driver")]
        public string Driver { get; set; }

        /// <summary>
        ///     Optional: Extra command options if any.
        /// </summary>
        [YamlMember(Alias = "options")]
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Optional: Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public bool ReadOnly { get; set; }
    }
}
