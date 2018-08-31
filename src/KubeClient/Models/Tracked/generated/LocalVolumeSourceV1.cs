using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Local represents directly-attached storage with node affinity
    /// </summary>
    public partial class LocalVolumeSourceV1 : Models.LocalVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     The full path to the volume on the node For alpha, this path must be a directory Once block as a source is supported, then this path can point to a block device
        /// </summary>
        [JsonProperty("path")]
        [YamlMember(Alias = "path")]
        public override string Path
        {
            get
            {
                return base.Path;
            }
            set
            {
                base.Path = value;

                __ModifiedProperties__.Add("Path");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
