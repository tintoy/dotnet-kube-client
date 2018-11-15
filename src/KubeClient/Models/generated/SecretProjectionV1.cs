using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Adapts a secret into a projected volume.
    ///     
    ///     The contents of the target Secret's Data field will be presented in a projected volume as files using the keys in the Data field as the file names. Note that this is identical to a secret volume source without the default mode.
    /// </summary>
    [KubeListItem("KeyToPath", "v1")]
    public partial class SecretProjectionV1
    {
        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     Specify whether the Secret or its key must be defined
        /// </summary>
        [YamlMember(Alias = "optional")]
        [JsonProperty("optional", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Optional { get; set; }

        /// <summary>
        ///     If unspecified, each key-value pair in the Data field of the referenced Secret will be projected into the volume as a file whose name is the key and content is the value. If specified, the listed keys will be projected into the specified paths, and unlisted keys will not be present. If a key is specified which is not present in the Secret, the volume setup will error unless it is marked optional. Paths must be relative and may not contain the '..' path or start with '..'.
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
