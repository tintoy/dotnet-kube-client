using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSchedulingGate is associated to a Pod to guard its scheduling.
    /// </summary>
    public partial class PodSchedulingGateV1
    {
        /// <summary>
        ///     Name of the scheduling gate. Each scheduling gate must have a unique name field.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }
    }
}
