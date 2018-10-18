using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeMount describes a mounting of a Volume within a container.
    /// </summary>
    public partial class VolumeMountV1
    {
        /// <summary>
        ///     This must match the Name of a Volume.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Path within the container at which the volume should be mounted.  Must not contain ':'.
        /// </summary>
        [YamlMember(Alias = "mountPath")]
        [JsonProperty("mountPath", NullValueHandling = NullValueHandling.Include)]
        public string MountPath { get; set; }

        /// <summary>
        ///     Path within the volume from which the container's volume should be mounted. Defaults to "" (volume's root).
        /// </summary>
        [YamlMember(Alias = "subPath")]
        [JsonProperty("subPath", NullValueHandling = NullValueHandling.Ignore)]
        public string SubPath { get; set; }

        /// <summary>
        ///     mountPropagation determines how mounts are propagated from the host to container and the other way around. When not set, MountPropagationNone is used. This field is beta in 1.10.
        /// </summary>
        [YamlMember(Alias = "mountPropagation")]
        [JsonProperty("mountPropagation", NullValueHandling = NullValueHandling.Ignore)]
        public string MountPropagation { get; set; }

        /// <summary>
        ///     Mounted read-only if true, read-write otherwise (false or unspecified). Defaults to false.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }
    }
}
