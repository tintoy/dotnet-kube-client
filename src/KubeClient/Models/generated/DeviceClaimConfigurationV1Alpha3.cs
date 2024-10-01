using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceClaimConfiguration is used for configuration parameters in DeviceClaim.
    /// </summary>
    public partial class DeviceClaimConfigurationV1Alpha3
    {
        /// <summary>
        ///     Opaque provides driver-specific configuration parameters.
        /// </summary>
        [YamlMember(Alias = "opaque")]
        [JsonProperty("opaque", NullValueHandling = NullValueHandling.Ignore)]
        public OpaqueDeviceConfigurationV1Alpha3 Opaque { get; set; }

        /// <summary>
        ///     Requests lists the names of requests where the configuration applies. If empty, it applies to all requests.
        /// </summary>
        [YamlMember(Alias = "requests")]
        [JsonProperty("requests", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Requests { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Requests"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRequests() => Requests.Count > 0;
    }
}
