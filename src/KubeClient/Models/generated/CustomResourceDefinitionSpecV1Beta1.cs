using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionSpec describes how a user wants their resource to appear
    /// </summary>
    public partial class CustomResourceDefinitionSpecV1Beta1
    {
        /// <summary>
        ///     Scope indicates whether this resource is cluster or namespace scoped.  Default is namespaced
        /// </summary>
        [YamlMember(Alias = "scope")]
        [JsonProperty("scope", NullValueHandling = NullValueHandling.Include)]
        public string Scope { get; set; }

        /// <summary>
        ///     Validation describes the validation methods for CustomResources
        /// </summary>
        [YamlMember(Alias = "validation")]
        [JsonProperty("validation", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceValidationV1Beta1 Validation { get; set; }

        /// <summary>
        ///     Version is the version this resource belongs in Should be always first item in Versions field if provided. Optional, but at least one of Version or Versions must be set. Deprecated: Please use `Versions`.
        /// </summary>
        [YamlMember(Alias = "version")]
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        /// <summary>
        ///     Group is the group this resource belongs in
        /// </summary>
        [YamlMember(Alias = "group")]
        [JsonProperty("group", NullValueHandling = NullValueHandling.Include)]
        public string Group { get; set; }

        /// <summary>
        ///     AdditionalPrinterColumns are additional columns shown e.g. in kubectl next to the name. Defaults to a created-at column.
        /// </summary>
        [YamlMember(Alias = "additionalPrinterColumns")]
        [JsonProperty("additionalPrinterColumns", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<CustomResourceColumnDefinitionV1Beta1> AdditionalPrinterColumns { get; } = new List<CustomResourceColumnDefinitionV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="AdditionalPrinterColumns"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAdditionalPrinterColumns() => AdditionalPrinterColumns.Count > 0;

        /// <summary>
        ///     Names are the names used to describe this custom resource
        /// </summary>
        [YamlMember(Alias = "names")]
        [JsonProperty("names", NullValueHandling = NullValueHandling.Include)]
        public CustomResourceDefinitionNamesV1Beta1 Names { get; set; }

        /// <summary>
        ///     Subresources describes the subresources for CustomResources
        /// </summary>
        [YamlMember(Alias = "subresources")]
        [JsonProperty("subresources", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceSubresourcesV1Beta1 Subresources { get; set; }

        /// <summary>
        ///     Versions is the list of all supported versions for this resource. If Version field is provided, this field is optional. Validation: All versions must use the same validation schema for now. i.e., top level Validation field is applied to all of these versions. Order: The version name will be used to compute the order. If the version string is "kube-like", it will sort above non "kube-like" version strings, which are ordered lexicographically. "Kube-like" versions start with a "v", then are followed by a number (the major version), then optionally the string "alpha" or "beta" and another number (the minor version). These are sorted first by GA &gt; beta &gt; alpha (where GA is a version with no suffix such as beta or alpha), and then by comparing major version, then minor version. An example sorted list of versions: v10, v2, v1, v11beta2, v10beta3, v3beta1, v12alpha1, v11alpha2, foo1, foo10.
        /// </summary>
        [YamlMember(Alias = "versions")]
        [JsonProperty("versions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<CustomResourceDefinitionVersionV1Beta1> Versions { get; } = new List<CustomResourceDefinitionVersionV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Versions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeVersions() => Versions.Count > 0;
    }
}
