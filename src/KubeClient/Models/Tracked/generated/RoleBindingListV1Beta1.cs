using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     RoleBindingList is a collection of RoleBindings
    /// </summary>
    [KubeListItem("RoleBinding", "rbac.authorization.k8s.io/v1beta1")]
    [KubeObject("RoleBindingList", "rbac.authorization.k8s.io/v1beta1")]
    public partial class RoleBindingListV1Beta1 : Models.RoleBindingListV1Beta1, ITracked
    {
        /// <summary>
        ///     Items is a list of RoleBindings
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.RoleBindingV1Beta1> Items { get; } = new List<Models.RoleBindingV1Beta1>();
    }
}
