using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NamedRuleWithOperations is a tuple of Operations and Resources with ResourceNames.
    /// </summary>
    public partial class NamedRuleWithOperationsV1Beta1
    {
        /// <summary>
        ///     scope specifies the scope of this rule. Valid values are "Cluster", "Namespaced", and "*" "Cluster" means that only cluster-scoped resources will match this rule. Namespace API objects are cluster-scoped. "Namespaced" means that only namespaced resources will match this rule. "*" means that there are no scope restrictions. Subresources match the scope of their parent resource. Default is "*".
        /// </summary>
        [YamlMember(Alias = "scope")]
        [JsonProperty("scope", NullValueHandling = NullValueHandling.Ignore)]
        public string Scope { get; set; }

        /// <summary>
        ///     APIGroups is the API groups the resources belong to. '*' is all groups. If '*' is present, the length of the slice must be one. Required.
        /// </summary>
        [YamlMember(Alias = "apiGroups")]
        [JsonProperty("apiGroups", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> ApiGroups { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="ApiGroups"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeApiGroups() => ApiGroups.Count > 0;

        /// <summary>
        ///     APIVersions is the API versions the resources belong to. '*' is all versions. If '*' is present, the length of the slice must be one. Required.
        /// </summary>
        [YamlMember(Alias = "apiVersions")]
        [JsonProperty("apiVersions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> ApiVersions { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="ApiVersions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeApiVersions() => ApiVersions.Count > 0;

        /// <summary>
        ///     Operations is the operations the admission hook cares about - CREATE, UPDATE, DELETE, CONNECT or * for all of those operations and any future admission operations that are added. If '*' is present, the length of the slice must be one. Required.
        /// </summary>
        [YamlMember(Alias = "operations")]
        [JsonProperty("operations", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Operations { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Operations"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeOperations() => Operations.Count > 0;

        /// <summary>
        ///     ResourceNames is an optional white list of names that the rule applies to.  An empty set means that everything is allowed.
        /// </summary>
        [YamlMember(Alias = "resourceNames")]
        [JsonProperty("resourceNames", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> ResourceNames { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="ResourceNames"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeResourceNames() => ResourceNames.Count > 0;

        /// <summary>
        ///     Resources is a list of resources this rule applies to.
        ///     
        ///     For example: 'pods' means pods. 'pods/log' means the log subresource of pods. '*' means all resources, but not subresources. 'pods/*' means all subresources of pods. '*/scale' means all scale subresources. '*/*' means all resources and their subresources.
        ///     
        ///     If wildcard is present, the validation rule will ensure resources do not overlap with each other.
        ///     
        ///     Depending on the enclosing object, subresources might not be allowed. Required.
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Resources { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Resources"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeResources() => Resources.Count > 0;
    }
}
