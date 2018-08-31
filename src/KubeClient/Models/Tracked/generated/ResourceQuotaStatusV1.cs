using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     ResourceQuotaStatus defines the enforced hard limits and observed use.
    /// </summary>
    public partial class ResourceQuotaStatusV1 : Models.ResourceQuotaStatusV1
    {
        /// <summary>
        ///     Hard is the set of enforced hard limits for each named resource. More info: https://git.k8s.io/community/contributors/design-proposals/admission_control_resource_quota.md
        /// </summary>
        [YamlMember(Alias = "hard")]
        [JsonProperty("hard", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Hard { get; set; } = new Dictionary<string, string>();

        /// <summary>
        ///     Used is the current observed total usage of the resource in the namespace.
        /// </summary>
        [YamlMember(Alias = "used")]
        [JsonProperty("used", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, string> Used { get; set; } = new Dictionary<string, string>();
    }
}
