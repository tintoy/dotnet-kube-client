using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ParentReference describes a reference to a parent object.
    /// </summary>
    public partial class ParentReferenceV1Beta1
    {
        /// <summary>
        ///     Name is the name of the object being referenced.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Namespace is the namespace of the object being referenced.
        /// </summary>
        [YamlMember(Alias = "namespace")]
        [JsonProperty("namespace", NullValueHandling = NullValueHandling.Ignore)]
        public string Namespace { get; set; }

        /// <summary>
        ///     Resource is the resource of the object being referenced.
        /// </summary>
        [YamlMember(Alias = "resource")]
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Include)]
        public string Resource { get; set; }

        /// <summary>
        ///     Group is the group of the object being referenced.
        /// </summary>
        [YamlMember(Alias = "group")]
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }
    }
}
