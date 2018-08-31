using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ClusterRoleList is a collection of ClusterRoles
    /// </summary>
    [KubeListItem("ClusterRole", "rbac.authorization.k8s.io/v1alpha1")]
    [KubeObject("ClusterRoleList", "rbac.authorization.k8s.io/v1alpha1")]
    public partial class ClusterRoleListV1Alpha1 : Models.ClusterRoleListV1Alpha1, ITracked
    {
        /// <summary>
        ///     Items is a list of ClusterRoles
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.ClusterRoleV1Alpha1> Items { get; } = new List<Models.ClusterRoleV1Alpha1>();
    }
}
