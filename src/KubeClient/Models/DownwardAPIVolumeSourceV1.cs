using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     DownwardAPIVolumeSource represents a volume containing downward API info. Downward API volumes support ownership management and SELinux relabeling.
    /// </summary>
    [KubeListItem("DownwardAPIVolumeFile", "v1")]
    [KubeObject("DownwardAPIVolumeSource", "v1")]
    public partial class DownwardAPIVolumeSourceV1
    {
        /// <summary>
        ///     Optional: mode bits to use on created files by default. Must be a value between 0 and 0777. Defaults to 0644. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
        /// </summary>
        [JsonProperty("defaultMode")]
        public int? DefaultMode { get; set; }

        /// <summary>
        ///     Items is a list of downward API volume file
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<DownwardAPIVolumeFileV1> Items { get; set; } = new List<DownwardAPIVolumeFileV1>();
    }
}
