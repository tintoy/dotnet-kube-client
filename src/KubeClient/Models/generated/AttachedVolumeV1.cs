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
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public virtual string Name { get; set; }

        /// <summary>
        ///     DevicePath represents the device path where the volume should be available
        /// </summary>
        [JsonProperty("devicePath")]
        [YamlMember(Alias = "devicePath")]
        public virtual string DevicePath { get; set; }
    }
}
