using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ConfigMapEnvSource selects a ConfigMap to populate the environment variables with.
    ///     
    ///     The contents of the target ConfigMap's Data field will represent the key-value pairs as environment variables.
    /// </summary>
    public partial class ConfigMapEnvSourceV1 : Models.ConfigMapEnvSourceV1, ITracked
    {
        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;

                __ModifiedProperties__.Add("Name");
            }
        }


        /// <summary>
        ///     Specify whether the ConfigMap must be defined
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
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
