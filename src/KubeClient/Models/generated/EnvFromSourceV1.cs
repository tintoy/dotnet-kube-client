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
        public virtual ConfigMapEnvSourceV1 ConfigMapRef { get; set; }

        /// <summary>
        ///     The Secret to select from
        /// </summary>
        [JsonProperty("secretRef")]
        [YamlMember(Alias = "secretRef")]
        public virtual SecretEnvSourceV1 SecretRef { get; set; }

        /// <summary>
        ///     An optional identifer to prepend to each key in the ConfigMap. Must be a C_IDENTIFIER.
        /// </summary>
        [JsonProperty("prefix")]
        [YamlMember(Alias = "prefix")]
        public virtual string Prefix { get; set; }
    }
}
