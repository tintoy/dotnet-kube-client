using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Describes the state of the storageVersion at a certain point.
    /// </summary>
    public partial class StorageVersionConditionV1Alpha1
    {
        /// <summary>
        ///     Last time the condition transitioned from one status to another.
        /// </summary>
        [YamlMember(Alias = "lastTransitionTime")]
        [JsonProperty("lastTransitionTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     A human readable message indicating details about the transition.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Include)]
        public string Message { get; set; }

        /// <summary>
        ///     Type of the condition.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     If set, this represents the .metadata.generation that the condition was set based upon.
        /// </summary>
        [YamlMember(Alias = "observedGeneration")]
        [JsonProperty("observedGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObservedGeneration { get; set; }

        /// <summary>
        ///     The reason for the condition's last transition.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Include)]
        public string Reason { get; set; }

        /// <summary>
        ///     Status of the condition, one of True, False, Unknown.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public string Status { get; set; }
    }
}
