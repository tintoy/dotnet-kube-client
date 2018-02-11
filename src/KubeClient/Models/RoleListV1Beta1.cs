using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     RoleList is a collection of Roles
    /// </summary>
    [KubeResource("RoleList", "v1beta1")]
    public class RoleListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Items is a list of Roles
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<RoleV1Beta1> Items { get; set; } = new List<RoleV1Beta1>();
    }
}
