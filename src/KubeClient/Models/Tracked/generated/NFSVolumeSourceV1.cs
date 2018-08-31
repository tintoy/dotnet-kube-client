using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents an NFS mount that lasts the lifetime of a pod. NFS volumes do not support ownership management or SELinux relabeling.
    /// </summary>
    public partial class NFSVolumeSourceV1 : Models.NFSVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     Path that is exported by the NFS server. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [JsonProperty("path")]
        [YamlMember(Alias = "path")]
        public override string Path
        {
            get
            {
                return base.Path;
            }
            set
            {
                base.Path = value;

                __ModifiedProperties__.Add("Path");
            }
        }


        /// <summary>
        ///     Server is the hostname or IP address of the NFS server. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
        /// </summary>
        [JsonProperty("server")]
        [YamlMember(Alias = "server")]
        public override string Server
        {
            get
            {
                return base.Server;
            }
            set
            {
                base.Server = value;

                __ModifiedProperties__.Add("Server");
            }
        }


        /// <summary>
        ///     ReadOnly here will force the NFS export to be mounted with read-only permissions. Defaults to false. More info: https://kubernetes.io/docs/concepts/storage/volumes#nfs
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
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
