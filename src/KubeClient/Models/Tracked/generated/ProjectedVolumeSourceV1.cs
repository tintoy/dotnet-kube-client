using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents a projected volume source
    /// </summary>
    public partial class ProjectedVolumeSourceV1 : Models.ProjectedVolumeSourceV1
    {
        /// <summary>
        ///     Mode bits to use on created files by default. Must be a value between 0 and 0777. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
        /// </summary>
        [JsonProperty("defaultMode")]
        [YamlMember(Alias = "defaultMode")]
        public override int DefaultMode
        {
            get
            {
                return base.DefaultMode;
            }
            set
            {
                base.DefaultMode = value;

                __ModifiedProperties__.Add("DefaultMode");
            }
        }


        /// <summary>
        ///     list of volume projections
        /// </summary>
        [YamlMember(Alias = "sources")]
        [JsonProperty("sources", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.VolumeProjectionV1> Sources { get; set; } = new List<Models.VolumeProjectionV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
