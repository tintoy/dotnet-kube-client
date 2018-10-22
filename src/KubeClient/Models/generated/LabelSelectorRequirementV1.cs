using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A label selector requirement is a selector that contains values, a key, and an operator that relates the key and values.
    /// </summary>
    public partial class LabelSelectorRequirementV1
    {
        /// <summary>
        ///     operator represents a key's relationship to a set of values. Valid operators are In, NotIn, Exists and DoesNotExist.
        /// </summary>
        [YamlMember(Alias = "operator")]
        [JsonProperty("operator", NullValueHandling = NullValueHandling.Include)]
        public string Operator { get; set; }

        /// <summary>
        ///     values is an array of string values. If the operator is In or NotIn, the values array must be non-empty. If the operator is Exists or DoesNotExist, the values array must be empty. This array is replaced during a strategic merge patch.
        /// </summary>
        [YamlMember(Alias = "values")]
        [JsonProperty("values", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Values { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Values"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeValues() => Values.Count > 0;

        /// <summary>
        ///     key is the label key that the selector applies to.
        /// </summary>
        [YamlMember(Alias = "key")]
        [JsonProperty("key", NullValueHandling = NullValueHandling.Include)]
        [MergeStrategy(Key = "key")]
        public string Key { get; set; }
    }
}
