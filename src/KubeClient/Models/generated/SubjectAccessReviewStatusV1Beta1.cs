using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SubjectAccessReviewStatus
    /// </summary>
    public partial class SubjectAccessReviewStatusV1Beta1
    {
        /// <summary>
        ///     Allowed is required. True if the action would be allowed, false otherwise.
        /// </summary>
        [YamlMember(Alias = "allowed")]
        [JsonProperty("allowed", NullValueHandling = NullValueHandling.Include)]
        public bool Allowed { get; set; }

        /// <summary>
        ///     Denied is optional. True if the action would be denied, otherwise false. If both allowed is false and denied is false, then the authorizer has no opinion on whether to authorize the action. Denied may not be true if Allowed is true.
        /// </summary>
        [YamlMember(Alias = "denied")]
        [JsonProperty("denied", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Denied { get; set; }

        /// <summary>
        ///     Reason is optional.  It indicates why a request was allowed or denied.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }

        /// <summary>
        ///     EvaluationError is an indication that some error occurred during the authorization check. It is entirely possible to get an error and be able to continue determine authorization status in spite of it. For instance, RBAC can be missing a role, but enough roles are still present and bound to reason about the request.
        /// </summary>
        [YamlMember(Alias = "evaluationError")]
        [JsonProperty("evaluationError", NullValueHandling = NullValueHandling.Ignore)]
        public string EvaluationError { get; set; }
    }
}
