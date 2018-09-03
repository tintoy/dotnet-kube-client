using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicaSetStatus represents the current status of a ReplicaSet.
    /// </summary>
    public partial class ReplicaSetStatusV1Beta2
    {
        /// <summary>
        ///     Represents the latest available observations of a replica set's current state.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public List<ReplicaSetConditionV1Beta2> Conditions { get; set; } = new List<ReplicaSetConditionV1Beta2>();

        /// <summary>
        ///     ObservedGeneration reflects the generation of the most recently observed ReplicaSet.
        /// </summary>
        [JsonProperty("observedGeneration")]
        [YamlMember(Alias = "observedGeneration")]
        public int ObservedGeneration { get; set; }

        /// <summary>
        ///     The number of available replicas (ready for at least minReadySeconds) for this replica set.
        /// </summary>
        [JsonProperty("availableReplicas")]
        [YamlMember(Alias = "availableReplicas")]
        public int AvailableReplicas { get; set; }

        /// <summary>
        ///     Replicas is the most recently oberved number of replicas. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller/#what-is-a-replicationcontroller
        /// </summary>
        [JsonProperty("replicas")]
        [YamlMember(Alias = "replicas")]
        public int Replicas { get; set; }

        /// <summary>
        ///     The number of pods that have labels matching the labels of the pod template of the replicaset.
        /// </summary>
        [JsonProperty("fullyLabeledReplicas")]
        [YamlMember(Alias = "fullyLabeledReplicas")]
        public int FullyLabeledReplicas { get; set; }

        /// <summary>
        ///     The number of ready replicas for this replica set.
        /// </summary>
        [JsonProperty("readyReplicas")]
        [YamlMember(Alias = "readyReplicas")]
        public int ReadyReplicas { get; set; }
    }
}
