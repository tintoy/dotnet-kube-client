using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents downward API info for projecting into a projected volume. Note that this is identical to a downwardAPI volume source without the default mode.
    /// </summary>
    [KubeListItem("DownwardAPIVolumeFile", "v1")]
    public partial class DownwardAPIProjectionV1
    {
        /// <summary>
        ///     Items is a list of DownwardAPIVolume file
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
