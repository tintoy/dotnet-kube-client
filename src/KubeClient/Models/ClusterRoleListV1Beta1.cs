using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterRoleList is a collection of ClusterRoles
    /// </summary>
    [KubeResource("ClusterRoleList", "v1beta1")]
    public class ClusterRoleListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of ClusterRoles
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ClusterRoleV1Beta1> Items { get; set; } = new List<ClusterRoleV1Beta1>();
    }
}
