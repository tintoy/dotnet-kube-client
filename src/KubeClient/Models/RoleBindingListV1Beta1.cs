using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleBindingList is a collection of RoleBindings
    /// </summary>
    [KubeObject("RoleBindingList", "rbac.authorization.k8s.io/v1beta1")]
    public class RoleBindingListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of RoleBindings
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<RoleBindingV1Beta1> Items { get; set; } = new List<RoleBindingV1Beta1>();
    }
}
