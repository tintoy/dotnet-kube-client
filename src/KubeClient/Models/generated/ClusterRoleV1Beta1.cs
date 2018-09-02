using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterRole is a cluster level, logical grouping of PolicyRules that can be referenced as a unit by a RoleBinding or ClusterRoleBinding.
    /// </summary>
    [KubeObject("ClusterRole", "v1beta1")]
    [KubeApi("apis/rbac.authorization.k8s.io/v1beta1/clusterroles", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("apis/rbac.authorization.k8s.io/v1beta1/clusterroles/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
    [KubeApi("apis/rbac.authorization.k8s.io/v1beta1/watch/clusterroles", KubeAction.WatchList)]
    [KubeApi("apis/rbac.authorization.k8s.io/v1beta1/watch/clusterroles/{name}", KubeAction.Watch)]
    public partial class ClusterRoleV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Rules holds all the PolicyRules for this ClusterRole
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", NullValueHandling = NullValueHandling.Ignore)]
        public List<PolicyRuleV1Beta1> Rules { get; set; } = new List<PolicyRuleV1Beta1>();
    }
}
