using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     AttachedVolume describes a volume attached to a node
    /// </summary>
    public partial class AttachedVolumeV1 : Models.AttachedVolumeV1, ITracked
    {
        /// <summary>
        ///     Name of the attached volume
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;

                __ModifiedProperties__.Add("Name");
            }
        }


        /// <summary>
        ///     DevicePath represents the device path where the volume should be available
        /// </summary>
        [JsonProperty("devicePath")]
        [YamlMember(Alias = "devicePath")]
        public override string DevicePath
        {
            get
            {
                return base.DevicePath;
            }
            set
            {
                base.DevicePath = value;

                __ModifiedProperties__.Add("DevicePath");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
