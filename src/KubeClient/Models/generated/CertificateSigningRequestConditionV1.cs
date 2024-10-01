using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CertificateSigningRequestCondition describes a condition of a CertificateSigningRequest object
    /// </summary>
    public partial class CertificateSigningRequestConditionV1
    {
        /// <summary>
        ///     lastTransitionTime is the time the condition last transitioned from one status to another. If unset, when a new condition type is added or an existing condition's status is changed, the server defaults this to the current time.
        /// </summary>
        [YamlMember(Alias = "lastTransitionTime")]
        [JsonProperty("lastTransitionTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastTransitionTime { get; set; }

        /// <summary>
        ///     lastUpdateTime is the time of the last update to this condition
        /// </summary>
        [YamlMember(Alias = "lastUpdateTime")]
        [JsonProperty("lastUpdateTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        ///     message contains a human readable message with details about the request state
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     type of the condition. Known conditions are "Approved", "Denied", and "Failed".
        ///     
        ///     An "Approved" condition is added via the /approval subresource, indicating the request was approved and should be issued by the signer.
        ///     
        ///     A "Denied" condition is added via the /approval subresource, indicating the request was denied and should not be issued by the signer.
        ///     
        ///     A "Failed" condition is added via the /status subresource, indicating the signer failed to issue the certificate.
        ///     
        ///     Approved and Denied conditions are mutually exclusive. Approved, Denied, and Failed conditions cannot be removed once added.
        ///     
        ///     Only one condition of a given type is allowed.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Include)]
        public string Type { get; set; }

        /// <summary>
        ///     reason indicates a brief reason for the request state
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     status of the condition, one of True, False, Unknown. Approved, Denied, and Failed conditions may not be "False" or "Unknown".
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public string Status { get; set; }
    }
}
