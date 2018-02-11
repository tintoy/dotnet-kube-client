using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     SecretEnvSource selects a Secret to populate the environment variables with.
    ///     
    ///     The contents of the target Secret's Data field will represent the key-value pairs as environment variables.
    /// </summary>
    [KubeResource("SecretEnvSource", "v1")]
    public class SecretEnvSourceV1
    {
        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Specify whether the Secret must be defined
        /// </summary>
        [JsonProperty("optional")]
        public bool Optional { get; set; }
    }
}
