using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a Ceph Filesystem mount that lasts the lifetime of a pod Cephfs volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    public partial class CephFSVolumeSourceV1
    {
        /// <summary>
        ///     Optional: SecretFile is the path to key ring for User, default is /etc/ceph/user.secret More info: https://releases.k8s.io/HEAD/examples/volumes/cephfs/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "secretFile")]
        [JsonProperty("secretFile", NullValueHandling = NullValueHandling.Ignore)]
        public string SecretFile { get; set; }

        /// <summary>
        ///     Optional: SecretRef is reference to the authentication secret for User, default is empty. More info: https://releases.k8s.io/HEAD/examples/volumes/cephfs/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "secretRef")]
        [JsonProperty("secretRef", NullValueHandling = NullValueHandling.Ignore)]
        public LocalObjectReferenceV1 SecretRef { get; set; }

        /// <summary>
        ///     Optional: Used as the mounted root, rather than the full Ceph tree, default is /
        /// </summary>
        [YamlMember(Alias = "path")]
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        /// <summary>
        ///     Optional: User is the rados user name, default is admin More info: https://releases.k8s.io/HEAD/examples/volumes/cephfs/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "user")]
        [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }

        /// <summary>
        ///     Required: Monitors is a collection of Ceph monitors More info: https://releases.k8s.io/HEAD/examples/volumes/cephfs/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "monitors")]
        [JsonProperty("monitors", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Monitors { get; } = new List<string>();

        /// <summary>
        ///     Optional: Defaults to false (read/write). ReadOnly here will force the ReadOnly setting in VolumeMounts. More info: https://releases.k8s.io/HEAD/examples/volumes/cephfs/README.md#how-to-use-it
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
