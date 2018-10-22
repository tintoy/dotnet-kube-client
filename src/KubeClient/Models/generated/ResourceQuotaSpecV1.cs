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
        ///     hard is the set of desired hard limits for each named resource. More info: https://kubernetes.io/docs/concepts/policy/resource-quotas/
        /// </summary>
        [YamlMember(Alias = "hard")]
        [JsonProperty("hard", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> Hard { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="Hard"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeHard() => Hard.Count > 0;

        /// <summary>
        ///     scopeSelector is also a collection of filters like scopes that must match each object tracked by a quota but expressed using ScopeSelectorOperator in combination with possible values. For a resource to match, both scopes AND scopeSelector (if specified in spec), must be matched.
        /// </summary>
        [YamlMember(Alias = "scopeSelector")]
        [JsonProperty("scopeSelector", NullValueHandling = NullValueHandling.Ignore)]
        public ScopeSelectorV1 ScopeSelector { get; set; }

        /// <summary>
        ///     A collection of filters that must match each object tracked by a quota. If not specified, the quota matches all objects.
        /// </summary>
        [YamlMember(Alias = "scopes")]
        [JsonProperty("scopes", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Scopes { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Scopes"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeScopes() => Scopes.Count > 0;
    }
}
