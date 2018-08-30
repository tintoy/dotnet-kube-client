using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SecretEnvSource selects a Secret to populate the environment variables with.
    ///     
    ///     The contents of the target Secret's Data field will represent the key-value pairs as environment variables.
    /// </summary>
    public partial class SecretEnvSourceV1
    {
        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Specify whether the Secret must be defined
        /// </summary>
        [JsonProperty("optional")]
        [YamlMember(Alias = "optional")]
        public virtual bool Optional { get; set; }
    }
}
