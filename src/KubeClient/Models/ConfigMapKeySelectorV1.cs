using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Selects a key from a ConfigMap.
    /// </summary>
    [KubeObject("ConfigMapKeySelector", "v1")]
    public partial class ConfigMapKeySelectorV1
    {
        /// <summary>
        ///     Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Specify whether the ConfigMap or it's key must be defined
        /// </summary>
        [JsonProperty("optional")]
        public bool Optional { get; set; }

        /// <summary>
        ///     The key to select.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
