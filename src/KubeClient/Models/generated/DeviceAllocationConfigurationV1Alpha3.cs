using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceAllocationConfiguration gets embedded in an AllocationResult.
    /// </summary>
    public partial class DeviceAllocationConfigurationV1Alpha3
    {
        /// <summary>
        ///     Opaque provides driver-specific configuration parameters.
        /// </summary>
        [YamlMember(Alias = "opaque")]
        [JsonProperty("opaque", NullValueHandling = NullValueHandling.Ignore)]
        public OpaqueDeviceConfigurationV1Alpha3 Opaque { get; set; }

        /// <summary>
        ///     Source records whether the configuration comes from a class and thus is not something that a normal user would have been able to set or from a claim.
        /// </summary>
        [YamlMember(Alias = "source")]
        [JsonProperty("source", NullValueHandling = NullValueHandling.Include)]
        public string Source { get; set; }

        /// <summary>
        ///     Requests lists the names of requests where the configuration applies. If empty, its applies to all requests.
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
