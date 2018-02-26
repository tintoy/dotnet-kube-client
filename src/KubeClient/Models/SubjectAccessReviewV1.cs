using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     SubjectAccessReview checks whether or not a user or group can perform an action.
    /// </summary>
    [KubeObject("SubjectAccessReview", "v1")]
    public class SubjectAccessReviewV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec holds information about the request being evaluated
        /// </summary>
        [JsonProperty("spec")]
        public SubjectAccessReviewSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status is filled in by the server and indicates whether the request is allowed or not
        /// </summary>
        [JsonProperty("status")]
        public SubjectAccessReviewStatusV1 Status { get; set; }
    }
}
