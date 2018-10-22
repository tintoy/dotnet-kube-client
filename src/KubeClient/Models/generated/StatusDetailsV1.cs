using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     StatusDetails is a set of additional properties that MAY be set by the server to provide additional information about a response. The Reason field of a Status object defines what attributes will be set. Clients must ignore fields that do not match the defined type of each attribute, and should assume that any attribute may be empty, invalid, or under defined.
    /// </summary>
    public partial class StatusDetailsV1
    {
        /// <summary>
        ///     The kind attribute of the resource associated with the status StatusReason. On some operations may differ from the requested resource Kind. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds
        /// </summary>
        [YamlMember(Alias = "kind")]
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
        public string Kind { get; set; }

        /// <summary>
        ///     UID of the resource. (when there is a single resource which can be described). More info: http://kubernetes.io/docs/user-guide/identifiers#uids
        /// </summary>
        [YamlMember(Alias = "uid")]
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }

        /// <summary>
        ///     The name attribute of the resource associated with the status StatusReason (when there is a single name which can be described).
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     The group attribute of the resource associated with the status StatusReason.
        /// </summary>
        [YamlMember(Alias = "group")]
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }

        /// <summary>
        ///     The Causes array includes more details associated with the StatusReason failure. Not all StatusReasons may provide detailed causes.
        /// </summary>
        [YamlMember(Alias = "causes")]
        [JsonProperty("causes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<StatusCauseV1> Causes { get; } = new List<StatusCauseV1>();

        /// <summary>
        ///     Determine whether the <see cref="Causes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeCauses() => Causes.Count > 0;

        /// <summary>
        ///     If specified, the time in seconds before the operation should be retried. Some errors may indicate the client must take an alternate action - for those errors this field may indicate how long to wait before taking the alternate action.
        /// </summary>
        [YamlMember(Alias = "retryAfterSeconds")]
        [JsonProperty("retryAfterSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public int? RetryAfterSeconds { get; set; }
    }
}
