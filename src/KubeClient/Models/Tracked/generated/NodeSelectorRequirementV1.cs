using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     A node selector requirement is a selector that contains values, a key, and an operator that relates the key and values.
    /// </summary>
    public partial class NodeSelectorRequirementV1 : Models.NodeSelectorRequirementV1, ITracked
    {
        /// <summary>
        ///     Represents a key's relationship to a set of values. Valid operators are In, NotIn, Exists, DoesNotExist. Gt, and Lt.
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
        ///     An array of string values. If the operator is In or NotIn, the values array must be non-empty. If the operator is Exists or DoesNotExist, the values array must be empty. If the operator is Gt or Lt, the values array must have a single element, which will be interpreted as an integer. This array is replaced during a strategic merge patch.
        /// </summary>
        [YamlMember(Alias = "values")]
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Values { get; set; } = new List<string>();

        /// <summary>
        ///     The label key that the selector applies to.
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
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
