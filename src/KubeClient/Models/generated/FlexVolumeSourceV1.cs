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
        ///     fsType is the filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". The default filesystem depends on FlexVolume script.
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     secretRef is Optional: secretRef is reference to the secret object containing sensitive information to pass to the plugin scripts. This may be empty if no secret object is specified. If the secret object contains more than one secret, all secrets are passed to the plugin scripts.
        /// </summary>
        [YamlMember(Alias = "secretRef")]
        [JsonProperty("secretRef", NullValueHandling = NullValueHandling.Ignore)]
        public LocalObjectReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     driver is the name of the driver to use for this volume.
        /// </summary>
        [YamlMember(Alias = "driver")]
        [JsonProperty("driver", NullValueHandling = NullValueHandling.Include)]
        public string Driver { get; set; }

        /// <summary>
        ///     options is Optional: this field holds extra command options if any.
        /// </summary>
        [YamlMember(Alias = "options")]
        [JsonProperty("options", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Options { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Options"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeOptions() => Options.Count > 0;

        /// <summary>
        ///     readOnly is Optional: defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
