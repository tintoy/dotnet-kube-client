using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceAttributes includes the authorization attributes available for resource requests to the Authorizer interface
    /// </summary>
    public partial class ResourceAttributesV1Beta1
    {
        /// <summary>
        ///     Verb is a kubernetes resource API verb, like: get, list, watch, create, update, delete, proxy.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "verb")]
        [JsonProperty("verb", NullValueHandling = NullValueHandling.Ignore)]
        public string Verb { get; set; }

        /// <summary>
        ///     Name is the name of the resource being requested for a "get" or deleted for a "delete". "" (empty) means all.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     Namespace is the namespace of the action being requested.  Currently, there is no distinction between no namespace and all namespaces "" (empty) is defaulted for LocalSubjectAccessReviews "" (empty) is empty for cluster-scoped resources "" (empty) means "all" for namespace scoped resources from a SubjectAccessReview or SelfSubjectAccessReview
        /// </summary>
        [YamlMember(Alias = "namespace")]
        [JsonProperty("namespace", NullValueHandling = NullValueHandling.Ignore)]
        public string Namespace { get; set; }

        /// <summary>
        ///     Resource is one of the existing resource types.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "resource")]
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Ignore)]
        public string Resource { get; set; }

        /// <summary>
        ///     Subresource is one of the existing resource types.  "" means none.
        /// </summary>
        [YamlMember(Alias = "subresource")]
        [JsonProperty("subresource", NullValueHandling = NullValueHandling.Ignore)]
        public string Subresource { get; set; }

        /// <summary>
        ///     Version is the API Version of the Resource.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "version")]
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        /// <summary>
        ///     Group is the API Group of the Resource.  "*" means all.
        /// </summary>
        [YamlMember(Alias = "group")]
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }
    }
}
