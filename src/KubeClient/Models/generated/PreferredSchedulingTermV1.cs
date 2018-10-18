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
        ///     A node selector term, associated with the corresponding weight.
        /// </summary>
        [YamlMember(Alias = "preference")]
        [JsonProperty("preference", NullValueHandling = NullValueHandling.Include)]
        public NodeSelectorTermV1 Preference { get; set; }

        /// <summary>
        ///     Weight associated with matching the corresponding nodeSelectorTerm, in the range 1-100.
        /// </summary>
        [YamlMember(Alias = "weight")]
        [JsonProperty("weight", NullValueHandling = NullValueHandling.Include)]
        public int Weight { get; set; }
    }
}
