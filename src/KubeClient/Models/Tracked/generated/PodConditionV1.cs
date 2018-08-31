using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodCondition contains details for the current condition of this pod.
    /// </summary>
    public partial class PodConditionV1 : Models.PodConditionV1
    {
        /// <summary>
        ///     Last time we probed the condition.
        /// </summary>
        [JsonProperty("lastProbeTime")]
        [YamlMember(Alias = "lastProbeTime")]
        public override DateTime? LastProbeTime
        {
            get
            {
                return base.LastProbeTime;
            }
            set
            {
                base.LastProbeTime = value;

                __ModifiedProperties__.Add("LastProbeTime");
            }
        }


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
        ///     Human-readable message indicating details about last transition.
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
        ///     Type is the type of the condition. Currently only Ready. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-conditions
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
        ///     Unique, one-word, CamelCase reason for the condition's last transition.
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
        ///     Status is the status of the condition. Can be True, False, Unknown. More info: https://kubernetes.io/docs/concepts/workloads/pods/pod-lifecycle#pod-conditions
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
