using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     SecretKeySelector selects a key of a Secret.
    /// </summary>
    public partial class SecretKeySelectorV1 : Models.SecretKeySelectorV1
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
        ///     Specify whether the Secret or it's key must be defined
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
        ///     The key of the secret to select from.  Must be a valid secret key.
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
