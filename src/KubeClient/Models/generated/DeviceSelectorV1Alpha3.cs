using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeviceSelector must have exactly one field set.
    /// </summary>
    public partial class DeviceSelectorV1Alpha3
    {
        /// <summary>
        ///     CEL contains a CEL expression for selecting a device.
        /// </summary>
        [YamlMember(Alias = "cel")]
        [JsonProperty("cel", NullValueHandling = NullValueHandling.Ignore)]
        public CELDeviceSelectorV1Alpha3 Cel { get; set; }
    }
}
