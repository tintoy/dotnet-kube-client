using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressClassParametersReference identifies an API object. This can be used to specify a cluster or namespace-scoped resource.
    /// </summary>
    public partial class IngressClassParametersReferenceV1
    {
        /// <summary>
        ///     kind is the type of resource being referenced.
        /// </summary>
        [YamlMember(Alias = "kind")]
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Include)]
        public string Kind { get; set; }

        /// <summary>
        ///     name is the name of resource being referenced.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     namespace is the namespace of the resource being referenced. This field is required when scope is set to "Namespace" and must be unset when scope is set to "Cluster".
        /// </summary>
        [YamlMember(Alias = "namespace")]
        [JsonProperty("namespace", NullValueHandling = NullValueHandling.Ignore)]
        public string Namespace { get; set; }

        /// <summary>
        ///     scope represents if this refers to a cluster or namespace scoped resource. This may be set to "Cluster" (default) or "Namespace".
        /// </summary>
        [YamlMember(Alias = "scope")]
        [JsonProperty("scope", NullValueHandling = NullValueHandling.Ignore)]
        public string Scope { get; set; }

        /// <summary>
        ///     apiGroup is the group for the resource being referenced. If APIGroup is not specified, the specified Kind must be in the core API group. For any other third-party types, APIGroup is required.
        /// </summary>
        [YamlMember(Alias = "apiGroup")]
        [JsonProperty("apiGroup", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiGroup { get; set; }
    }
}
