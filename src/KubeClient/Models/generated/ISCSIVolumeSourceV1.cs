using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents an ISCSI disk. ISCSI volumes can only be mounted as read/write once. ISCSI volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class ISCSIVolumeSourceV1
    {
        /// <summary>
        ///     Filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#iscsi
        /// </summary>
        [JsonProperty("fsType")]
        [YamlMember(Alias = "fsType")]
        public virtual string FsType { get; set; }

        /// <summary>
        ///     Optional: Defaults to 'default' (tcp). iSCSI interface name that uses an iSCSI transport.
        /// </summary>
        [JsonProperty("iscsiInterface")]
        [YamlMember(Alias = "iscsiInterface")]
        public virtual string IscsiInterface { get; set; }

        /// <summary>
        ///     CHAP secret for iSCSI target and initiator authentication
        /// </summary>
        [JsonProperty("secretRef")]
        [YamlMember(Alias = "secretRef")]
        public virtual LocalObjectReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     iSCSI target portal. The portal is either an IP or ip_addr:port if the port is other than default (typically TCP ports 860 and 3260).
        /// </summary>
        [JsonProperty("targetPortal")]
        [YamlMember(Alias = "targetPortal")]
        public virtual string TargetPortal { get; set; }

        /// <summary>
        ///     whether support iSCSI Session CHAP authentication
        /// </summary>
        [JsonProperty("chapAuthSession")]
        [YamlMember(Alias = "chapAuthSession")]
        public virtual bool ChapAuthSession { get; set; }

        /// <summary>
        ///     Target iSCSI Qualified Name.
        /// </summary>
        [JsonProperty("iqn")]
        [YamlMember(Alias = "iqn")]
        public virtual string Iqn { get; set; }

        /// <summary>
        ///     iSCSI target lun number.
        /// </summary>
        [JsonProperty("lun")]
        [YamlMember(Alias = "lun")]
        public virtual int Lun { get; set; }

        /// <summary>
        ///     iSCSI target portal List. The portal is either an IP or ip_addr:port if the port is other than default (typically TCP ports 860 and 3260).
        /// </summary>
        [YamlMember(Alias = "portals")]
        [JsonProperty("portals", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Portals { get; set; } = new List<string>();

        /// <summary>
        ///     whether support iSCSI Discovery CHAP authentication
        /// </summary>
        [JsonProperty("chapAuthDiscovery")]
        [YamlMember(Alias = "chapAuthDiscovery")]
        public virtual bool ChapAuthDiscovery { get; set; }

        /// <summary>
        ///     ReadOnly here will force the ReadOnly setting in VolumeMounts. Defaults to false.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public virtual bool ReadOnly { get; set; }
    }
}
