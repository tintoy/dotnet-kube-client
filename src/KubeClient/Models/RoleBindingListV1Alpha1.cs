using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleBindingList is a collection of RoleBindings
    /// </summary>
    [KubeObject("RoleBindingList", "rbac.authorization.k8s.io/v1alpha1")]
    public class RoleBindingListV1Alpha1 : KubeResourceListV1<RoleBindingV1Alpha1>
    {
        /// <summary>
        ///     Items is a list of RoleBindings
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<RoleBindingV1Alpha1> Items { get; } = new List<RoleBindingV1Alpha1>();
    }
}
