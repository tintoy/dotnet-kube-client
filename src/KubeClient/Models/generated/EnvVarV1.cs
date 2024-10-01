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
        ///     Variable references $(VAR_NAME) are expanded using the previously defined environment variables in the container and any service environment variables. If a variable cannot be resolved, the reference in the input string will be unchanged. Double $$ are reduced to a single $, which allows for escaping the $(VAR_NAME) syntax: i.e. "$$(VAR_NAME)" will produce the string literal "$(VAR_NAME)". Escaped references will never be expanded, regardless of whether the variable exists or not. Defaults to "".
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
