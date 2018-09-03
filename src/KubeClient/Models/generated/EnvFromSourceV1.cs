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
        [JsonProperty("configMapRef")]
        [YamlMember(Alias = "configMapRef")]
        public ConfigMapEnvSourceV1 ConfigMapRef { get; set; }

        /// <summary>
        ///     The Secret to select from
        /// </summary>
        [JsonProperty("secretRef")]
        [YamlMember(Alias = "secretRef")]
        public SecretEnvSourceV1 SecretRef { get; set; }

        /// <summary>
        ///     An optional identifier to prepend to each key in the ConfigMap. Must be a C_IDENTIFIER.
        /// </summary>
        [JsonProperty("prefix")]
        [YamlMember(Alias = "prefix")]
        public string Prefix { get; set; }
    }
}
