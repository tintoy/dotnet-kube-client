using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     SubjectAccessReviewStatus
    /// </summary>
    [KubeObject("SubjectAccessReviewStatus", "v1")]
    public partial class SubjectAccessReviewStatusV1
    {
        /// <summary>
        ///     Allowed is required.  True if the action would be allowed, false otherwise.
        /// </summary>
        [JsonProperty("allowed")]
        public bool Allowed { get; set; }

        /// <summary>
        ///     Reason is optional.  It indicates why a request was allowed or denied.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        ///     EvaluationError is an indication that some error occurred during the authorization check. It is entirely possible to get an error and be able to continue determine authorization status in spite of it. For instance, RBAC can be missing a role, but enough roles are still present and bound to reason about the request.
        /// </summary>
        [JsonProperty("evaluationError")]
        public string EvaluationError { get; set; }
    }
}
