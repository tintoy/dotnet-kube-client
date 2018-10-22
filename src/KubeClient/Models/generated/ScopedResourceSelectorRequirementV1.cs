using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A scoped-resource selector requirement is a selector that contains values, a scope name, and an operator that relates the scope name and values.
    /// </summary>
    public partial class ScopedResourceSelectorRequirementV1
    {
        /// <summary>
        ///     The name of the scope that the selector applies to.
        /// </summary>
        [YamlMember(Alias = "scopeName")]
        [JsonProperty("scopeName", NullValueHandling = NullValueHandling.Include)]
        public string ScopeName { get; set; }

        /// <summary>
        ///     Represents a scope's relationship to a set of values. Valid operators are In, NotIn, Exists, DoesNotExist.
        /// </summary>
        [YamlMember(Alias = "operator")]
        [JsonProperty("operator", NullValueHandling = NullValueHandling.Include)]
        public string Operator { get; set; }

        /// <summary>
        ///     An array of string values. If the operator is In or NotIn, the values array must be non-empty. If the operator is Exists or DoesNotExist, the values array must be empty. This array is replaced during a strategic merge patch.
        /// </summary>
        [YamlMember(Alias = "values")]
        [JsonProperty("values", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Values { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Values"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeValues() => Values.Count > 0;
    }
}
