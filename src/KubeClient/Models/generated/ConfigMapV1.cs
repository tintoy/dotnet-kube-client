using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ConfigMap holds configuration data for pods to consume.
    /// </summary>
    [KubeObject("ConfigMap", "v1")]
    public partial class ConfigMapV1 : KubeResourceV1
    {
        /// <summary>
        ///     Data contains the configuration data. Each key must consist of alphanumeric characters, '-', '_' or '.'.
        /// </summary>
        [YamlMember(Alias = "data")]
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
    }
}
