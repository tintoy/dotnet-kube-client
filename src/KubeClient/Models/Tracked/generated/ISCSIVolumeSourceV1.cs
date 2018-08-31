using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents an ISCSI disk. ISCSI volumes can only be mounted as read/write once. ISCSI volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class ISCSIVolumeSourceV1 : Models.ISCSIVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     Filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#iscsi
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
        ///     Optional: Defaults to 'default' (tcp). iSCSI interface name that uses an iSCSI transport.
        /// </summary>
        [JsonProperty("iscsiInterface")]
        [YamlMember(Alias = "iscsiInterface")]
        public override string IscsiInterface
        {
            get
            {
                return base.IscsiInterface;
            }
            set
            {
                base.IscsiInterface = value;

                __ModifiedProperties__.Add("IscsiInterface");
            }
        }


        /// <summary>
        ///     CHAP secret for iSCSI target and initiator authentication
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
        ///     iSCSI target portal. The portal is either an IP or ip_addr:port if the port is other than default (typically TCP ports 860 and 3260).
        /// </summary>
        [JsonProperty("targetPortal")]
        [YamlMember(Alias = "targetPortal")]
        public override string TargetPortal
        {
            get
            {
                return base.TargetPortal;
            }
            set
            {
                base.TargetPortal = value;

                __ModifiedProperties__.Add("TargetPortal");
            }
        }


        /// <summary>
        ///     whether support iSCSI Session CHAP authentication
        /// </summary>
        [JsonProperty("chapAuthSession")]
        [YamlMember(Alias = "chapAuthSession")]
        public override bool ChapAuthSession
        {
            get
            {
                return base.ChapAuthSession;
            }
            set
            {
                base.ChapAuthSession = value;

                __ModifiedProperties__.Add("ChapAuthSession");
            }
        }


        /// <summary>
        ///     Target iSCSI Qualified Name.
        /// </summary>
        [JsonProperty("iqn")]
        [YamlMember(Alias = "iqn")]
        public override string Iqn
        {
            get
            {
                return base.Iqn;
            }
            set
            {
                base.Iqn = value;

                __ModifiedProperties__.Add("Iqn");
            }
        }


        /// <summary>
        ///     iSCSI target lun number.
        /// </summary>
        [JsonProperty("lun")]
        [YamlMember(Alias = "lun")]
        public override int Lun
        {
            get
            {
                return base.Lun;
            }
            set
            {
                base.Lun = value;

                __ModifiedProperties__.Add("Lun");
            }
        }


        /// <summary>
        ///     iSCSI target portal List. The portal is either an IP or ip_addr:port if the port is other than default (typically TCP ports 860 and 3260).
        /// </summary>
        [YamlMember(Alias = "portals")]
        [JsonProperty("portals", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Portals { get; set; } = new List<string>();

        /// <summary>
        ///     whether support iSCSI Discovery CHAP authentication
        /// </summary>
        [JsonProperty("chapAuthDiscovery")]
        [YamlMember(Alias = "chapAuthDiscovery")]
        public override bool ChapAuthDiscovery
        {
            get
            {
                return base.ChapAuthDiscovery;
            }
            set
            {
                base.ChapAuthDiscovery = value;

                __ModifiedProperties__.Add("ChapAuthDiscovery");
            }
        }


        /// <summary>
        ///     ReadOnly here will force the ReadOnly setting in VolumeMounts. Defaults to false.
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
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
