using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceQuotaSpec defines the desired hard limits to enforce for Quota.
    /// </summary>
    [KubeObject("ResourceQuotaSpec", "v1")]
    public class ResourceQuotaSpecV1
    {
        /// <summary>
        ///     Hard is the set of desired hard limits for each named resource. More info: https://git.k8s.io/community/contributors/design-proposals/admission_control_resource_quota.md
        /// </summary>
        [JsonProperty("hard", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Hard { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     A collection of filters that must match each object tracked by a quota. If not specified, the quota matches all objects.
        /// </summary>
        [JsonProperty("scopes", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Scopes { get; set; } = new List<string>();
    }
}
