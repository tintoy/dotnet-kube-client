using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionNames indicates the names to serve this CustomResourceDefinition
    /// </summary>
    public partial class CustomResourceDefinitionNamesV1
    {
        /// <summary>
        ///     kind is the serialized kind of the resource. It is normally CamelCase and singular. Custom resource instances will use this value as the `kind` attribute in API calls.
        /// </summary>
        [YamlMember(Alias = "kind")]
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Include)]
        public string Kind { get; set; }

        /// <summary>
        ///     listKind is the serialized kind of the list for this resource. Defaults to "`kind`List".
        /// </summary>
        [YamlMember(Alias = "listKind")]
        [JsonProperty("listKind", NullValueHandling = NullValueHandling.Ignore)]
        public string ListKind { get; set; }

        /// <summary>
        ///     plural is the plural name of the resource to serve. The custom resources are served under `/apis/&lt;group&gt;/&lt;version&gt;/.../&lt;plural&gt;`. Must match the name of the CustomResourceDefinition (in the form `&lt;names.plural&gt;.&lt;group&gt;`). Must be all lowercase.
        /// </summary>
        [YamlMember(Alias = "plural")]
        [JsonProperty("plural", NullValueHandling = NullValueHandling.Include)]
        public string Plural { get; set; }

        /// <summary>
        ///     singular is the singular name of the resource. It must be all lowercase. Defaults to lowercased `kind`.
        /// </summary>
        [YamlMember(Alias = "singular")]
        [JsonProperty("singular", NullValueHandling = NullValueHandling.Ignore)]
        public string Singular { get; set; }

        /// <summary>
        ///     categories is a list of grouped resources this custom resource belongs to (e.g. 'all'). This is published in API discovery documents, and used by clients to support invocations like `kubectl get all`.
        /// </summary>
        [YamlMember(Alias = "categories")]
        [JsonProperty("categories", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Categories { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Categories"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeCategories() => Categories.Count > 0;

        /// <summary>
        ///     shortNames are short names for the resource, exposed in API discovery documents, and used by clients to support invocations like `kubectl get &lt;shortname&gt;`. It must be all lowercase.
        /// </summary>
        [YamlMember(Alias = "shortNames")]
        [JsonProperty("shortNames", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> ShortNames { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="ShortNames"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeShortNames() => ShortNames.Count > 0;
    }
}
