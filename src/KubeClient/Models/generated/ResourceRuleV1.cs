using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceRule is the list of actions the subject is allowed to perform on resources. The list ordering isn't significant, may contain duplicates, and possibly be incomplete.
    /// </summary>
    public partial class ResourceRuleV1
    {
        /// <summary>
        ///     APIGroups is the name of the APIGroup that contains the resources.  If multiple API groups are specified, any action requested against one of the enumerated resources in any API group will be allowed.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "apiGroups")]
        [JsonProperty("apiGroups", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ApiGroups { get; set; } = new List<string>();

        /// <summary>
        ///     ResourceNames is an optional white list of names that the rule applies to.  An empty set means that everything is allowed.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "resourceNames")]
        [JsonProperty("resourceNames", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ResourceNames { get; set; } = new List<string>();

        /// <summary>
        ///     Resources is a list of resources this rule applies to.  "*" means all in the specified apiGroups.
        ///      "*/foo" represents the subresource 'foo' for all resources in the specified apiGroups.
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Resources { get; set; } = new List<string>();

        /// <summary>
        ///     Verb is a list of kubernetes resource API verbs, like: get, list, watch, create, update, delete, proxy.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "verbs")]
        [JsonProperty("verbs", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Verbs { get; set; } = new List<string>();
    }
}
