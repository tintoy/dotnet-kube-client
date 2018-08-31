using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a volume that is populated with the contents of a git repository. Git repo volumes do not support ownership management. Git repo volumes support SELinux relabeling.
    /// </summary>
    public partial class GitRepoVolumeSourceV1 : Models.GitRepoVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     Commit hash for the specified revision.
        /// </summary>
        [JsonProperty("revision")]
        [YamlMember(Alias = "revision")]
        public override string Revision
        {
            get
            {
                return base.Revision;
            }
            set
            {
                base.Revision = value;

                __ModifiedProperties__.Add("Revision");
            }
        }


        /// <summary>
        ///     Target directory name. Must not contain or start with '..'.  If '.' is supplied, the volume directory will be the git repository.  Otherwise, if specified, the volume will contain the git repository in the subdirectory with the given name.
        /// </summary>
        [JsonProperty("directory")]
        [YamlMember(Alias = "directory")]
        public override string Directory
        {
            get
            {
                return base.Directory;
            }
            set
            {
                base.Directory = value;

                __ModifiedProperties__.Add("Directory");
            }
        }


        /// <summary>
        ///     Repository URL
        /// </summary>
        [JsonProperty("repository")]
        [YamlMember(Alias = "repository")]
        public override string Repository
        {
            get
            {
                return base.Repository;
            }
            set
            {
                base.Repository = value;

                __ModifiedProperties__.Add("Repository");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
