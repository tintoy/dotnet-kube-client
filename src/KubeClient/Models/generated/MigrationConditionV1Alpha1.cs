using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Describes the state of a migration at a certain point.
    /// </summary>
    public partial class MigrationConditionV1Alpha1
    {
        /// <summary>
        ///     The last time this condition was updated.
        /// </summary>
        [YamlMember(Alias = "lastUpdateTime")]
        [JsonProperty("lastUpdateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        ///     A human readable message indicating details about the transition.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     Type of the condition.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     The reason for the condition's last transition.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     Status of the condition, one of True, False, Unknown.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public string Status { get; set; }
    }
}
