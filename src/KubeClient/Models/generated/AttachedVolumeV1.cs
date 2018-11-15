using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     AttachedVolume describes a volume attached to a node
    /// </summary>
    public partial class AttachedVolumeV1
    {
        /// <summary>
        ///     Name of the attached volume
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     DevicePath represents the device path where the volume should be available
        /// </summary>
        [YamlMember(Alias = "devicePath")]
        [JsonProperty("devicePath", NullValueHandling = NullValueHandling.Include)]
        public string DevicePath { get; set; }
    }
}
