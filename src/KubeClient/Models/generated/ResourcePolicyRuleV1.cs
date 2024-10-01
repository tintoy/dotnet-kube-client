using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourcePolicyRule is a predicate that matches some resource requests, testing the request's verb and the target resource. A ResourcePolicyRule matches a resource request if and only if: (a) at least one member of verbs matches the request, (b) at least one member of apiGroups matches the request, (c) at least one member of resources matches the request, and (d) either (d1) the request does not specify a namespace (i.e., `Namespace==""`) and clusterScope is true or (d2) the request specifies a namespace and least one member of namespaces matches the request's namespace.
    /// </summary>
    public partial class ResourcePolicyRuleV1
    {
        /// <summary>
        ///     `clusterScope` indicates whether to match requests that do not specify a namespace (which happens either because the resource is not namespaced or the request targets all namespaces). If this field is omitted or false then the `namespaces` field must contain a non-empty list.
        /// </summary>
        [YamlMember(Alias = "clusterScope")]
        [JsonProperty("clusterScope", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ClusterScope { get; set; }

        /// <summary>
        ///     `apiGroups` is a list of matching API groups and may not be empty. "*" matches all API groups and, if present, must be the only entry. Required.
        /// </summary>
        [YamlMember(Alias = "apiGroups")]
        [JsonProperty("apiGroups", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> ApiGroups { get; } = new List<string>();

        /// <summary>
        ///     `namespaces` is a list of target namespaces that restricts matches.  A request that specifies a target namespace matches only if either (a) this list contains that target namespace or (b) this list contains "*".  Note that "*" matches any specified namespace but does not match a request that _does not specify_ a namespace (see the `clusterScope` field for that). This list may be empty, but only if `clusterScope` is true.
        /// </summary>
        [YamlMember(Alias = "namespaces")]
        [JsonProperty("namespaces", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Namespaces { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Namespaces"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeNamespaces() => Namespaces.Count > 0;

        /// <summary>
        ///     `resources` is a list of matching resources (i.e., lowercase and plural) with, if desired, subresource.  For example, [ "services", "nodes/status" ].  This list may not be empty. "*" matches all resources and, if present, must be the only entry. Required.
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Resources { get; } = new List<string>();

        /// <summary>
        ///     `verbs` is a list of matching verbs and may not be empty. "*" matches all verbs and, if present, must be the only entry. Required.
        /// </summary>
        [YamlMember(Alias = "verbs")]
        [JsonProperty("verbs", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Verbs { get; } = new List<string>();
    }
}
