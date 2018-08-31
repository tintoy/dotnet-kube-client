using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     The node this Taint is attached to has the effect "effect" on any pod that that does not tolerate the Taint.
    /// </summary>
    public partial class TaintV1 : Models.TaintV1, ITracked
    {
        /// <summary>
        ///     TimeAdded represents the time at which the taint was added. It is only written for NoExecute taints.
        /// </summary>
        [JsonProperty("timeAdded")]
        [YamlMember(Alias = "timeAdded")]
        public override DateTime? TimeAdded
        {
            get
            {
                return base.TimeAdded;
            }
            set
            {
                base.TimeAdded = value;

                __ModifiedProperties__.Add("TimeAdded");
            }
        }


        /// <summary>
        ///     Required. The taint value corresponding to the taint key.
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
        ///     Required. The effect of the taint on pods that do not tolerate the taint. Valid effects are NoSchedule, PreferNoSchedule and NoExecute.
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
        ///     Required. The taint key to be applied to a node.
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
