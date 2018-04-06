using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class CertificateSigningRequestConditionV1Beta1
    {
        /// <summary>
        ///     human readable message with details about the request state
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     brief reason for the request state
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     timestamp for the last update to this condition
        /// </summary>
        [JsonProperty("lastUpdateTime")]
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        ///     request approval state, currently Approved or Denied.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
