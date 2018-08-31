using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     StatusDetails is a set of additional properties that MAY be set by the server to provide additional information about a response. The Reason field of a Status object defines what attributes will be set. Clients must ignore fields that do not match the defined type of each attribute, and should assume that any attribute may be empty, invalid, or under defined.
    /// </summary>
    public partial class StatusDetailsV1 : Models.StatusDetailsV1, ITracked
    {
        /// <summary>
        ///     The kind attribute of the resource associated with the status StatusReason. On some operations may differ from the requested resource Kind. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public override string Kind
        {
            get
            {
                return base.Kind;
            }
            set
            {
                base.Kind = value;

                __ModifiedProperties__.Add("Kind");
            }
        }


        /// <summary>
        ///     UID of the resource. (when there is a single resource which can be described). More info: http://kubernetes.io/docs/user-guide/identifiers#uids
        /// </summary>
        [JsonProperty("uid")]
        [YamlMember(Alias = "uid")]
        public override string Uid
        {
            get
            {
                return base.Uid;
            }
            set
            {
                base.Uid = value;

                __ModifiedProperties__.Add("Uid");
            }
        }


        /// <summary>
        ///     The name attribute of the resource associated with the status StatusReason (when there is a single name which can be described).
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
        ///     The group attribute of the resource associated with the status StatusReason.
        /// </summary>
        [JsonProperty("group")]
        [YamlMember(Alias = "group")]
        public override string Group
        {
            get
            {
                return base.Group;
            }
            set
            {
                base.Group = value;

                __ModifiedProperties__.Add("Group");
            }
        }


        /// <summary>
        ///     The Causes array includes more details associated with the StatusReason failure. Not all StatusReasons may provide detailed causes.
        /// </summary>
        [YamlMember(Alias = "causes")]
        [JsonProperty("causes", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.StatusCauseV1> Causes { get; set; } = new List<Models.StatusCauseV1>();

        /// <summary>
        ///     If specified, the time in seconds before the operation should be retried.
        /// </summary>
        [JsonProperty("retryAfterSeconds")]
        [YamlMember(Alias = "retryAfterSeconds")]
        public override int RetryAfterSeconds
        {
            get
            {
                return base.RetryAfterSeconds;
            }
            set
            {
                base.RetryAfterSeconds = value;

                __ModifiedProperties__.Add("RetryAfterSeconds");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
