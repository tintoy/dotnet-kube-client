using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     SecretKeySelector selects a key of a Secret.
    /// </summary>
    public partial class SecretKeySelectorV1
    {
        /// <summary>
        ///     Specify whether the Secret or it's key must be defined
        /// </summary>
        [JsonProperty("optional")]
        public bool Optional { get; set; }

        /// <summary>
        ///     The key of the secret to select from.  Must be a valid secret key.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
