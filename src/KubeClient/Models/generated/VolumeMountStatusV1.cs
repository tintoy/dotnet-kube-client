using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeMountStatus shows status of volume mounts.
    /// </summary>
    public partial class VolumeMountStatusV1
    {
        /// <summary>
        ///     Name corresponds to the name of the original VolumeMount.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     MountPath corresponds to the original VolumeMount.
        /// </summary>
        [YamlMember(Alias = "mountPath")]
        [JsonProperty("mountPath", NullValueHandling = NullValueHandling.Include)]
        public string MountPath { get; set; }

        /// <summary>
        ///     ReadOnly corresponds to the original VolumeMount.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }

        /// <summary>
        ///     RecursiveReadOnly must be set to Disabled, Enabled, or unspecified (for non-readonly mounts). An IfPossible value in the original VolumeMount must be translated to Disabled or Enabled, depending on the mount result.
        /// </summary>
        [YamlMember(Alias = "recursiveReadOnly")]
        [JsonProperty("recursiveReadOnly", NullValueHandling = NullValueHandling.Ignore)]
        public string RecursiveReadOnly { get; set; }
    }
}
