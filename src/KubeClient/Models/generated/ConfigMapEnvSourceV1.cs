using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ConfigMapEnvSource selects a ConfigMap to populate the environment variables with.
    ///     
    ///     The contents of the target ConfigMap's Data field will represent the key-value pairs as environment variables.
    /// </summary>
    public partial class ConfigMapEnvSourceV1
    {
        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     Specify whether the ConfigMap must be defined
        /// </summary>
        [YamlMember(Alias = "optional")]
        [JsonProperty("optional", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Optional { get; set; }
    }
}
