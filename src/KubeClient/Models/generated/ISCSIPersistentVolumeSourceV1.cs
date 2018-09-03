using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ISCSIPersistentVolumeSource represents an ISCSI disk. ISCSI volumes can only be mounted as read/write once. ISCSI volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class ISCSIPersistentVolumeSourceV1
    {
        /// <summary>
        ///     iSCSI Target Lun number.
        /// </summary>
        [JsonProperty("lun")]
        [YamlMember(Alias = "lun")]
        public int Lun { get; set; }

        /// <summary>
        ///     Filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#iscsi
        /// </summary>
        [JsonProperty("fsType")]
        [YamlMember(Alias = "fsType")]
        public string FsType { get; set; }

        /// <summary>
        ///     iSCSI Interface Name that uses an iSCSI transport. Defaults to 'default' (tcp).
        /// </summary>
        [JsonProperty("iscsiInterface")]
        [YamlMember(Alias = "iscsiInterface")]
        public string IscsiInterface { get; set; }

        /// <summary>
        ///     iSCSI Target Portal. The Portal is either an IP or ip_addr:port if the port is other than default (typically TCP ports 860 and 3260).
        /// </summary>
        [JsonProperty("targetPortal")]
        [YamlMember(Alias = "targetPortal")]
        public string TargetPortal { get; set; }

        /// <summary>
        ///     whether support iSCSI Discovery CHAP authentication
        /// </summary>
        [JsonProperty("chapAuthDiscovery")]
        [YamlMember(Alias = "chapAuthDiscovery")]
        public bool ChapAuthDiscovery { get; set; }

        /// <summary>
        ///     CHAP Secret for iSCSI target and initiator authentication
        /// </summary>
        [JsonProperty("secretRef")]
        [YamlMember(Alias = "secretRef")]
        public SecretReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     iSCSI Target Portal List. The Portal is either an IP or ip_addr:port if the port is other than default (typically TCP ports 860 and 3260).
        /// </summary>
        [YamlMember(Alias = "portals")]
        [JsonProperty("portals", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Portals { get; set; } = new List<string>();

        /// <summary>
        ///     ReadOnly here will force the ReadOnly setting in VolumeMounts. Defaults to false.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public bool ReadOnly { get; set; }

        /// <summary>
        ///     Target iSCSI Qualified Name.
        /// </summary>
        [JsonProperty("iqn")]
        [YamlMember(Alias = "iqn")]
        public string Iqn { get; set; }

        /// <summary>
        ///     whether support iSCSI Session CHAP authentication
        /// </summary>
        [JsonProperty("chapAuthSession")]
        [YamlMember(Alias = "chapAuthSession")]
        public bool ChapAuthSession { get; set; }

        /// <summary>
        ///     Custom iSCSI Initiator Name. If initiatorName is specified with iscsiInterface simultaneously, new iSCSI interface &lt;target portal&gt;:&lt;volume name&gt; will be created for the connection.
        /// </summary>
        [JsonProperty("initiatorName")]
        [YamlMember(Alias = "initiatorName")]
        public string InitiatorName { get; set; }
    }
}
