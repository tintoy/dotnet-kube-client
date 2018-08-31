using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ClusterRoleBindingList is a collection of ClusterRoleBindings
    /// </summary>
    [KubeListItem("ClusterRoleBinding", "rbac.authorization.k8s.io/v1alpha1")]
    [KubeObject("ClusterRoleBindingList", "rbac.authorization.k8s.io/v1alpha1")]
    public partial class ClusterRoleBindingListV1Alpha1 : Models.ClusterRoleBindingListV1Alpha1
    {
        /// <summary>
        ///     Items is a list of ClusterRoleBindings
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.ClusterRoleBindingV1Alpha1> Items { get; } = new List<Models.ClusterRoleBindingV1Alpha1>();
    }
}
