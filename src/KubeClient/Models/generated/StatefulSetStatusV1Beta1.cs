using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     StatefulSetStatus represents the current state of a StatefulSet.
    /// </summary>
    public partial class StatefulSetStatusV1Beta1
    {
        /// <summary>
        ///     currentRevision, if not empty, indicates the version of the StatefulSet used to generate Pods in the sequence [0,currentReplicas).
        /// </summary>
        [JsonProperty("currentRevision")]
        [YamlMember(Alias = "currentRevision")]
        public string CurrentRevision { get; set; }

        /// <summary>
        ///     observedGeneration is the most recent generation observed for this StatefulSet. It corresponds to the StatefulSet's generation, which is updated on mutation by the API Server.
        /// </summary>
        [JsonProperty("observedGeneration")]
        [YamlMember(Alias = "observedGeneration")]
        public int ObservedGeneration { get; set; }

        /// <summary>
        ///     updateRevision, if not empty, indicates the version of the StatefulSet used to generate Pods in the sequence [replicas-updatedReplicas,replicas)
        /// </summary>
        [JsonProperty("updateRevision")]
        [YamlMember(Alias = "updateRevision")]
        public string UpdateRevision { get; set; }

        /// <summary>
        ///     currentReplicas is the number of Pods created by the StatefulSet controller from the StatefulSet version indicated by currentRevision.
        /// </summary>
        [JsonProperty("currentReplicas")]
        [YamlMember(Alias = "currentReplicas")]
        public int CurrentReplicas { get; set; }

        /// <summary>
        ///     readyReplicas is the number of Pods created by the StatefulSet controller that have a Ready Condition.
        /// </summary>
        [JsonProperty("readyReplicas")]
        [YamlMember(Alias = "readyReplicas")]
        public int ReadyReplicas { get; set; }

        /// <summary>
        ///     replicas is the number of Pods created by the StatefulSet controller.
        /// </summary>
        [JsonProperty("replicas")]
        [YamlMember(Alias = "replicas")]
        public int Replicas { get; set; }

        /// <summary>
        ///     updatedReplicas is the number of Pods created by the StatefulSet controller from the StatefulSet version indicated by updateRevision.
        /// </summary>
        [JsonProperty("updatedReplicas")]
        [YamlMember(Alias = "updatedReplicas")]
        public int UpdatedReplicas { get; set; }
    }
}
