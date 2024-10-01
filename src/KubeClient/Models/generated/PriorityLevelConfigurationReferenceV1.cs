using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PriorityLevelConfigurationReference contains information that points to the "request-priority" being used.
    /// </summary>
    public partial class PriorityLevelConfigurationReferenceV1
    {
        /// <summary>
        ///     `name` is the name of the priority level configuration being referenced Required.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }
    }
}
