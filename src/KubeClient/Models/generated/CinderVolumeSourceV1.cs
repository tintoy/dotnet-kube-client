using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a cinder volume resource in Openstack. A Cinder volume must exist before mounting to a container. The volume must also be in the same region as the kubelet. Cinder volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class CinderVolumeSourceV1
    {
        /// <summary>
        ///     volumeID used to identify the volume in cinder. More info: https://examples.k8s.io/mysql-cinder-pd/README.md
        /// </summary>
        [YamlMember(Alias = "volumeID")]
        [JsonProperty("volumeID", NullValueHandling = NullValueHandling.Include)]
        public string VolumeID { get; set; }

        /// <summary>
        ///     fsType is the filesystem type to mount. Must be a filesystem type supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://examples.k8s.io/mysql-cinder-pd/README.md
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     secretRef is optional: points to a secret object containing parameters used to connect to OpenStack.
        /// </summary>
        [YamlMember(Alias = "secretRef")]
        [JsonProperty("secretRef", NullValueHandling = NullValueHandling.Ignore)]
        public LocalObjectReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     readOnly defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts. More info: https://examples.k8s.io/mysql-cinder-pd/README.md
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
