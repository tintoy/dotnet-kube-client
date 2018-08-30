using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeploymentCondition describes the state of a deployment at a certain point.
    /// </summary>
    public partial class DeploymentConditionV1Beta1
    {
        /// <summary>
        ///     Last time the condition transitioned from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        [YamlMember(Alias = "lastTransitionTime")]
        public virtual DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     The last time this condition was updated.
        /// </summary>
        [JsonProperty("lastUpdateTime")]
        [YamlMember(Alias = "lastUpdateTime")]
        public virtual DateTime? LastUpdateTime { get; set; }

        /// <summary>
        ///     A human readable message indicating details about the transition.
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public virtual string Message { get; set; }

        /// <summary>
        ///     Type of deployment condition.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public virtual string Type { get; set; }

        /// <summary>
        ///     The reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public virtual string Reason { get; set; }

        /// <summary>
        ///     Status of the condition, one of True, False, Unknown.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public virtual string Status { get; set; }
    }
}
