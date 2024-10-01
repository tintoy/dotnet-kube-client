using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     TypedLocalObjectReference contains enough information to let you locate the typed referenced object inside the same namespace.
    /// </summary>
    public partial class TypedLocalObjectReferenceV1
    {
        /// <summary>
        ///     Kind is the type of resource being referenced
        /// </summary>
        [YamlMember(Alias = "kind")]
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Include)]
        public string Kind { get; set; }

        /// <summary>
        ///     Name is the name of resource being referenced
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     APIGroup is the group for the resource being referenced. If APIGroup is not specified, the specified Kind must be in the core API group. For any other third-party types, APIGroup is required.
        /// </summary>
        [YamlMember(Alias = "apiGroup")]
        [JsonProperty("apiGroup", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiGroup { get; set; }
    }
}
