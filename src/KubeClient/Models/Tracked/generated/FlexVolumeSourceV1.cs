using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     FlexVolume represents a generic volume resource that is provisioned/attached using an exec based plugin. This is an alpha feature and may change in future.
    /// </summary>
    public partial class FlexVolumeSourceV1 : Models.FlexVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". The default filesystem depends on FlexVolume script.
        /// </summary>
        [JsonProperty("fsType")]
        [YamlMember(Alias = "fsType")]
        public override string FsType
        {
            get
            {
                return base.FsType;
            }
            set
            {
                base.FsType = value;

                __ModifiedProperties__.Add("FsType");
            }
        }


        /// <summary>
        ///     Optional: SecretRef is reference to the secret object containing sensitive information to pass to the plugin scripts. This may be empty if no secret object is specified. If the secret object contains more than one secret, all secrets are passed to the plugin scripts.
        /// </summary>
        [JsonProperty("secretRef")]
        [YamlMember(Alias = "secretRef")]
        public override Models.LocalObjectReferenceV1 SecretRef
        {
            get
            {
                return base.SecretRef;
            }
            set
            {
                base.SecretRef = value;

                __ModifiedProperties__.Add("SecretRef");
            }
        }


        /// <summary>
        ///     Driver is the name of the driver to use for this volume.
        /// </summary>
        [JsonProperty("driver")]
        [YamlMember(Alias = "driver")]
        public override string Driver
        {
            get
            {
                return base.Driver;
            }
            set
            {
                base.Driver = value;

                __ModifiedProperties__.Add("Driver");
            }
        }


        /// <summary>
        ///     Optional: Extra command options if any.
        /// </summary>
        [YamlMember(Alias = "options")]
        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Optional: Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;

                __ModifiedProperties__.Add("ReadOnly");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
