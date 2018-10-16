using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterRoleBindingList is a collection of ClusterRoleBindings
    /// </summary>
    [KubeListItem("ClusterRoleBinding", "v1")]
    [KubeObject("ClusterRoleBindingList", "v1")]
    public partial class ClusterRoleBindingListV1 : KubeResourceListV1<ClusterRoleBindingV1>
    {
        /// <summary>
        ///     Items is a list of ClusterRoleBindings
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ClusterRoleBindingV1> Items { get; } = new List<ClusterRoleBindingV1>();
    }
}
