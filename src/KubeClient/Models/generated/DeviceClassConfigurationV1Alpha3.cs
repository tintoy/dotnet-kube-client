using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceClassConfiguration is used in DeviceClass.
    /// </summary>
    public partial class DeviceClassConfigurationV1Alpha3
    {
        /// <summary>
        ///     Opaque provides driver-specific configuration parameters.
        /// </summary>
        [YamlMember(Alias = "opaque")]
        [JsonProperty("opaque", NullValueHandling = NullValueHandling.Ignore)]
        public OpaqueDeviceConfigurationV1Alpha3 Opaque { get; set; }
    }
}
