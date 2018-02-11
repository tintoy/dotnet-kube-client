using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleList is a collection of Roles
    /// </summary>
    [KubeResource("RoleList", "v1alpha1")]
    public class RoleListV1Alpha1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of Roles
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<RoleV1Alpha1> Items { get; set; } = new List<RoleV1Alpha1>();
    }
}
