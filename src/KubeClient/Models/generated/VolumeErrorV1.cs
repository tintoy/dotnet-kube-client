using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     VolumeError captures an error encountered during a volume operation.
    /// </summary>
    public partial class VolumeErrorV1
    {
        /// <summary>
        ///     message represents the error encountered during Attach or Detach operation. This string may be logged, so it should not contain sensitive information.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     time represents the time the error was encountered.
        /// </summary>
        [YamlMember(Alias = "time")]
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Time { get; set; }
    }
}
