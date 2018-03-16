using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleBindingList is a collection of RoleBindings
    /// </summary>
    [KubeObject("RoleBindingList", "rbac.authorization.k8s.io/v1beta1")]
    public class RoleBindingListV1Beta1 : KubeResourceListV1<RoleBindingV1Beta1>
    {
        /// <summary>
        ///     Items is a list of RoleBindings
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<RoleBindingV1Beta1> Items { get; } = new List<RoleBindingV1Beta1>();
    }
}
