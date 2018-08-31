using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a StorageOS persistent volume resource.
    /// </summary>
    public partial class StorageOSPersistentVolumeSourceV1 : Models.StorageOSPersistentVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     Filesystem type to mount. Must be a filesystem type supported by the host operating system. Ex. "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified.
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
        ///     VolumeName is the human-readable name of the StorageOS volume.  Volume names are only unique within a namespace.
        /// </summary>
        [JsonProperty("volumeName")]
        [YamlMember(Alias = "volumeName")]
        public override string VolumeName
        {
            get
            {
                return base.VolumeName;
            }
            set
            {
                base.VolumeName = value;

                __ModifiedProperties__.Add("VolumeName");
            }
        }


        /// <summary>
        ///     VolumeNamespace specifies the scope of the volume within StorageOS.  If no namespace is specified then the Pod's namespace will be used.  This allows the Kubernetes name scoping to be mirrored within StorageOS for tighter integration. Set VolumeName to any name to override the default behaviour. Set to "default" if you are not using namespaces within StorageOS. Namespaces that do not pre-exist within StorageOS will be created.
        /// </summary>
        [JsonProperty("volumeNamespace")]
        [YamlMember(Alias = "volumeNamespace")]
        public override string VolumeNamespace
        {
            get
            {
                return base.VolumeNamespace;
            }
            set
            {
                base.VolumeNamespace = value;

                __ModifiedProperties__.Add("VolumeNamespace");
            }
        }


        /// <summary>
        ///     SecretRef specifies the secret to use for obtaining the StorageOS API credentials.  If not specified, default values will be attempted.
        /// </summary>
        [JsonProperty("secretRef")]
        [YamlMember(Alias = "secretRef")]
        public override Models.ObjectReferenceV1 SecretRef
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
        ///     Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts.
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
