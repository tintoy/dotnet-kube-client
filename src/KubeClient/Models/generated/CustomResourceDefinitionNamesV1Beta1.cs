using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionNames indicates the names to serve this CustomResourceDefinition
    /// </summary>
    public partial class CustomResourceDefinitionNamesV1Beta1
    {
        /// <summary>
        ///     Kind is the serialized kind of the resource.  It is normally CamelCase and singular.
        /// </summary>
        [YamlMember(Alias = "kind")]
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Include)]
        public string Kind { get; set; }

        /// <summary>
        ///     ListKind is the serialized kind of the list for this resource.  Defaults to &lt;kind&gt;List.
        /// </summary>
        [YamlMember(Alias = "listKind")]
        [JsonProperty("listKind", NullValueHandling = NullValueHandling.Ignore)]
        public string ListKind { get; set; }

        /// <summary>
        ///     Plural is the plural name of the resource to serve.  It must match the name of the CustomResourceDefinition-registration too: plural.group and it must be all lowercase.
        /// </summary>
        [YamlMember(Alias = "plural")]
        [JsonProperty("plural", NullValueHandling = NullValueHandling.Include)]
        public string Plural { get; set; }

        /// <summary>
        ///     Singular is the singular name of the resource.  It must be all lowercase  Defaults to lowercased &lt;kind&gt;
        /// </summary>
        [YamlMember(Alias = "singular")]
        [JsonProperty("singular", NullValueHandling = NullValueHandling.Ignore)]
        public string Singular { get; set; }

        /// <summary>
        ///     Categories is a list of grouped resources custom resources belong to (e.g. 'all')
        /// </summary>
        [YamlMember(Alias = "categories")]
        [JsonProperty("categories", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Categories { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Categories"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeCategories() => Categories.Count > 0;

        /// <summary>
        ///     ShortNames are short names for the resource.  It must be all lowercase.
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
