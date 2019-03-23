using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicationControllerStatus represents the current status of a replication controller.
    /// </summary>
    public partial class ReplicationControllerStatusV1
    {
        /// <summary>
        ///     ObservedGeneration reflects the generation of the most recently observed replication controller.
        /// </summary>
        [YamlMember(Alias = "observedGeneration")]
        [JsonProperty("observedGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObservedGeneration { get; set; }

        /// <summary>
        ///     The number of available replicas (ready for at least minReadySeconds) for this replication controller.
        /// </summary>
        [YamlMember(Alias = "availableReplicas")]
        [JsonProperty("availableReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? AvailableReplicas { get; set; }

        /// <summary>
        ///     Represents the latest available observations of a replication controller's current state.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ReplicationControllerConditionV1> Conditions { get; } = new List<ReplicationControllerConditionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;

        /// <summary>
        ///     The number of pods that have labels matching the labels of the pod template of the replication controller.
        /// </summary>
        [YamlMember(Alias = "fullyLabeledReplicas")]
        [JsonProperty("fullyLabeledReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? FullyLabeledReplicas { get; set; }

        /// <summary>
        ///     The number of ready replicas for this replication controller.
        /// </summary>
        [YamlMember(Alias = "readyReplicas")]
        [JsonProperty("readyReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReadyReplicas { get; set; }

        /// <summary>
        ///     Replicas is the most recently oberved number of replicas. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller#what-is-a-replicationcontroller
        /// </summary>
        [YamlMember(Alias = "replicas")]
        [JsonProperty("replicas", NullValueHandling = NullValueHandling.Include)]
        public int Replicas { get; set; }
    }
}
