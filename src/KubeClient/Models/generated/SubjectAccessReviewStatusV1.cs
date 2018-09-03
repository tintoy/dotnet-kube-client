using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SubjectAccessReviewStatus
    /// </summary>
    public partial class SubjectAccessReviewStatusV1
    {
        /// <summary>
        ///     Denied is optional. True if the action would be denied, otherwise false. If both allowed is false and denied is false, then the authorizer has no opinion on whether to authorize the action. Denied may not be true if Allowed is true.
        /// </summary>
        [JsonProperty("denied")]
        [YamlMember(Alias = "denied")]
        public bool Denied { get; set; }

        /// <summary>
        ///     Denied is optional. True if the action would be denied, otherwise false. If both allowed is false and denied is false, then the authorizer has no opinion on whether to authorize the action. Denied may not be true if Allowed is true.
        /// </summary>
        [JsonProperty("denied")]
        [YamlMember(Alias = "denied")]
        public bool Denied { get; set; }

        /// <summary>
        ///     Reason is optional.  It indicates why a request was allowed or denied.
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     EvaluationError is an indication that some error occurred during the authorization check. It is entirely possible to get an error and be able to continue determine authorization status in spite of it. For instance, RBAC can be missing a role, but enough roles are still present and bound to reason about the request.
        /// </summary>
        [JsonProperty("evaluationError")]
        [YamlMember(Alias = "evaluationError")]
        public string EvaluationError { get; set; }

        /// <summary>
        ///     Allowed is required. True if the action would be allowed, false otherwise.
        /// </summary>
        [JsonProperty("allowed")]
        [YamlMember(Alias = "allowed")]
        public bool Allowed { get; set; }
    }
}
