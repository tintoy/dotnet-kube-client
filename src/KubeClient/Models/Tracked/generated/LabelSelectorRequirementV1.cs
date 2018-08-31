using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     A label selector requirement is a selector that contains values, a key, and an operator that relates the key and values.
    /// </summary>
    public partial class LabelSelectorRequirementV1 : Models.LabelSelectorRequirementV1
    {
        /// <summary>
        ///     operator represents a key's relationship to a set of values. Valid operators ard In, NotIn, Exists and DoesNotExist.
        /// </summary>
        [JsonProperty("operator")]
        [YamlMember(Alias = "operator")]
        public override string Operator
        {
            get
            {
                return base.Operator;
            }
            set
            {
                base.Operator = value;

                __ModifiedProperties__.Add("Operator");
            }
        }


        /// <summary>
        ///     values is an array of string values. If the operator is In or NotIn, the values array must be non-empty. If the operator is Exists or DoesNotExist, the values array must be empty. This array is replaced during a strategic merge patch.
        /// </summary>
        [YamlMember(Alias = "values")]
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Values { get; set; } = new List<string>();

        /// <summary>
        ///     key is the label key that the selector applies to.
        /// </summary>
        [JsonProperty("key")]
        [YamlMember(Alias = "key")]
        [MergeStrategy(Key = "key")]
        public override string Key
        {
            get
            {
                return base.Key;
            }
            set
            {
                base.Key = value;

                __ModifiedProperties__.Add("Key");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
