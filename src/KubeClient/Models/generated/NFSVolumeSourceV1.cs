using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents an NFS mount that lasts the lifetime of a pod. NFS volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    public partial class NFSVolumeSourceV1
    {
        /// <summary>
        ///     Path that is exported by the NFS server. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [YamlMember(Alias = "path")]
        [JsonProperty("path", NullValueHandling = NullValueHandling.Include)]
        public string Path { get; set; }

        /// <summary>
        ///     Server is the hostname or IP address of the NFS server. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [YamlMember(Alias = "server")]
        [JsonProperty("server", NullValueHandling = NullValueHandling.Include)]
        public string Server { get; set; }

        /// <summary>
        ///     ReadOnly here will force the NFS export to be mounted with read-only permissions. Defaults to false. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
