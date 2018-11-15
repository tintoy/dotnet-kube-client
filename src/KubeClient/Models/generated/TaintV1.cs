using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     The node this Taint is attached to has the "effect" on any pod that does not tolerate the Taint.
    /// </summary>
    public partial class TaintV1
    {
        /// <summary>
        ///     TimeAdded represents the time at which the taint was added. It is only written for NoExecute taints.
        /// </summary>
        [YamlMember(Alias = "timeAdded")]
        [JsonProperty("timeAdded", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? TimeAdded { get; set; }

        /// <summary>
        ///     Required. The taint value corresponding to the taint key.
        /// </summary>
        [YamlMember(Alias = "value")]
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        ///     Required. The effect of the taint on pods that do not tolerate the taint. Valid effects are NoSchedule, PreferNoSchedule and NoExecute.
        /// </summary>
        [YamlMember(Alias = "effect")]
        [JsonProperty("effect", NullValueHandling = NullValueHandling.Include)]
        public string Effect { get; set; }

        /// <summary>
        ///     Required. The taint key to be applied to a node.
        /// </summary>
        [YamlMember(Alias = "key")]
        [JsonProperty("key", NullValueHandling = NullValueHandling.Include)]
        public string Key { get; set; }
    }
}
