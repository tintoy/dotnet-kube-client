using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleBinding references a role, but does not contain it.  It can reference a Role in the same namespace or a ClusterRole in the global namespace. It adds who information via Subjects and namespace information by which namespace it exists in.  RoleBindings in a given namespace only have effect in that namespace.
    /// </summary>
    [KubeObject("RoleBinding", "rbac.authorization.k8s.io/v1alpha1")]
    [KubeApi(KubeAction.List, "apis/rbac.authorization.k8s.io/v1alpha1/rolebindings")]
    [KubeApi(KubeAction.WatchList, "apis/rbac.authorization.k8s.io/v1alpha1/watch/rolebindings")]
    [KubeApi(KubeAction.List, "apis/rbac.authorization.k8s.io/v1alpha1/namespaces/{namespace}/rolebindings")]
    [KubeApi(KubeAction.Create, "apis/rbac.authorization.k8s.io/v1alpha1/namespaces/{namespace}/rolebindings")]
    [KubeApi(KubeAction.Get, "apis/rbac.authorization.k8s.io/v1alpha1/namespaces/{namespace}/rolebindings/{name}")]
    [KubeApi(KubeAction.Patch, "apis/rbac.authorization.k8s.io/v1alpha1/namespaces/{namespace}/rolebindings/{name}")]
    [KubeApi(KubeAction.Delete, "apis/rbac.authorization.k8s.io/v1alpha1/namespaces/{namespace}/rolebindings/{name}")]
    [KubeApi(KubeAction.Update, "apis/rbac.authorization.k8s.io/v1alpha1/namespaces/{namespace}/rolebindings/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/rbac.authorization.k8s.io/v1alpha1/watch/namespaces/{namespace}/rolebindings")]
    [KubeApi(KubeAction.DeleteCollection, "apis/rbac.authorization.k8s.io/v1alpha1/namespaces/{namespace}/rolebindings")]
    [KubeApi(KubeAction.Watch, "apis/rbac.authorization.k8s.io/v1alpha1/watch/namespaces/{namespace}/rolebindings/{name}")]
    public partial class RoleBindingV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     RoleRef can reference a Role in the current namespace or a ClusterRole in the global namespace. If the RoleRef cannot be resolved, the Authorizer must return an error.
        /// </summary>
        [YamlMember(Alias = "roleRef")]
        [JsonProperty("roleRef", NullValueHandling = NullValueHandling.Include)]
        public RoleRefV1Alpha1 RoleRef { get; set; }

        /// <summary>
        ///     Subjects holds references to the objects the role applies to.
        /// </summary>
        [YamlMember(Alias = "subjects")]
        [JsonProperty("subjects", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<SubjectV1Alpha1> Subjects { get; } = new List<SubjectV1Alpha1>();

        /// <summary>
        ///     Determine whether the <see cref="Subjects"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeSubjects() => Subjects.Count > 0;
    }
}
