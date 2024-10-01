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
        ///     mountPropagation determines how mounts are propagated from the host to container and the other way around. When not set, MountPropagationNone is used. This field is beta in 1.10. When RecursiveReadOnly is set to IfPossible or to Enabled, MountPropagation must be None or unspecified (which defaults to None).
        /// </summary>
        [YamlMember(Alias = "mountPropagation")]
        [JsonProperty("mountPropagation", NullValueHandling = NullValueHandling.Ignore)]
        public string MountPropagation { get; set; }

        /// <summary>
        ///     Expanded path within the volume from which the container's volume should be mounted. Behaves similarly to SubPath but environment variable references $(VAR_NAME) are expanded using the container's environment. Defaults to "" (volume's root). SubPathExpr and SubPath are mutually exclusive.
        /// </summary>
        [YamlMember(Alias = "subPathExpr")]
        [JsonProperty("subPathExpr", NullValueHandling = NullValueHandling.Ignore)]
        public string SubPathExpr { get; set; }

        /// <summary>
        ///     Mounted read-only if true, read-write otherwise (false or unspecified). Defaults to false.
        /// </summary>
        [YamlMember(Alias = "readOnly")]
        [JsonProperty("readOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ReadOnly { get; set; }

        /// <summary>
        ///     RecursiveReadOnly specifies whether read-only mounts should be handled recursively.
        ///     
        ///     If ReadOnly is false, this field has no meaning and must be unspecified.
        ///     
        ///     If ReadOnly is true, and this field is set to Disabled, the mount is not made recursively read-only.  If this field is set to IfPossible, the mount is made recursively read-only, if it is supported by the container runtime.  If this field is set to Enabled, the mount is made recursively read-only if it is supported by the container runtime, otherwise the pod will not be started and an error will be generated to indicate the reason.
        ///     
        ///     If this field is set to IfPossible or Enabled, MountPropagation must be set to None (or be unspecified, which defaults to None).
        ///     
        ///     If this field is not specified, it is treated as an equivalent of Disabled.
        /// </summary>
        [YamlMember(Alias = "recursiveReadOnly")]
        [JsonProperty("recursiveReadOnly", NullValueHandling = NullValueHandling.Ignore)]
        public string RecursiveReadOnly { get; set; }
    }
}
