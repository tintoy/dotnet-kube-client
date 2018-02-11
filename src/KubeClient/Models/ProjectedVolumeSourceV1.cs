using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a projected volume source
    /// </summary>
    [KubeResource("ProjectedVolumeSource", "v1")]
    public class ProjectedVolumeSourceV1
    {
        /// <summary>
        ///     Mode bits to use on created files by default. Must be a value between 0 and 0777. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
        /// </summary>
        [JsonProperty("defaultMode")]
        public int DefaultMode { get; set; }

        /// <summary>
        ///     list of volume projections
        /// </summary>
        [JsonProperty("sources", NullValueHandling = NullValueHandling.Ignore)]
        public List<VolumeProjectionV1> Sources { get; set; } = new List<VolumeProjectionV1>();
    }
}
