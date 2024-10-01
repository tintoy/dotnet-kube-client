using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CSINodeSpec holds information about the specification of all CSI drivers installed on a node
    /// </summary>
    public partial class CSINodeSpecV1
    {
        /// <summary>
        ///     drivers is a list of information of all CSI Drivers existing on a node. If all drivers in the list are uninstalled, this can become empty.
        /// </summary>
        [MergeStrategy(Key = "name")]
        [YamlMember(Alias = "drivers")]
        [JsonProperty("drivers", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<CSINodeDriverV1> Drivers { get; } = new List<CSINodeDriverV1>();
    }
}
