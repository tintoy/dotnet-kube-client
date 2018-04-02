using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents downward API info for projecting into a projected volume. Note that this is identical to a downwardAPI volume source without the default mode.
    /// </summary>
    [KubeListItem("DownwardAPIVolumeFile", "v1")]
    [KubeObject("DownwardAPIProjection", "v1")]
    public partial class DownwardAPIProjectionV1
    {
        /// <summary>
        ///     Items is a list of DownwardAPIVolume file
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<DownwardAPIVolumeFileV1> Items { get; set; } = new List<DownwardAPIVolumeFileV1>();
    }
}
