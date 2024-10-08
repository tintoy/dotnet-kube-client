using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Role is a namespaced, logical grouping of PolicyRules that can be referenced as a unit by a RoleBinding.
    /// </summary>
    [KubeObject("Role", "rbac.authorization.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/rbac.authorization.k8s.io/v1/roles")]
    [KubeApi(KubeAction.WatchList, "apis/rbac.authorization.k8s.io/v1/watch/roles")]
    [KubeApi(KubeAction.List, "apis/rbac.authorization.k8s.io/v1/namespaces/{namespace}/roles")]
    [KubeApi(KubeAction.Create, "apis/rbac.authorization.k8s.io/v1/namespaces/{namespace}/roles")]
    [KubeApi(KubeAction.Get, "apis/rbac.authorization.k8s.io/v1/namespaces/{namespace}/roles/{name}")]
    [KubeApi(KubeAction.Patch, "apis/rbac.authorization.k8s.io/v1/namespaces/{namespace}/roles/{name}")]
    [KubeApi(KubeAction.Delete, "apis/rbac.authorization.k8s.io/v1/namespaces/{namespace}/roles/{name}")]
    [KubeApi(KubeAction.Update, "apis/rbac.authorization.k8s.io/v1/namespaces/{namespace}/roles/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/rbac.authorization.k8s.io/v1/watch/namespaces/{namespace}/roles")]
    [KubeApi(KubeAction.DeleteCollection, "apis/rbac.authorization.k8s.io/v1/namespaces/{namespace}/roles")]
    [KubeApi(KubeAction.Watch, "apis/rbac.authorization.k8s.io/v1/watch/namespaces/{namespace}/roles/{name}")]
    public partial class RoleV1 : KubeResourceV1
    {
        /// <summary>
        ///     Rules holds all the PolicyRules for this Role
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PolicyRuleV1> Rules { get; } = new List<PolicyRuleV1>();

        /// <summary>
        ///     Determine whether the <see cref="Rules"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRules() => Rules.Count > 0;
    }
}
