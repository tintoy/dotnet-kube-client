using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Adapts a Secret into a volume.
    ///     
    ///     The contents of the target Secret's Data field will be presented in a volume as files using the keys in the Data field as the file names. Secret volumes support ownership management and SELinux relabeling.
    /// </summary>
    [KubeListItem("KeyToPath", "v1")]
    public partial class SecretVolumeSourceV1 : Models.SecretVolumeSourceV1
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
        ///     Name of the secret in the pod's namespace to use. More info: https://kubernetes.io/docs/concepts/storage/volumes#secret
        /// </summary>
        [JsonProperty("secretName")]
        [YamlMember(Alias = "secretName")]
        public override string SecretName
        {
            get
            {
                return base.SecretName;
            }
            set
            {
                base.SecretName = value;

                __ModifiedProperties__.Add("SecretName");
            }
        }


        /// <summary>
        ///     Specify whether the Secret or it's keys must be defined
        /// </summary>
        [JsonProperty("optional")]
        [YamlMember(Alias = "optional")]
        public override bool Optional
        {
            get
            {
                return base.Optional;
            }
            set
            {
                base.Optional = value;

                __ModifiedProperties__.Add("Optional");
            }
        }


        /// <summary>
        ///     If unspecified, each key-value pair in the Data field of the referenced Secret will be projected into the volume as a file whose name is the key and content is the value. If specified, the listed keys will be projected into the specified paths, and unlisted keys will not be present. If a key is specified which is not present in the Secret, the volume setup will error unless it is marked optional. Paths must be relative and may not contain the '..' path or start with '..'.
        /// </summary>
        [YamlMember(Alias = "items")]
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.KeyToPathV1> Items { get; set; } = new List<Models.KeyToPathV1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
