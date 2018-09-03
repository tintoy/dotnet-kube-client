using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NonResourceRule holds information that describes a rule for the non-resource
    /// </summary>
    public partial class NonResourceRuleV1
    {
        /// <summary>
        ///     NonResourceURLs is a set of partial urls that a user should have access to.  *s are allowed, but only as the full, final step in the path.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "nonResourceURLs")]
        [JsonProperty("nonResourceURLs", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> NonResourceURLs { get; set; } = new List<string>();

        /// <summary>
        ///     Verb is a list of kubernetes non-resource API verbs, like: get, post, put, delete, patch, head, options.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "verbs")]
        [JsonProperty("verbs", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Verbs { get; set; } = new List<string>();
    }
}
