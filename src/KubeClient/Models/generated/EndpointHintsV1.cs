using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     EndpointHints provides hints describing how an endpoint should be consumed.
    /// </summary>
    public partial class EndpointHintsV1
    {
        /// <summary>
        ///     forZones indicates the zone(s) this endpoint should be consumed by to enable topology aware routing.
        /// </summary>
        [YamlMember(Alias = "forZones")]
        [JsonProperty("forZones", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ForZoneV1> ForZones { get; } = new List<ForZoneV1>();

        /// <summary>
        ///     Determine whether the <see cref="ForZones"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeForZones() => ForZones.Count > 0;
    }
}
