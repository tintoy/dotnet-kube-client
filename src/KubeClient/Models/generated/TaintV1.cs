using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     The node this Taint is attached to has the effect "effect" on any pod that that does not tolerate the Taint.
    /// </summary>
    public partial class TaintV1
    {
        /// <summary>
        ///     TimeAdded represents the time at which the taint was added. It is only written for NoExecute taints.
        /// </summary>
        [JsonProperty("timeAdded")]
        [YamlMember(Alias = "timeAdded")]
        public DateTime? TimeAdded { get; set; }

        /// <summary>
        ///     Required. The taint value corresponding to the taint key.
        /// </summary>
        [JsonProperty("value")]
        [YamlMember(Alias = "value")]
        public string Value { get; set; }

        /// <summary>
        ///     Required. The effect of the taint on pods that do not tolerate the taint. Valid effects are NoSchedule, PreferNoSchedule and NoExecute.
        /// </summary>
        [JsonProperty("effect")]
        [YamlMember(Alias = "effect")]
        public string Effect { get; set; }

        /// <summary>
        ///     Required. The taint key to be applied to a node.
        /// </summary>
        [JsonProperty("key")]
        [StrategicMergeKey("key")]
        [YamlMember(Alias = "key")]
        public string Key { get; set; }
    }
}
