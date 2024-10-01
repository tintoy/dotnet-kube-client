using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Scheduling specifies the scheduling constraints for nodes supporting a RuntimeClass.
    /// </summary>
    public partial class SchedulingV1
    {
        /// <summary>
        ///     nodeSelector lists labels that must be present on nodes that support this RuntimeClass. Pods using this RuntimeClass can only be scheduled to a node matched by this selector. The RuntimeClass nodeSelector is merged with a pod's existing nodeSelector. Any conflicts will cause the pod to be rejected in admission.
        /// </summary>
        [YamlMember(Alias = "nodeSelector")]
        [JsonProperty("nodeSelector", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, string> NodeSelector { get; } = new Dictionary<string, string>();

        /// <summary>
        ///     Determine whether the <see cref="NodeSelector"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeNodeSelector() => NodeSelector.Count > 0;

        /// <summary>
        ///     tolerations are appended (excluding duplicates) to pods running with this RuntimeClass during admission, effectively unioning the set of nodes tolerated by the pod and the RuntimeClass.
        /// </summary>
        [YamlMember(Alias = "tolerations")]
        [JsonProperty("tolerations", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<TolerationV1> Tolerations { get; } = new List<TolerationV1>();

        /// <summary>
        ///     Determine whether the <see cref="Tolerations"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeTolerations() => Tolerations.Count > 0;
    }
}
