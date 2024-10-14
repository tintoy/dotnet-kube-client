using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionVersion describes a version for CRD.
    /// </summary>
    public partial class CustomResourceDefinitionVersionV1
    {
        /// <summary>
        ///     schema describes the schema used for validation, pruning, and defaulting of this version of the custom resource.
        /// </summary>
        [YamlMember(Alias = "schema")]
        [JsonProperty("schema", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceValidationV1 Schema { get; set; }

        /// <summary>
        ///     deprecated indicates this version of the custom resource API is deprecated. When set to true, API requests to this version receive a warning header in the server response. Defaults to false.
        /// </summary>
        [YamlMember(Alias = "deprecated")]
        [JsonProperty("deprecated", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Deprecated { get; set; }

        /// <summary>
        ///     served is a flag enabling/disabling this version from being served via REST APIs
        /// </summary>
        [YamlMember(Alias = "served")]
        [JsonProperty("served", NullValueHandling = NullValueHandling.Include)]
        public bool Served { get; set; }

        /// <summary>
        ///     name is the version name, e.g. “v1”, “v2beta1”, etc. The custom resources are served under this version at `/apis/&lt;group&gt;/&lt;version&gt;/...` if `served` is true.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     storage indicates this version should be used when persisting custom resources to storage. There must be exactly one version with storage=true.
        /// </summary>
        [YamlMember(Alias = "storage")]
        [JsonProperty("storage", NullValueHandling = NullValueHandling.Include)]
        public bool Storage { get; set; }

        /// <summary>
        ///     deprecationWarning overrides the default warning returned to API clients. May only be set when `deprecated` is true. The default warning indicates this version is deprecated and recommends use of the newest served version of equal or greater stability, if one exists.
        /// </summary>
        [YamlMember(Alias = "deprecationWarning")]
        [JsonProperty("deprecationWarning", NullValueHandling = NullValueHandling.Ignore)]
        public string DeprecationWarning { get; set; }

        /// <summary>
        ///     additionalPrinterColumns specifies additional columns returned in Table output. See https://kubernetes.io/docs/reference/using-api/api-concepts/#receiving-resources-as-tables for details. If no columns are specified, a single column displaying the age of the custom resource is used.
        /// </summary>
        [YamlMember(Alias = "additionalPrinterColumns")]
        [JsonProperty("additionalPrinterColumns", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<CustomResourceColumnDefinitionV1> AdditionalPrinterColumns { get; } = new List<CustomResourceColumnDefinitionV1>();

        /// <summary>
        ///     Determine whether the <see cref="AdditionalPrinterColumns"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAdditionalPrinterColumns() => AdditionalPrinterColumns.Count > 0;

        /// <summary>
        ///     selectableFields specifies paths to fields that may be used as field selectors. A maximum of 8 selectable fields are allowed. See https://kubernetes.io/docs/concepts/overview/working-with-objects/field-selectors
        /// </summary>
        [YamlMember(Alias = "selectableFields")]
        [JsonProperty("selectableFields", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<SelectableFieldV1> SelectableFields { get; } = new List<SelectableFieldV1>();

        /// <summary>
        ///     Determine whether the <see cref="SelectableFields"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSelectableFields() => SelectableFields.Count > 0;

        /// <summary>
        ///     subresources specify what subresources this version of the defined custom resource have.
        /// </summary>
        [YamlMember(Alias = "subresources")]
        [JsonProperty("subresources", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceSubresourcesV1 Subresources { get; set; }
    }
}
