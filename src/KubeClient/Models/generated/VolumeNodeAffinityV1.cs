using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeNodeAffinity defines constraints that limit what nodes this volume can be accessed from.
    /// </summary>
    public partial class VolumeNodeAffinityV1
    {
        /// <summary>
        ///     Required specifies hard node constraints that must be met.
        /// </summary>
        [YamlMember(Alias = "required")]
        [JsonProperty("required", NullValueHandling = NullValueHandling.Ignore)]
        public NodeSelectorV1 Required { get; set; }
    }
}
