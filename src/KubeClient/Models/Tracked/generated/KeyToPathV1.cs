using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Maps a string key to a path within a volume.
    /// </summary>
    public partial class KeyToPathV1 : Models.KeyToPathV1
    {
        /// <summary>
        ///     Optional: mode bits to use on this file, must be a value between 0 and 0777. If not specified, the volume defaultMode will be used. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
        /// </summary>
        [JsonProperty("mode")]
        [YamlMember(Alias = "mode")]
        public override int? Mode
        {
            get
            {
                return base.Mode;
            }
            set
            {
                base.Mode = value;

                __ModifiedProperties__.Add("Mode");
            }
        }


        /// <summary>
        ///     The relative path of the file to map the key to. May not be an absolute path. May not contain the path element '..'. May not start with the string '..'.
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
        ///     The key to project.
        /// </summary>
        [JsonProperty("key")]
        [YamlMember(Alias = "key")]
        public override string Key
        {
            get
            {
                return base.Key;
            }
            set
            {
                base.Key = value;

                __ModifiedProperties__.Add("Key");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
