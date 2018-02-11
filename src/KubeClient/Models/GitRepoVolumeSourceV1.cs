using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a volume that is populated with the contents of a git repository. Git repo volumes do not support ownership management. Git repo volumes support SELinux relabeling.
    /// </summary>
    [KubeResource("GitRepoVolumeSource", "v1")]
    public class GitRepoVolumeSourceV1
    {
        /// <summary>
        ///     Commit hash for the specified revision.
        /// </summary>
        [JsonProperty("revision")]
        public string Revision { get; set; }

        /// <summary>
        ///     Target directory name. Must not contain or start with '..'.  If '.' is supplied, the volume directory will be the git repository.  Otherwise, if specified, the volume will contain the git repository in the subdirectory with the given name.
        /// </summary>
        [JsonProperty("directory")]
        public string Directory { get; set; }

        /// <summary>
        ///     Repository URL
        /// </summary>
        [JsonProperty("repository")]
        public string Repository { get; set; }
    }
}
