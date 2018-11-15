using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     FlexPersistentVolumeSource represents a generic persistent volume resource that is provisioned/attached using an exec based plugin.
    /// </summary>
    public partial class FlexPersistentVolumeSourceV1
    {
        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". The default filesystem depends on FlexVolume script.
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     Optional: SecretRef is reference to the secret object containing sensitive information to pass to the plugin scripts. This may be empty if no secret object is specified. If the secret object contains more than one secret, all secrets are passed to the plugin scripts.
        /// </summary>
        [YamlMember(Alias = "secretRef")]
        [JsonProperty("secretRef", NullValueHandling = NullValueHandling.Ignore)]
        public SecretReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     Driver is the name of the driver to use for this volume.
        /// </summary>
        [YamlMember(Alias = "driver")]
        [JsonProperty("driver", NullValueHandling = NullValueHandling.Include)]
        public string Driver { get; set; }

        /// <summary>
        ///     Optional: Extra command options if any.
        /// </summary>
        [YamlMember(Alias = "options")]
        [JsonProperty("options", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Options { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Options"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeOptions() => Options.Count > 0;

        /// <summary>
        ///     Optional: Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
