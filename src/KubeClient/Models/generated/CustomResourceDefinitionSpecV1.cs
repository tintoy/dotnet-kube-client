using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionSpec describes how a user wants their resource to appear
    /// </summary>
    public partial class CustomResourceDefinitionSpecV1
    {
        /// <summary>
        ///     scope indicates whether the defined custom resource is cluster- or namespace-scoped. Allowed values are `Cluster` and `Namespaced`.
        /// </summary>
        [YamlMember(Alias = "scope")]
        [JsonProperty("scope", NullValueHandling = NullValueHandling.Include)]
        public string Scope { get; set; }

        /// <summary>
        ///     conversion defines conversion settings for the CRD.
        /// </summary>
        [YamlMember(Alias = "conversion")]
        [JsonProperty("conversion", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceConversionV1 Conversion { get; set; }

        /// <summary>
        ///     group is the API group of the defined custom resource. The custom resources are served under `/apis/&lt;group&gt;/...`. Must match the name of the CustomResourceDefinition (in the form `&lt;names.plural&gt;.&lt;group&gt;`).
        /// </summary>
        [YamlMember(Alias = "group")]
        [JsonProperty("group", NullValueHandling = NullValueHandling.Include)]
        public string Group { get; set; }

        /// <summary>
        ///     names specify the resource and kind names for the custom resource.
        /// </summary>
        [YamlMember(Alias = "names")]
        [JsonProperty("names", NullValueHandling = NullValueHandling.Include)]
        public CustomResourceDefinitionNamesV1 Names { get; set; }

        /// <summary>
        ///     preserveUnknownFields indicates that object fields which are not specified in the OpenAPI schema should be preserved when persisting to storage. apiVersion, kind, metadata and known fields inside metadata are always preserved. This field is deprecated in favor of setting `x-preserve-unknown-fields` to true in `spec.versions[*].schema.openAPIV3Schema`. See https://kubernetes.io/docs/tasks/extend-kubernetes/custom-resources/custom-resource-definitions/#field-pruning for details.
        /// </summary>
        [YamlMember(Alias = "preserveUnknownFields")]
        [JsonProperty("preserveUnknownFields", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PreserveUnknownFields { get; set; }

        /// <summary>
        ///     versions is the list of all API versions of the defined custom resource. Version names are used to compute the order in which served versions are listed in API discovery. If the version string is "kube-like", it will sort above non "kube-like" version strings, which are ordered lexicographically. "Kube-like" versions start with a "v", then are followed by a number (the major version), then optionally the string "alpha" or "beta" and another number (the minor version). These are sorted first by GA &gt; beta &gt; alpha (where GA is a version with no suffix such as beta or alpha), and then by comparing major version, then minor version. An example sorted list of versions: v10, v2, v1, v11beta2, v10beta3, v3beta1, v12alpha1, v11alpha2, foo1, foo10.
        /// </summary>
        [YamlMember(Alias = "versions")]
        [JsonProperty("versions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<CustomResourceDefinitionVersionV1> Versions { get; } = new List<CustomResourceDefinitionVersionV1>();
    }
}
