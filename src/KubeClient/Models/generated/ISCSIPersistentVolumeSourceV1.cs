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
        ///     Filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#iscsi
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     Custom iSCSI Initiator Name. If initiatorName is specified with iscsiInterface simultaneously, new iSCSI interface &lt;target portal&gt;:&lt;volume name&gt; will be created for the connection.
        /// </summary>
        [YamlMember(Alias = "initiatorName")]
        [JsonProperty("initiatorName", NullValueHandling = NullValueHandling.Ignore)]
        public string InitiatorName { get; set; }

        /// <summary>
        ///     iSCSI Interface Name that uses an iSCSI transport. Defaults to 'default' (tcp).
        /// </summary>
        [YamlMember(Alias = "iscsiInterface")]
        [JsonProperty("iscsiInterface", NullValueHandling = NullValueHandling.Ignore)]
        public string IscsiInterface { get; set; }

        /// <summary>
        ///     CHAP Secret for iSCSI target and initiator authentication
        /// </summary>
        [YamlMember(Alias = "secretRef")]
        [JsonProperty("secretRef", NullValueHandling = NullValueHandling.Ignore)]
        public SecretReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     iSCSI Target Portal. The Portal is either an IP or ip_addr:port if the port is other than default (typically TCP ports 860 and 3260).
        /// </summary>
        [YamlMember(Alias = "targetPortal")]
        [JsonProperty("targetPortal", NullValueHandling = NullValueHandling.Include)]
        public string TargetPortal { get; set; }

        /// <summary>
        ///     whether support iSCSI Session CHAP authentication
        /// </summary>
        [YamlMember(Alias = "chapAuthSession")]
        [JsonProperty("chapAuthSession", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ChapAuthSession { get; set; }

        /// <summary>
        ///     Target iSCSI Qualified Name.
        /// </summary>
        [YamlMember(Alias = "iqn")]
        [JsonProperty("iqn", NullValueHandling = NullValueHandling.Include)]
        public string Iqn { get; set; }

        /// <summary>
        ///     iSCSI Target Lun number.
        /// </summary>
        [YamlMember(Alias = "lun")]
        [JsonProperty("lun", NullValueHandling = NullValueHandling.Include)]
        public int Lun { get; set; }

        /// <summary>
        ///     iSCSI Target Portal List. The Portal is either an IP or ip_addr:port if the port is other than default (typically TCP ports 860 and 3260).
        /// </summary>
        [YamlMember(Alias = "portals")]
        [JsonProperty("portals", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Portals { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Portals"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePortals() => Portals.Count > 0;

        /// <summary>
        ///     whether support iSCSI Discovery CHAP authentication
        /// </summary>
        [YamlMember(Alias = "chapAuthDiscovery")]
        [JsonProperty("chapAuthDiscovery", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ChapAuthDiscovery { get; set; }

        /// <summary>
        ///     ReadOnly here will force the ReadOnly setting in VolumeMounts. Defaults to false.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
