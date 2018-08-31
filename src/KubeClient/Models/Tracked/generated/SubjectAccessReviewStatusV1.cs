using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     SubjectAccessReviewStatus
    /// </summary>
    public partial class SubjectAccessReviewStatusV1 : Models.SubjectAccessReviewStatusV1
    {
        /// <summary>
        ///     Allowed is required.  True if the action would be allowed, false otherwise.
        /// </summary>
        [JsonProperty("allowed")]
        [YamlMember(Alias = "allowed")]
        public override bool Allowed
        {
            get
            {
                return base.Allowed;
            }
            set
            {
                base.Allowed = value;

                __ModifiedProperties__.Add("Allowed");
            }
        }


        /// <summary>
        ///     Reason is optional.  It indicates why a request was allowed or denied.
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
        ///     EvaluationError is an indication that some error occurred during the authorization check. It is entirely possible to get an error and be able to continue determine authorization status in spite of it. For instance, RBAC can be missing a role, but enough roles are still present and bound to reason about the request.
        /// </summary>
        [JsonProperty("evaluationError")]
        [YamlMember(Alias = "evaluationError")]
        public override string EvaluationError
        {
            get
            {
                return base.EvaluationError;
            }
            set
            {
                base.EvaluationError = value;

                __ModifiedProperties__.Add("EvaluationError");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
