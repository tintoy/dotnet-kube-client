using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NonResourceRule holds information that describes a rule for the non-resource
    /// </summary>
    public partial class NonResourceRuleV1Beta1
    {
        /// <summary>
        ///     NonResourceURLs is a set of partial urls that a user should have access to.  *s are allowed, but only as the full, final step in the path.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "nonResourceURLs")]
        [JsonProperty("nonResourceURLs", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> NonResourceURLs { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="NonResourceURLs"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeNonResourceURLs() => NonResourceURLs.Count > 0;

        /// <summary>
        ///     Verb is a list of kubernetes non-resource API verbs, like: get, post, put, delete, patch, head, options.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "verbs")]
        [JsonProperty("verbs", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Verbs { get; } = new List<string>();
    }
}
