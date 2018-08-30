using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     APIResource specifies the name of a resource and whether it is namespaced.
    /// </summary>
    public partial class APIResourceV1
    {
        /// <summary>
        ///     kind is the kind for the resource (e.g. 'Foo' is the kind for a resource 'foo')
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public virtual string Kind { get; set; }

        /// <summary>
        ///     namespaced indicates if a resource is namespaced or not.
        /// </summary>
        [JsonProperty("namespaced")]
        [YamlMember(Alias = "namespaced")]
        public virtual bool Namespaced { get; set; }

        /// <summary>
        ///     name is the plural name of the resource.
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public virtual string Name { get; set; }

        /// <summary>
        ///     singularName is the singular name of the resource.  This allows clients to handle plural and singular opaquely. The singularName is more correct for reporting status on a single item and both singular and plural are allowed from the kubectl CLI interface.
        /// </summary>
        [JsonProperty("singularName")]
        [YamlMember(Alias = "singularName")]
        public virtual string SingularName { get; set; }

        /// <summary>
        ///     categories is a list of the grouped resources this resource belongs to (e.g. 'all')
        /// </summary>
        [YamlMember(Alias = "categories")]
        [JsonProperty("categories", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Categories { get; set; } = new List<string>();

        /// <summary>
        ///     shortNames is a list of suggested short names of the resource.
        /// </summary>
        [YamlMember(Alias = "shortNames")]
        [JsonProperty("shortNames", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> ShortNames { get; set; } = new List<string>();

        /// <summary>
        ///     verbs is a list of supported kube verbs (this includes get, list, watch, create, update, patch, delete, deletecollection, and proxy)
        /// </summary>
        [YamlMember(Alias = "verbs")]
        [JsonProperty("verbs", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Verbs { get; set; } = new List<string>();
    }
}
