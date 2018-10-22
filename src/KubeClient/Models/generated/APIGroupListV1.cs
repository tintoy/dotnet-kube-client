using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     APIGroupList is a list of APIGroup, to allow clients to discover the API at /apis.
    /// </summary>
    public partial class APIGroupListV1 : KubeObjectV1
    {
        /// <summary>
        ///     groups is a list of APIGroup.
        /// </summary>
        [YamlMember(Alias = "groups")]
        [JsonProperty("groups", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<APIGroupV1> Groups { get; } = new List<APIGroupV1>();
    }
}
