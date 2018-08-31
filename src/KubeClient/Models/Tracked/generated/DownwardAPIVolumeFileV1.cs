using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DownwardAPIVolumeFile represents information to create the file containing the pod field
    /// </summary>
    public partial class DownwardAPIVolumeFileV1 : Models.DownwardAPIVolumeFileV1, ITracked
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
        ///     Required: Selects a field of the pod: only annotations, labels, name and namespace are supported.
        /// </summary>
        [JsonProperty("fieldRef")]
        [YamlMember(Alias = "fieldRef")]
        public override Models.ObjectFieldSelectorV1 FieldRef
        {
            get
            {
                return base.FieldRef;
            }
            set
            {
                base.FieldRef = value;

                __ModifiedProperties__.Add("FieldRef");
            }
        }


        /// <summary>
        ///     Selects a resource of the container: only resources limits and requests (limits.cpu, limits.memory, requests.cpu and requests.memory) are currently supported.
        /// </summary>
        [JsonProperty("resourceFieldRef")]
        [YamlMember(Alias = "resourceFieldRef")]
        public override Models.ResourceFieldSelectorV1 ResourceFieldRef
        {
            get
            {
                return base.ResourceFieldRef;
            }
            set
            {
                base.ResourceFieldRef = value;

                __ModifiedProperties__.Add("ResourceFieldRef");
            }
        }


        /// <summary>
        ///     Required: Path is  the relative path name of the file to be created. Must not be absolute or contain the '..' path. Must be utf-8 encoded. The first item of the relative path must not start with '..'
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
