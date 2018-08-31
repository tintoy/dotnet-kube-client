using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ConfigMap holds configuration data for pods to consume.
    /// </summary>
    [KubeObject("ConfigMap", "v1")]
    public partial class ConfigMapV1 : Models.ConfigMapV1, ITracked
    {
        /// <summary>
        ///     Data contains the configuration data. Each key must consist of alphanumeric characters, '-', '_' or '.'.
        /// </summary>
        [YamlMember(Alias = "data")]
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
