using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PolicyRule holds information that describes a policy rule, but does not contain information about who the rule applies to or which namespace the rule applies to.
    /// </summary>
    public partial class PolicyRuleV1Beta1
    {
        /// <summary>
        ///     APIGroups is the name of the APIGroup that contains the resources.  If multiple API groups are specified, any action requested against one of the enumerated resources in any API group will be allowed.
        /// </summary>
        [YamlMember(Alias = "apiGroups")]
        [JsonProperty("apiGroups", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ApiGroups { get; set; } = new List<string>();

        /// <summary>
        ///     NonResourceURLs is a set of partial urls that a user should have access to.  *s are allowed, but only as the full, final step in the path Since non-resource URLs are not namespaced, this field is only applicable for ClusterRoles referenced from a ClusterRoleBinding. Rules can either apply to API resources (such as "pods" or "secrets") or non-resource URL paths (such as "/api"),  but not both.
        /// </summary>
        [YamlMember(Alias = "nonResourceURLs")]
        [JsonProperty("nonResourceURLs", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> NonResourceURLs { get; set; } = new List<string>();

        /// <summary>
        ///     ResourceNames is an optional white list of names that the rule applies to.  An empty set means that everything is allowed.
        /// </summary>
        [YamlMember(Alias = "resourceNames")]
        [JsonProperty("resourceNames", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ResourceNames { get; set; } = new List<string>();

        /// <summary>
        ///     Resources is a list of resources this rule applies to.  ResourceAll represents all resources.
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Resources { get; set; } = new List<string>();

        /// <summary>
        ///     Verbs is a list of Verbs that apply to ALL the ResourceKinds and AttributeRestrictions contained in this rule.  VerbAll represents all kinds.
        /// </summary>
        [YamlMember(Alias = "verbs")]
        [JsonProperty("verbs", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Verbs { get; set; } = new List<string>();
    }
}
