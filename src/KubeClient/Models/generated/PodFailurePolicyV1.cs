using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodFailurePolicy describes how failed pods influence the backoffLimit.
    /// </summary>
    public partial class PodFailurePolicyV1
    {
        /// <summary>
        ///     A list of pod failure policy rules. The rules are evaluated in order. Once a rule matches a Pod failure, the remaining of the rules are ignored. When no rule matches the Pod failure, the default handling applies - the counter of pod failures is incremented and it is checked against the backoffLimit. At most 20 elements are allowed.
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PodFailurePolicyRuleV1> Rules { get; } = new List<PodFailurePolicyRuleV1>();
    }
}
