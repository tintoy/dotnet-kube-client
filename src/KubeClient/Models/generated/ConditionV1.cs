using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Condition contains details for one aspect of the current state of this API Resource.
    /// </summary>
    public partial class ConditionV1
    {
        /// <summary>
        ///     lastTransitionTime is the last time the condition transitioned from one status to another. This should be when the underlying condition changed.  If that is not known, then using the time when the API field changed is acceptable.
        /// </summary>
        [YamlMember(Alias = "lastTransitionTime")]
        [JsonProperty("lastTransitionTime", NullValueHandling = NullValueHandling.Include)]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     message is a human readable message indicating details about the transition. This may be an empty string.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Include)]
        public string Message { get; set; }

        /// <summary>
        ///     type of condition in CamelCase or in foo.example.com/CamelCase.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     observedGeneration represents the .metadata.generation that the condition was set based upon. For instance, if .metadata.generation is currently 12, but the .status.conditions[x].observedGeneration is 9, the condition is out of date with respect to the current state of the instance.
        /// </summary>
        [YamlMember(Alias = "observedGeneration")]
        [JsonProperty("observedGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObservedGeneration { get; set; }

        /// <summary>
        ///     reason contains a programmatic identifier indicating the reason for the condition's last transition. Producers of specific condition types may define expected values and meanings for this field, and whether the values are considered a guaranteed API. The value should be a CamelCase string. This field may not be empty.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Include)]
        public string Reason { get; set; }

        /// <summary>
        ///     status of the condition, one of True, False, Unknown.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public string Status { get; set; }
    }
}
