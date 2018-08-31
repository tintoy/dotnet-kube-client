using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents downward API info for projecting into a projected volume. Note that this is identical to a downwardAPI volume source without the default mode.
    /// </summary>
    [KubeListItem("DownwardAPIVolumeFile", "v1")]
    public partial class DownwardAPIProjectionV1 : Models.DownwardAPIProjectionV1, ITracked
    {
        /// <summary>
        ///     Items is a list of DownwardAPIVolume file
        /// </summary>
        [YamlMember(Alias = "items")]
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.DownwardAPIVolumeFileV1> Items { get; set; } = new List<Models.DownwardAPIVolumeFileV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
