using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleRef contains information that points to the role being used
    /// </summary>
    public partial class RoleRefV1Beta1
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
        ///     APIGroup is the group for the resource being referenced
        /// </summary>
        [YamlMember(Alias = "apiGroup")]
        [JsonProperty("apiGroup", NullValueHandling = NullValueHandling.Include)]
        public string ApiGroup { get; set; }
    }
}
