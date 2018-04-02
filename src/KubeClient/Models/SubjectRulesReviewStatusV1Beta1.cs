using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     SubjectRulesReviewStatus contains the result of a rules check. This check can be incomplete depending on the set of authorizers the server is configured with and any errors experienced during evaluation. Because authorization rules are additive, if a rule appears in a list it's safe to assume the subject has that permission, even if that list is incomplete.
    /// </summary>
    [KubeObject("SubjectRulesReviewStatus", "v1beta1")]
    public partial class SubjectRulesReviewStatusV1Beta1
    {
        /// <summary>
        ///     Incomplete is true when the rules returned by this call are incomplete. This is most commonly encountered when an authorizer, such as an external authorizer, doesn't support rules evaluation.
        /// </summary>
        [JsonProperty("incomplete")]
        public bool Incomplete { get; set; }

        /// <summary>
        ///     EvaluationError can appear in combination with Rules. It indicates an error occurred during rule evaluation, such as an authorizer that doesn't support rule evaluation, and that ResourceRules and/or NonResourceRules may be incomplete.
        /// </summary>
        [JsonProperty("evaluationError")]
        public string EvaluationError { get; set; }

        /// <summary>
        ///     NonResourceRules is the list of actions the subject is allowed to perform on non-resources. The list ordering isn't significant, may contain duplicates, and possibly be incomplete.
        /// </summary>
        [JsonProperty("nonResourceRules", NullValueHandling = NullValueHandling.Ignore)]
        public List<NonResourceRuleV1Beta1> NonResourceRules { get; set; } = new List<NonResourceRuleV1Beta1>();

        /// <summary>
        ///     ResourceRules is the list of actions the subject is allowed to perform on resources. The list ordering isn't significant, may contain duplicates, and possibly be incomplete.
        /// </summary>
        [JsonProperty("resourceRules", NullValueHandling = NullValueHandling.Ignore)]
        public List<ResourceRuleV1Beta1> ResourceRules { get; set; } = new List<ResourceRuleV1Beta1>();
    }
}
