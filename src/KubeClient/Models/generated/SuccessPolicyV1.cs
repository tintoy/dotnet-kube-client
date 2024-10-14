using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SuccessPolicy describes when a Job can be declared as succeeded based on the success of some indexes.
    /// </summary>
    public partial class SuccessPolicyV1
    {
        /// <summary>
        ///     rules represents the list of alternative rules for the declaring the Jobs as successful before `.status.succeeded &gt;= .spec.completions`. Once any of the rules are met, the "SucceededCriteriaMet" condition is added, and the lingering pods are removed. The terminal state for such a Job has the "Complete" condition. Additionally, these rules are evaluated in order; Once the Job meets one of the rules, other rules are ignored. At most 20 elements are allowed.
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<SuccessPolicyRuleV1> Rules { get; } = new List<SuccessPolicyRuleV1>();
    }
}
