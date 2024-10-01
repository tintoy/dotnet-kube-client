using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents a projected volume source
    /// </summary>
    public partial class ProjectedVolumeSourceV1
    {
        /// <summary>
        ///     defaultMode are the mode bits used to set permissions on created files by default. Must be an octal value between 0000 and 0777 or a decimal value between 0 and 511. YAML accepts both octal and decimal values, JSON requires decimal values for mode bits. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
        /// </summary>
        [YamlMember(Alias = "defaultMode")]
        [JsonProperty("defaultMode", NullValueHandling = NullValueHandling.Ignore)]
        public int? DefaultMode { get; set; }

        /// <summary>
        ///     sources is the list of volume projections. Each entry in this list handles one source.
        /// </summary>
        [YamlMember(Alias = "sources")]
        [JsonProperty("sources", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<VolumeProjectionV1> Sources { get; } = new List<VolumeProjectionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Sources"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSources() => Sources.Count > 0;
    }
}
