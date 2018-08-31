using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Status is a return value for calls that don't return other objects.
    /// </summary>
    [KubeObject("Status", "v1")]
    public partial class StatusV1 : Models.StatusV1, ITracked
    {
        /// <summary>
        ///     Suggested HTTP return code for this status, 0 if not set.
        /// </summary>
        [JsonProperty("code")]
        [YamlMember(Alias = "code")]
        public override int Code
        {
            get
            {
                return base.Code;
            }
            set
            {
                base.Code = value;

                __ModifiedProperties__.Add("Code");
            }
        }


        /// <summary>
        ///     A human-readable description of the status of this operation.
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
        ///     A machine-readable description of why this operation is in the "Failure" status. If this value is empty there is no information available. A Reason clarifies an HTTP status code but does not override it.
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
        ///     Extended data associated with the reason.  Each reason may define its own extended details. This field is optional and the data returned is not guaranteed to conform to any schema except that defined by the reason type.
        /// </summary>
        [JsonProperty("details")]
        [YamlMember(Alias = "details")]
        public override Models.StatusDetailsV1 Details
        {
            get
            {
                return base.Details;
            }
            set
            {
                base.Details = value;

                __ModifiedProperties__.Add("Details");
            }
        }


        /// <summary>
        ///     Status of the operation. One of: "Success" or "Failure". More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
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
