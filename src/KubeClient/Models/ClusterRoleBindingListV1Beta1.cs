using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterRoleBindingList is a collection of ClusterRoleBindings
    /// </summary>
    [KubeObject("ClusterRoleBindingList", "rbac.authorization.k8s.io/v1beta1")]
    public class ClusterRoleBindingListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of ClusterRoleBindings
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ClusterRoleBindingV1Beta1> Items { get; set; } = new List<ClusterRoleBindingV1Beta1>();
    }
}
