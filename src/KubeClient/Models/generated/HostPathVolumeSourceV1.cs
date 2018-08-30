using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a host path mapped into a pod. Host path volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    public partial class HostPathVolumeSourceV1
    {
        /// <summary>
        ///     The volume type. Can be one of ["File", "Directory", "FileOrCreate", "DirectoryOrCreate", "Socket", "CharDevice", "BlockDevice"].
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public virtual string Type { get; set; }

        /// <summary>
        ///     Path of the directory on the host. More info: https://kubernetes.io/docs/concepts/storage/volumes#hostpath
        /// </summary>
        [JsonProperty("path")]
        [YamlMember(Alias = "path")]
        public virtual string Path { get; set; }
    }
}
