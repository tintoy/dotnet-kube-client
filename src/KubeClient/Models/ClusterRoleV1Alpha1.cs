using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterRole is a cluster level, logical grouping of PolicyRules that can be referenced as a unit by a RoleBinding or ClusterRoleBinding.
    /// </summary>
    [KubeObject("ClusterRole", "v1alpha1")]
    public class ClusterRoleV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     Rules holds all the PolicyRules for this ClusterRole
        /// </summary>
        [JsonProperty("rules", NullValueHandling = NullValueHandling.Ignore)]
        public List<PolicyRuleV1Alpha1> Rules { get; set; } = new List<PolicyRuleV1Alpha1>();
    }
}
