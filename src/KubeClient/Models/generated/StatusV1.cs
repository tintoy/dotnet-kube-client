using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Status is a return value for calls that don't return other objects.
    /// </summary>
    [KubeObject("Status", "v1")]
    public partial class StatusV1 : KubeResourceListV1
    {
        /// <summary>
        ///     Suggested HTTP return code for this status, 0 if not set.
        /// </summary>
        [YamlMember(Alias = "code")]
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public int? Code { get; set; }

        /// <summary>
        ///     A human-readable description of the status of this operation.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     A machine-readable description of why this operation is in the "Failure" status. If this value is empty there is no information available. A Reason clarifies an HTTP status code but does not override it.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     Extended data associated with the reason.  Each reason may define its own extended details. This field is optional and the data returned is not guaranteed to conform to any schema except that defined by the reason type.
        /// </summary>
        [YamlMember(Alias = "details")]
        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public StatusDetailsV1 Details { get; set; }

        /// <summary>
        ///     Status of the operation. One of: "Success" or "Failure". More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }
}
