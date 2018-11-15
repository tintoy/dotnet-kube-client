using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EnvFromSource represents the source of a set of ConfigMaps
    /// </summary>
    public partial class EnvFromSourceV1
    {
        /// <summary>
        ///     The ConfigMap to select from
        /// </summary>
        [YamlMember(Alias = "configMapRef")]
        [JsonProperty("configMapRef", NullValueHandling = NullValueHandling.Ignore)]
        public ConfigMapEnvSourceV1 ConfigMapRef { get; set; }

        /// <summary>
        ///     The Secret to select from
        /// </summary>
        [YamlMember(Alias = "secretRef")]
        [JsonProperty("secretRef", NullValueHandling = NullValueHandling.Ignore)]
        public SecretEnvSourceV1 SecretRef { get; set; }

        /// <summary>
        ///     An optional identifier to prepend to each key in the ConfigMap. Must be a C_IDENTIFIER.
        /// </summary>
        [YamlMember(Alias = "prefix")]
        [JsonProperty("prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string Prefix { get; set; }
    }
}
