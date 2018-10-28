using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterRoleList is a collection of ClusterRoles
    /// </summary>
    [KubeListItem("ClusterRole", "rbac.authorization.k8s.io/v1")]
    [KubeObject("ClusterRoleList", "rbac.authorization.k8s.io/v1")]
    public partial class ClusterRoleListV1 : KubeResourceListV1<ClusterRoleV1>
    {
        /// <summary>
        ///     Items is a list of ClusterRoles
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ClusterRoleV1> Items { get; } = new List<ClusterRoleV1>();
    }
}
