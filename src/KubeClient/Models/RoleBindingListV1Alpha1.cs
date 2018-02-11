using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleBindingList is a collection of RoleBindings
    /// </summary>
    [KubeResource("RoleBindingList", "v1alpha1")]
    public class RoleBindingListV1Alpha1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of RoleBindings
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<RoleBindingV1Alpha1> Items { get; set; } = new List<RoleBindingV1Alpha1>();
    }
}
