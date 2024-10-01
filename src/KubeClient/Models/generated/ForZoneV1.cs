using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ForZone provides information about which zones should consume this endpoint.
    /// </summary>
    public partial class ForZoneV1
    {
        /// <summary>
        ///     name represents the name of the zone.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }
    }
}
