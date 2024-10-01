using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SleepAction describes a "sleep" action.
    /// </summary>
    public partial class SleepActionV1
    {
        /// <summary>
        ///     Seconds is the number of seconds to sleep.
        /// </summary>
        [YamlMember(Alias = "seconds")]
        [JsonProperty("seconds", NullValueHandling = NullValueHandling.Include)]
        public long Seconds { get; set; }
    }
}
