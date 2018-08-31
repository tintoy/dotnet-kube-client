using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DownwardAPIVolumeSource represents a volume containing downward API info. Downward API volumes support ownership management and SELinux relabeling.
    /// </summary>
    [KubeListItem("DownwardAPIVolumeFile", "v1")]
    public partial class DownwardAPIVolumeSourceV1 : Models.DownwardAPIVolumeSourceV1
    {
        /// <summary>
        ///     Optional: mode bits to use on created files by default. Must be a value between 0 and 0777. Defaults to 0644. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
        /// </summary>
        [JsonProperty("defaultMode")]
        [YamlMember(Alias = "defaultMode")]
        public override int? DefaultMode
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
        ///     Items is a list of downward API volume file
        /// </summary>
        [YamlMember(Alias = "items")]
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.DownwardAPIVolumeFileV1> Items { get; set; } = new List<Models.DownwardAPIVolumeFileV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
