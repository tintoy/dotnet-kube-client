using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     FieldSelectorRequirement is a selector that contains values, a key, and an operator that relates the key and values.
    /// </summary>
    public partial class FieldSelectorRequirementV1
    {
        /// <summary>
        ///     operator represents a key's relationship to a set of values. Valid operators are In, NotIn, Exists, DoesNotExist. The list of operators may grow in the future.
        /// </summary>
        [YamlMember(Alias = "operator")]
        [JsonProperty("operator", NullValueHandling = NullValueHandling.Include)]
        public string Operator { get; set; }

        /// <summary>
        ///     values is an array of string values. If the operator is In or NotIn, the values array must be non-empty. If the operator is Exists or DoesNotExist, the values array must be empty.
        /// </summary>
        [YamlMember(Alias = "values")]
        [JsonProperty("values", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Values { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Values"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeValues() => Values.Count > 0;

        /// <summary>
        ///     key is the field selector key that the requirement applies to.
        /// </summary>
        [YamlMember(Alias = "key")]
        [JsonProperty("key", NullValueHandling = NullValueHandling.Include)]
        public string Key { get; set; }
    }
}
