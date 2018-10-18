using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class CertificateSigningRequestConditionV1Beta1
    {
        /// <summary>
        ///     timestamp for the last update to this condition
        /// </summary>
        [YamlMember(Alias = "lastUpdateTime")]
        [JsonProperty("lastUpdateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        ///     human readable message with details about the request state
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     request approval state, currently Approved or Denied.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     brief reason for the request state
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }
    }
}
