using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodDNSConfigOption defines DNS resolver options of a pod.
    /// </summary>
    public partial class PodDNSConfigOptionV1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("value")]
        [YamlMember(Alias = "value")]
        public string Value { get; set; }

        /// <summary>
        ///     Required.
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public string Name { get; set; }
    }
}
