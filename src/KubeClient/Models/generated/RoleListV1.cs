using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleList is a collection of Roles
    /// </summary>
    [KubeListItem("Role", "v1")]
    [KubeObject("RoleList", "v1")]
    public partial class RoleListV1 : KubeResourceListV1<RoleV1>
    {
        /// <summary>
        ///     Items is a list of Roles
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<RoleV1> Items { get; } = new List<RoleV1>();
    }
}
