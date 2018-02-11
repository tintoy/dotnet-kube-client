using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterRoleList is a collection of ClusterRoles
    /// </summary>
    [KubeResource("ClusterRoleList", "v1alpha1")]
    public class ClusterRoleListV1Alpha1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of ClusterRoles
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ClusterRoleV1Alpha1> Items { get; set; } = new List<ClusterRoleV1Alpha1>();
    }
}
