using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a Rados Block Device mount that lasts the lifetime of a pod. RBD volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class RBDVolumeSourceV1 : Models.RBDVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     Filesystem type of the volume that you want to mount. Tip: Ensure that the filesystem type is supported by the host operating system. Examples: "ext4", "xfs", "ntfs". Implicitly inferred to be "ext4" if unspecified. More info: https://kubernetes.io/docs/concepts/storage/volumes#rbd
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
        ///     The rados image name. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [JsonProperty("image")]
        [YamlMember(Alias = "image")]
        public override string Image
        {
            get
            {
                return base.Image;
            }
            set
            {
                base.Image = value;

                __ModifiedProperties__.Add("Image");
            }
        }


        /// <summary>
        ///     SecretRef is name of the authentication secret for RBDUser. If provided overrides keyring. Default is nil. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md#how-to-use-it
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
        ///     Keyring is the path to key ring for RBDUser. Default is /etc/ceph/keyring. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [JsonProperty("keyring")]
        [YamlMember(Alias = "keyring")]
        public override string Keyring
        {
            get
            {
                return base.Keyring;
            }
            set
            {
                base.Keyring = value;

                __ModifiedProperties__.Add("Keyring");
            }
        }


        /// <summary>
        ///     The rados pool name. Default is rbd. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [JsonProperty("pool")]
        [YamlMember(Alias = "pool")]
        public override string Pool
        {
            get
            {
                return base.Pool;
            }
            set
            {
                base.Pool = value;

                __ModifiedProperties__.Add("Pool");
            }
        }


        /// <summary>
        ///     The rados user name. Default is admin. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [JsonProperty("user")]
        [YamlMember(Alias = "user")]
        public override string User
        {
            get
            {
                return base.User;
            }
            set
            {
                base.User = value;

                __ModifiedProperties__.Add("User");
            }
        }


        /// <summary>
        ///     A collection of Ceph monitors. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "monitors")]
        [JsonProperty("monitors", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Monitors { get; set; } = new List<string>();

        /// <summary>
        ///     ReadOnly here will force the ReadOnly setting in VolumeMounts. Defaults to false. More info: https://releases.k8s.io/HEAD/examples/volumes/rbd/README.md#how-to-use-it
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
