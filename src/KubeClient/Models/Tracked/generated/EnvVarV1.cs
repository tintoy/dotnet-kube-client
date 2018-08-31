using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     EnvVar represents an environment variable present in a Container.
    /// </summary>
    public partial class EnvVarV1 : Models.EnvVarV1, ITracked
    {
        /// <summary>
        ///     Name of the environment variable. Must be a C_IDENTIFIER.
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
        ///     Variable references $(VAR_NAME) are expanded using the previous defined environment variables in the container and any service environment variables. If a variable cannot be resolved, the reference in the input string will be unchanged. The $(VAR_NAME) syntax can be escaped with a double $$, ie: $$(VAR_NAME). Escaped references will never be expanded, regardless of whether the variable exists or not. Defaults to "".
        /// </summary>
        [JsonProperty("value")]
        [YamlMember(Alias = "value")]
        public override string Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;

                __ModifiedProperties__.Add("Value");
            }
        }


        /// <summary>
        ///     Source for the environment variable's value. Cannot be used if value is not empty.
        /// </summary>
        [JsonProperty("valueFrom")]
        [YamlMember(Alias = "valueFrom")]
        public override Models.EnvVarSourceV1 ValueFrom
        {
            get
            {
                return base.ValueFrom;
            }
            set
            {
                base.ValueFrom = value;

                __ModifiedProperties__.Add("ValueFrom");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
