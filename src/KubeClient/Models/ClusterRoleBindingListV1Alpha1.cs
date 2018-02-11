using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterRoleBindingList is a collection of ClusterRoleBindings
    /// </summary>
    [KubeResource("ClusterRoleBindingList", "v1alpha1")]
    public class ClusterRoleBindingListV1Alpha1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of ClusterRoleBindings
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ClusterRoleBindingV1Alpha1> Items { get; set; } = new List<ClusterRoleBindingV1Alpha1>();
    }
}
