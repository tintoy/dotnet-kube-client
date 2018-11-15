using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterRole is a cluster level, logical grouping of PolicyRules that can be referenced as a unit by a RoleBinding or ClusterRoleBinding.
    /// </summary>
    [KubeObject("ClusterRole", "rbac.authorization.k8s.io/v1beta1")]
    [KubeApi(KubeAction.List, "apis/rbac.authorization.k8s.io/v1beta1/clusterroles")]
    [KubeApi(KubeAction.Create, "apis/rbac.authorization.k8s.io/v1beta1/clusterroles")]
    [KubeApi(KubeAction.Get, "apis/rbac.authorization.k8s.io/v1beta1/clusterroles/{name}")]
    [KubeApi(KubeAction.Patch, "apis/rbac.authorization.k8s.io/v1beta1/clusterroles/{name}")]
    [KubeApi(KubeAction.Delete, "apis/rbac.authorization.k8s.io/v1beta1/clusterroles/{name}")]
    [KubeApi(KubeAction.Update, "apis/rbac.authorization.k8s.io/v1beta1/clusterroles/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/rbac.authorization.k8s.io/v1beta1/watch/clusterroles")]
    [KubeApi(KubeAction.DeleteCollection, "apis/rbac.authorization.k8s.io/v1beta1/clusterroles")]
    [KubeApi(KubeAction.Watch, "apis/rbac.authorization.k8s.io/v1beta1/watch/clusterroles/{name}")]
    public partial class ClusterRoleV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     AggregationRule is an optional field that describes how to build the Rules for this ClusterRole. If AggregationRule is set, then the Rules are controller managed and direct changes to Rules will be stomped by the controller.
        /// </summary>
        [YamlMember(Alias = "aggregationRule")]
        [JsonProperty("aggregationRule", NullValueHandling = NullValueHandling.Ignore)]
        public AggregationRuleV1Beta1 AggregationRule { get; set; }

        /// <summary>
        ///     Rules holds all the PolicyRules for this ClusterRole
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PolicyRuleV1Beta1> Rules { get; } = new List<PolicyRuleV1Beta1>();
    }
}
