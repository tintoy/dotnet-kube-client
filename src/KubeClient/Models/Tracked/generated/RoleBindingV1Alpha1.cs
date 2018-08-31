using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     RoleBinding references a role, but does not contain it.  It can reference a Role in the same namespace or a ClusterRole in the global namespace. It adds who information via Subjects and namespace information by which namespace it exists in.  RoleBindings in a given namespace only have effect in that namespace.
    /// </summary>
    [KubeObject("RoleBinding", "rbac.authorization.k8s.io/v1alpha1")]
    public partial class RoleBindingV1Alpha1 : Models.RoleBindingV1Alpha1, ITracked
    {
        /// <summary>
        ///     RoleRef can reference a Role in the current namespace or a ClusterRole in the global namespace. If the RoleRef cannot be resolved, the Authorizer must return an error.
        /// </summary>
        [JsonProperty("roleRef")]
        [YamlMember(Alias = "roleRef")]
        public override Models.RoleRefV1Alpha1 RoleRef
        {
            get
            {
                return base.RoleRef;
            }
            set
            {
                base.RoleRef = value;

                __ModifiedProperties__.Add("RoleRef");
            }
        }


        /// <summary>
        ///     Subjects holds references to the objects the role applies to.
        /// </summary>
        [YamlMember(Alias = "subjects")]
        [JsonProperty("subjects", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.SubjectV1Alpha1> Subjects { get; set; } = new List<Models.SubjectV1Alpha1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
