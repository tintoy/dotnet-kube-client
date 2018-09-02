using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleList is a collection of Roles
    /// </summary>
    [KubeListItem("Role", "v1beta1")]
    [KubeObject("RoleList", "v1beta1")]
    public partial class RoleListV1Beta1 : KubeResourceListV1<RoleV1Beta1>
    {
        /// <summary>
        ///     Items is a list of Roles
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<RoleV1Beta1> Items { get; } = new List<RoleV1Beta1>();
    }
}
