using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     The pod this Toleration is attached to tolerates any taint that matches the triple &lt;key,value,effect&gt; using the matching operator &lt;operator&gt;.
    /// </summary>
    public partial class TolerationV1 : Models.TolerationV1, ITracked
    {
        /// <summary>
        ///     Value is the taint value the toleration matches to. If the operator is Exists, the value should be empty, otherwise just a regular string.
        /// </summary>
        [JsonProperty("value")]
        [YamlMember(Alias = "value")]
        public override string Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;

                __ModifiedProperties__.Add("Value");
            }
        }


        /// <summary>
        ///     Operator represents a key's relationship to the value. Valid operators are Exists and Equal. Defaults to Equal. Exists is equivalent to wildcard for value, so that a pod can tolerate all taints of a particular category.
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
        ///     TolerationSeconds represents the period of time the toleration (which must be of effect NoExecute, otherwise this field is ignored) tolerates the taint. By default, it is not set, which means tolerate the taint forever (do not evict). Zero and negative values will be treated as 0 (evict immediately) by the system.
        /// </summary>
        [JsonProperty("tolerationSeconds")]
        [YamlMember(Alias = "tolerationSeconds")]
        public override int TolerationSeconds
        {
            get
            {
                return base.TolerationSeconds;
            }
            set
            {
                base.TolerationSeconds = value;

                __ModifiedProperties__.Add("TolerationSeconds");
            }
        }


        /// <summary>
        ///     Effect indicates the taint effect to match. Empty means match all taint effects. When specified, allowed values are NoSchedule, PreferNoSchedule and NoExecute.
        /// </summary>
        [JsonProperty("effect")]
        [YamlMember(Alias = "effect")]
        public override string Effect
        {
            get
            {
                return base.Effect;
            }
            set
            {
                base.Effect = value;

                __ModifiedProperties__.Add("Effect");
            }
        }


        /// <summary>
        ///     Key is the taint key that the toleration applies to. Empty means match all taint keys. If the key is empty, operator must be Exists; this combination means to match all values and all keys.
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
