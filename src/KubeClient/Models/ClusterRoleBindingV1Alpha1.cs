using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterRoleBinding references a ClusterRole, but not contain it.  It can reference a ClusterRole in the global namespace, and adds who information via Subject.
    /// </summary>
    [KubeObject("ClusterRoleBinding", "rbac.authorization.k8s.io/v1alpha1")]
    public class ClusterRoleBindingV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     RoleRef can only reference a ClusterRole in the global namespace. If the RoleRef cannot be resolved, the Authorizer must return an error.
        /// </summary>
        [JsonProperty("roleRef")]
        public RoleRefV1Alpha1 RoleRef { get; set; }

        /// <summary>
        ///     Subjects holds references to the objects the role applies to.
        /// </summary>
        [JsonProperty("subjects", NullValueHandling = NullValueHandling.Ignore)]
        public List<SubjectV1Alpha1> Subjects { get; set; } = new List<SubjectV1Alpha1>();
    }
}
