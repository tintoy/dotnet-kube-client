using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SecretKeySelector selects a key of a Secret.
    /// </summary>
    public partial class SecretKeySelectorV1
    {
        /// <summary>
        ///     Name of the referent. This field is effectively required, but due to backwards compatibility is allowed to be empty. Instances of this type with an empty value here are almost certainly wrong. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
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
        ///     The key of the secret to select from.  Must be a valid secret key.
        /// </summary>
        [YamlMember(Alias = "key")]
        [JsonProperty("key", NullValueHandling = NullValueHandling.Include)]
        public string Key { get; set; }
    }
}
