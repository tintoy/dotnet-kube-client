using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DownwardAPIVolumeSource represents a volume containing downward API info. Downward API volumes support ownership management and SELinux relabeling.
    /// </summary>
    [KubeListItem("DownwardAPIVolumeFile", "v1")]
    public partial class DownwardAPIVolumeSourceV1
    {
        /// <summary>
        ///     Optional: mode bits to use on created files by default. Must be a value between 0 and 0777. Defaults to 0644. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
        /// </summary>
        [YamlMember(Alias = "defaultMode")]
        [JsonProperty("defaultMode", NullValueHandling = NullValueHandling.Ignore)]
        public int? DefaultMode { get; set; }

        /// <summary>
        ///     Items is a list of downward API volume file
        /// </summary>
        [YamlMember(Alias = "items")]
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DownwardAPIVolumeFileV1> Items { get; } = new List<DownwardAPIVolumeFileV1>();

        /// <summary>
        ///     Determine whether the <see cref="Items"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeItems() => Items.Count > 0;
    }
}
