using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a Rados Block Device mount that lasts the lifetime of a pod. RBD volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class RBDVolumeSourceV1
    {
        /// <summary>
        ///     fsType is the filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#rbd
        /// </summary>
        [YamlMember(Alias = "fsType")]
        [JsonProperty("fsType", NullValueHandling = NullValueHandling.Ignore)]
        public string FsType { get; set; }

        /// <summary>
        ///     image is the rados image name. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "image")]
        [JsonProperty("image", NullValueHandling = NullValueHandling.Include)]
        public string Image { get; set; }

        /// <summary>
        ///     secretRef is name of the authentication secret for RBDUser. If provided overrides keyring. Default is nil. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "secretRef")]
        [JsonProperty("secretRef", NullValueHandling = NullValueHandling.Ignore)]
        public LocalObjectReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     keyring is the path to key ring for RBDUser. Default is /etc/ceph/keyring. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "keyring")]
        [JsonProperty("keyring", NullValueHandling = NullValueHandling.Ignore)]
        public string Keyring { get; set; }

        /// <summary>
        ///     pool is the rados pool name. Default is rbd. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "pool")]
        [JsonProperty("pool", NullValueHandling = NullValueHandling.Ignore)]
        public string Pool { get; set; }

        /// <summary>
        ///     user is the rados user name. Default is admin. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "user")]
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }

        /// <summary>
        ///     monitors is a collection of Ceph monitors. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "monitors")]
        [JsonProperty("monitors", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Monitors { get; } = new List<string>();

        /// <summary>
        ///     readOnly here will force the ReadOnly setting in VolumeMounts. Defaults to false. More info: https://examples.k8s.io/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
