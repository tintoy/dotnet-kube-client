using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     RoleBindingList is a collection of RoleBindings
    /// </summary>
    [KubeListItem("RoleBinding", "rbac.authorization.k8s.io/v1alpha1")]
    [KubeObject("RoleBindingList", "rbac.authorization.k8s.io/v1alpha1")]
    public partial class RoleBindingListV1Alpha1 : Models.RoleBindingListV1Alpha1
    {
        /// <summary>
        ///     Items is a list of RoleBindings
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.RoleBindingV1Alpha1> Items { get; } = new List<Models.RoleBindingV1Alpha1>();
    }
}
