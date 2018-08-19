using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        [JsonProperty("observedGeneration")]
        public int ObservedGeneration { get; set; }

        /// <summary>
        ///     The number of available replicas (ready for at least minReadySeconds) for this replication controller.
        /// </summary>
        [JsonProperty("availableReplicas")]
        public int AvailableReplicas { get; set; }

        /// <summary>
        ///     Represents the latest available observations of a replication controller's current state.
        /// </summary>
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public List<ReplicationControllerConditionV1> Conditions { get; set; } = new List<ReplicationControllerConditionV1>();

        /// <summary>
        ///     The number of pods that have labels matching the labels of the pod template of the replication controller.
        /// </summary>
        [JsonProperty("fullyLabeledReplicas")]
        public int FullyLabeledReplicas { get; set; }

        /// <summary>
        ///     The number of ready replicas for this replication controller.
        /// </summary>
        [JsonProperty("readyReplicas")]
        public int ReadyReplicas { get; set; }

        /// <summary>
        ///     Replicas is the most recently oberved number of replicas. More info: https://kubernetes.io/docs/concepts/workloads/controllers/replicationcontroller#what-is-a-replicationcontroller
        /// </summary>
        [JsonProperty("replicas")]
        public int Replicas { get; set; }
    }
}
