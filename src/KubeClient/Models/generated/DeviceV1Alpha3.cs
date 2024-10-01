using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Device represents one individual hardware instance that can be selected based on its attributes. Besides the name, exactly one field must be set.
    /// </summary>
    public partial class DeviceV1Alpha3
    {
        /// <summary>
        ///     Basic defines one device instance.
        /// </summary>
        [YamlMember(Alias = "basic")]
        [JsonProperty("basic", NullValueHandling = NullValueHandling.Ignore)]
        public BasicDeviceV1Alpha3 Basic { get; set; }

        /// <summary>
        ///     Name is unique identifier among all devices managed by the driver in the pool. It must be a DNS label.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }
    }
}
