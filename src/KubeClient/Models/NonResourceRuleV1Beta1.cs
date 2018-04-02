using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     NonResourceRule holds information that describes a rule for the non-resource
    /// </summary>
    [KubeObject("NonResourceRule", "v1beta1")]
    public partial class NonResourceRuleV1Beta1
    {
        /// <summary>
        ///     NonResourceURLs is a set of partial urls that a user should have access to.  *s are allowed, but only as the full, final step in the path.  "*" means all.
        /// </summary>
        [JsonProperty("nonResourceURLs", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> NonResourceURLs { get; set; } = new List<string>();

        /// <summary>
        ///     Verb is a list of kubernetes non-resource API verbs, like: get, post, put, delete, patch, head, options.  "*" means all.
        /// </summary>
        [JsonProperty("verbs", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Verbs { get; set; } = new List<string>();
    }
}
