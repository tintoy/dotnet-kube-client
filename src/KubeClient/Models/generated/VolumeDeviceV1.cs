using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     volumeDevice describes a mapping of a raw block device within a container.
    /// </summary>
    public partial class VolumeDeviceV1
    {
        /// <summary>
        ///     name must match the name of a persistentVolumeClaim in the pod
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     devicePath is the path inside of the container that the device will be mapped to.
        /// </summary>
        [JsonProperty("devicePath")]
        [YamlMember(Alias = "devicePath")]
        public string DevicePath { get; set; }
    }
}
