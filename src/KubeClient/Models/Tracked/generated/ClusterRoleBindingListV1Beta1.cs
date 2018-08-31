using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ClusterRoleBindingList is a collection of ClusterRoleBindings
    /// </summary>
    [KubeListItem("ClusterRoleBinding", "rbac.authorization.k8s.io/v1beta1")]
    [KubeObject("ClusterRoleBindingList", "rbac.authorization.k8s.io/v1beta1")]
    public partial class ClusterRoleBindingListV1Beta1 : Models.ClusterRoleBindingListV1Beta1, ITracked
    {
        /// <summary>
        ///     Items is a list of ClusterRoleBindings
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.ClusterRoleBindingV1Beta1> Items { get; } = new List<Models.ClusterRoleBindingV1Beta1>();
    }
}
