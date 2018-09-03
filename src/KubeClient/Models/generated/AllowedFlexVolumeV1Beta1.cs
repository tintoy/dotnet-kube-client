using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     AllowedFlexVolume represents a single Flexvolume that is allowed to be used.
    /// </summary>
    public partial class AllowedFlexVolumeV1Beta1
    {
        /// <summary>
        ///     driver is the name of the Flexvolume driver.
        /// </summary>
        [JsonProperty("driver")]
        [YamlMember(Alias = "driver")]
        public string Driver { get; set; }
    }
}
