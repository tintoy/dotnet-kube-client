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
        ///     key is the label key that the selector applies to.
        /// </summary>
        [JsonProperty("key")]
        [YamlMember(Alias = "key")]
        [MergeStrategy(Key = "key")]
        public string Key { get; set; }

        /// <summary>
        ///     values is an array of string values. If the operator is In or NotIn, the values array must be non-empty. If the operator is Exists or DoesNotExist, the values array must be empty. This array is replaced during a strategic merge patch.
        /// </summary>
        [YamlMember(Alias = "values")]
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Values { get; set; } = new List<string>();

        /// <summary>
        ///     operator represents a key's relationship to a set of values. Valid operators are In, NotIn, Exists and DoesNotExist.
        /// </summary>
        [JsonProperty("operator")]
        [YamlMember(Alias = "operator")]
        public string Operator { get; set; }
    }
}
