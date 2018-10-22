using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceQuotaStatus defines the enforced hard limits and observed use.
    /// </summary>
    public partial class ResourceQuotaStatusV1
    {
        /// <summary>
        ///     Hard is the set of enforced hard limits for each named resource. More info: https://kubernetes.io/docs/concepts/policy/resource-quotas/
        /// </summary>
        [YamlMember(Alias = "hard")]
        [JsonProperty("hard", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Hard { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Hard"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeHard() => Hard.Count > 0;

        /// <summary>
        ///     Used is the current observed total usage of the resource in the namespace.
        /// </summary>
        [YamlMember(Alias = "used")]
        [JsonProperty("used", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Used { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Used"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeUsed() => Used.Count > 0;
    }
}
