using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceQuotaSpec defines the desired hard limits to enforce for Quota.
    /// </summary>
    public partial class ResourceQuotaSpecV1
    {
        /// <summary>
        ///     Hard is the set of desired hard limits for each named resource. More info: https://git.k8s.io/community/contributors/design-proposals/admission_control_resource_quota.md
        /// </summary>
        [YamlMember(Alias = "hard")]
        [JsonProperty("hard", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Dictionary<string, string> Hard { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     A collection of filters that must match each object tracked by a quota. If not specified, the quota matches all objects.
        /// </summary>
        [YamlMember(Alias = "scopes")]
        [JsonProperty("scopes", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Scopes { get; set; } = new List<string>();
    }
}
