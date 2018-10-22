using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Adapts a ConfigMap into a volume.
    ///     
    ///     The contents of the target ConfigMap's Data field will be presented in a volume as files using the keys in the Data field as the file names, unless the items element is populated with specific mappings of keys to paths. ConfigMap volumes support ownership management and SELinux relabeling.
    /// </summary>
    [KubeListItem("KeyToPath", "v1")]
    public partial class ConfigMapVolumeSourceV1
    {
        /// <summary>
        ///     Optional: mode bits to use on created files by default. Must be a value between 0 and 0777. Defaults to 0644. Directories within the path are not affected by this setting. This might be in conflict with other options that affect the file mode, like fsGroup, and the result can be other mode bits set.
        /// </summary>
        [YamlMember(Alias = "defaultMode")]
        [JsonProperty("defaultMode", NullValueHandling = NullValueHandling.Ignore)]
        public int? DefaultMode { get; set; }

        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     Specify whether the ConfigMap or it's keys must be defined
        /// </summary>
        [YamlMember(Alias = "optional")]
        [JsonProperty("optional", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Optional { get; set; }

        /// <summary>
        ///     If unspecified, each key-value pair in the Data field of the referenced ConfigMap will be projected into the volume as a file whose name is the key and content is the value. If specified, the listed keys will be projected into the specified paths, and unlisted keys will not be present. If a key is specified which is not present in the ConfigMap, the volume setup will error unless it is marked optional. Paths must be relative and may not contain the '..' path or start with '..'.
        /// </summary>
        [YamlMember(Alias = "items")]
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<KeyToPathV1> Items { get; } = new List<KeyToPathV1>();

        /// <summary>
        ///     Determine whether the <see cref="Items"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeItems() => Items.Count > 0;
    }
}
