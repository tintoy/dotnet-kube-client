using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EnvVar represents an environment variable present in a Container.
    /// </summary>
    public partial class EnvVarV1
    {
        /// <summary>
        ///     Name of the environment variable. Must be a C_IDENTIFIER.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Variable references $(VAR_NAME) are expanded using the previous defined environment variables in the container and any service environment variables. If a variable cannot be resolved, the reference in the input string will be unchanged. The $(VAR_NAME) syntax can be escaped with a double $$, ie: $$(VAR_NAME). Escaped references will never be expanded, regardless of whether the variable exists or not. Defaults to "".
        /// </summary>
        [YamlMember(Alias = "value")]
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        ///     Source for the environment variable's value. Cannot be used if value is not empty.
        /// </summary>
        [YamlMember(Alias = "valueFrom")]
        [JsonProperty("valueFrom", NullValueHandling = NullValueHandling.Ignore)]
        public EnvVarSourceV1 ValueFrom { get; set; }
    }
}
