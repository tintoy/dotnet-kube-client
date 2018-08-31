using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Selects a key from a ConfigMap.
    /// </summary>
    public partial class ConfigMapKeySelectorV1 : Models.ConfigMapKeySelectorV1, ITracked
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
        ///     Specify whether the ConfigMap or it's key must be defined
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
        ///     The key to select.
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
