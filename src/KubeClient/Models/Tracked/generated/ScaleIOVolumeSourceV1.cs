using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ScaleIOVolumeSource represents a persistent ScaleIO volume
    /// </summary>
    public partial class ScaleIOVolumeSourceV1 : Models.ScaleIOVolumeSourceV1
    {
        /// <summary>
        ///     Flag to enable/disable SSL communication with Gateway, default false
        /// </summary>
        [JsonProperty("sslEnabled")]
        [YamlMember(Alias = "sslEnabled")]
        public override bool SslEnabled
        {
            get
            {
                return base.SslEnabled;
            }
            set
            {
                base.SslEnabled = value;

                __ModifiedProperties__.Add("SslEnabled");
            }
        }


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
        ///     Indicates whether the storage for a volume should be thick or thin (defaults to "thin").
        /// </summary>
        [JsonProperty("storageMode")]
        [YamlMember(Alias = "storageMode")]
        public override string StorageMode
        {
            get
            {
                return base.StorageMode;
            }
            set
            {
                base.StorageMode = value;

                __ModifiedProperties__.Add("StorageMode");
            }
        }


        /// <summary>
        ///     The name of a volume already created in the ScaleIO system that is associated with this volume source.
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
        ///     SecretRef references to the secret for ScaleIO user and other sensitive information. If this is not provided, Login operation will fail.
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
        ///     The Storage Pool associated with the protection domain (defaults to "default").
        /// </summary>
        [JsonProperty("storagePool")]
        [YamlMember(Alias = "storagePool")]
        public override string StoragePool
        {
            get
            {
                return base.StoragePool;
            }
            set
            {
                base.StoragePool = value;

                __ModifiedProperties__.Add("StoragePool");
            }
        }


        /// <summary>
        ///     The name of the storage system as configured in ScaleIO.
        /// </summary>
        [JsonProperty("system")]
        [YamlMember(Alias = "system")]
        public override string System
        {
            get
            {
                return base.System;
            }
            set
            {
                base.System = value;

                __ModifiedProperties__.Add("System");
            }
        }


        /// <summary>
        ///     The name of the Protection Domain for the configured storage (defaults to "default").
        /// </summary>
        [JsonProperty("protectionDomain")]
        [YamlMember(Alias = "protectionDomain")]
        public override string ProtectionDomain
        {
            get
            {
                return base.ProtectionDomain;
            }
            set
            {
                base.ProtectionDomain = value;

                __ModifiedProperties__.Add("ProtectionDomain");
            }
        }


        /// <summary>
        ///     The host address of the ScaleIO API Gateway.
        /// </summary>
        [JsonProperty("gateway")]
        [YamlMember(Alias = "gateway")]
        public override string Gateway
        {
            get
            {
                return base.Gateway;
            }
            set
            {
                base.Gateway = value;

                __ModifiedProperties__.Add("Gateway");
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
