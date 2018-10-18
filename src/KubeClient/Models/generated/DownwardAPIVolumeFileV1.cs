using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DownwardAPIVolumeFile represents information to create the file containing the pod field
    /// </summary>
    public partial class DownwardAPIVolumeFileV1
    {
        /// <summary>
        ///     Optional: mode bits to use on this file, must be a value between 0 and 0777. If not specified, the volume defaultMode will be used. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
        /// </summary>
        [YamlMember(Alias = "mode")]
        [JsonProperty("mode", NullValueHandling = NullValueHandling.Ignore)]
        public int? Mode { get; set; }

        /// <summary>
        ///     Required: Selects a field of the pod: only annotations, labels, name and namespace are supported.
        /// </summary>
        [YamlMember(Alias = "fieldRef")]
        [JsonProperty("fieldRef", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectFieldSelectorV1 FieldRef { get; set; }

        /// <summary>
        ///     Selects a resource of the container: only resources limits and requests (limits.cpu, limits.memory, requests.cpu and requests.memory) are currently supported.
        /// </summary>
        [YamlMember(Alias = "resourceFieldRef")]
        [JsonProperty("resourceFieldRef", NullValueHandling = NullValueHandling.Ignore)]
        public ResourceFieldSelectorV1 ResourceFieldRef { get; set; }

        /// <summary>
        ///     Required: Path is  the relative path name of the file to be created. Must not be absolute or contain the '..' path. Must be utf-8 encoded. The first item of the relative path must not start with '..'
        /// </summary>
        [YamlMember(Alias = "path")]
        [JsonProperty("path", NullValueHandling = NullValueHandling.Include)]
        public string Path { get; set; }
    }
}
