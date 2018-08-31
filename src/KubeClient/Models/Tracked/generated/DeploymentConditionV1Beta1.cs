using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DeploymentCondition describes the state of a deployment at a certain point.
    /// </summary>
    public partial class DeploymentConditionV1Beta1 : Models.DeploymentConditionV1Beta1
    {
        /// <summary>
        ///     Last time the condition transitioned from one status to another.
        /// </summary>
        [JsonProperty("lastTransitionTime")]
        [YamlMember(Alias = "lastTransitionTime")]
        public override DateTime? LastTransitionTime
        {
            get
            {
                return base.LastTransitionTime;
            }
            set
            {
                base.LastTransitionTime = value;

                __ModifiedProperties__.Add("LastTransitionTime");
            }
        }


        /// <summary>
        ///     The last time this condition was updated.
        /// </summary>
        [JsonProperty("lastUpdateTime")]
        [YamlMember(Alias = "lastUpdateTime")]
        public override DateTime? LastUpdateTime
        {
            get
            {
                return base.LastUpdateTime;
            }
            set
            {
                base.LastUpdateTime = value;

                __ModifiedProperties__.Add("LastUpdateTime");
            }
        }


        /// <summary>
        ///     A human readable message indicating details about the transition.
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public override string Message
        {
            get
            {
                return base.Message;
            }
            set
            {
                base.Message = value;

                __ModifiedProperties__.Add("Message");
            }
        }


        /// <summary>
        ///     Type of deployment condition.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public override string Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;

                __ModifiedProperties__.Add("Type");
            }
        }


        /// <summary>
        ///     The reason for the condition's last transition.
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public override string Reason
        {
            get
            {
                return base.Reason;
            }
            set
            {
                base.Reason = value;

                __ModifiedProperties__.Add("Reason");
            }
        }


        /// <summary>
        ///     Status of the condition, one of True, False, Unknown.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public override string Status
        {
            get
            {
                return base.Status;
            }
            set
            {
                base.Status = value;

                __ModifiedProperties__.Add("Status");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
