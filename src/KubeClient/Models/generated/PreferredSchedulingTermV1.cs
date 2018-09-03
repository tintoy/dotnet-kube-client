using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     An empty preferred scheduling term matches all objects with implicit weight 0 (i.e. it's a no-op). A null preferred scheduling term matches no objects (i.e. is also a no-op).
    /// </summary>
    public partial class PreferredSchedulingTermV1
    {
        /// <summary>
        ///     Weight associated with matching the corresponding nodeSelectorTerm, in the range 1-100.
        /// </summary>
        [JsonProperty("weight")]
        [YamlMember(Alias = "weight")]
        public int Weight { get; set; }

        /// <summary>
        ///     A node selector term, associated with the corresponding weight.
        /// </summary>
        [JsonProperty("preference")]
        [YamlMember(Alias = "preference")]
        public NodeSelectorTermV1 Preference { get; set; }
    }
}
