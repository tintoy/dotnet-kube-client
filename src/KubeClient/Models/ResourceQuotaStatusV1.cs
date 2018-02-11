using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceQuotaStatus defines the enforced hard limits and observed use.
    /// </summary>
    [KubeResource("ResourceQuotaStatus", "v1")]
    public class ResourceQuotaStatusV1
    {
        /// <summary>
        ///     Hard is the set of enforced hard limits for each named resource. More info: https://git.k8s.io/community/contributors/design-proposals/admission_control_resource_quota.md
        /// </summary>
        [JsonProperty("hard", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Hard { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Used is the current observed total usage of the resource in the namespace.
        /// </summary>
        [JsonProperty("used", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Used { get; set; } = new Dictionary<string, string>();
    }
}
