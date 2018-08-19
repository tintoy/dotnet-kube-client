using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleList is a collection of Roles
    /// </summary>
    [KubeListItem("Role", "rbac.authorization.k8s.io/v1alpha1")]
    [KubeObject("RoleList", "rbac.authorization.k8s.io/v1alpha1")]
    public partial class RoleListV1Alpha1 : KubeResourceListV1<RoleV1Alpha1>
    {
        /// <summary>
        ///     Items is a list of Roles
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<RoleV1Alpha1> Items { get; } = new List<RoleV1Alpha1>();
    }
}
