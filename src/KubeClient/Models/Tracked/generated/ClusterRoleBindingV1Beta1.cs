using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ClusterRoleBinding references a ClusterRole, but not contain it.  It can reference a ClusterRole in the global namespace, and adds who information via Subject.
    /// </summary>
    [KubeObject("ClusterRoleBinding", "rbac.authorization.k8s.io/v1beta1")]
    public partial class ClusterRoleBindingV1Beta1 : Models.ClusterRoleBindingV1Beta1, ITracked
    {
        /// <summary>
        ///     RoleRef can only reference a ClusterRole in the global namespace. If the RoleRef cannot be resolved, the Authorizer must return an error.
        /// </summary>
        [JsonProperty("roleRef")]
        [YamlMember(Alias = "roleRef")]
        public override Models.RoleRefV1Beta1 RoleRef
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
        public override List<Models.SubjectV1Beta1> Subjects { get; set; } = new List<Models.SubjectV1Beta1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
