using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LinuxContainerUser represents user identity information in Linux containers
    /// </summary>
    public partial class LinuxContainerUserV1
    {
        /// <summary>
        ///     GID is the primary gid initially attached to the first process in the container
        /// </summary>
        [YamlMember(Alias = "gid")]
        [JsonProperty("gid", NullValueHandling = NullValueHandling.Include)]
        public long Gid { get; set; }

        /// <summary>
        ///     UID is the primary uid initially attached to the first process in the container
        /// </summary>
        [YamlMember(Alias = "uid")]
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Include)]
        public long Uid { get; set; }

        /// <summary>
        ///     SupplementalGroups are the supplemental groups initially attached to the first process in the container
        /// </summary>
        [YamlMember(Alias = "supplementalGroups")]
        [JsonProperty("supplementalGroups", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<long> SupplementalGroups { get; } = new List<long>();

        /// <summary>
        ///     Determine whether the <see cref="SupplementalGroups"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSupplementalGroups() => SupplementalGroups.Count > 0;
    }
}
