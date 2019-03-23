using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     StatefulSetStatus represents the current state of a StatefulSet.
    /// </summary>
    public partial class StatefulSetStatusV1Beta2
    {
        /// <summary>
        ///     currentRevision, if not empty, indicates the version of the StatefulSet used to generate Pods in the sequence [0,currentReplicas).
        /// </summary>
        [YamlMember(Alias = "currentRevision")]
        [JsonProperty("currentRevision", NullValueHandling = NullValueHandling.Ignore)]
        public string CurrentRevision { get; set; }

        /// <summary>
        ///     observedGeneration is the most recent generation observed for this StatefulSet. It corresponds to the StatefulSet's generation, which is updated on mutation by the API Server.
        /// </summary>
        [YamlMember(Alias = "observedGeneration")]
        [JsonProperty("observedGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObservedGeneration { get; set; }

        /// <summary>
        ///     updateRevision, if not empty, indicates the version of the StatefulSet used to generate Pods in the sequence [replicas-updatedReplicas,replicas)
        /// </summary>
        [YamlMember(Alias = "updateRevision")]
        [JsonProperty("updateRevision", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdateRevision { get; set; }

        /// <summary>
        ///     Represents the latest available observations of a statefulset's current state.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<StatefulSetConditionV1Beta2> Conditions { get; } = new List<StatefulSetConditionV1Beta2>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;

        /// <summary>
        ///     currentReplicas is the number of Pods created by the StatefulSet controller from the StatefulSet version indicated by currentRevision.
        /// </summary>
        [YamlMember(Alias = "currentReplicas")]
        [JsonProperty("currentReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentReplicas { get; set; }

        /// <summary>
        ///     readyReplicas is the number of Pods created by the StatefulSet controller that have a Ready Condition.
        /// </summary>
        [YamlMember(Alias = "readyReplicas")]
        [JsonProperty("readyReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReadyReplicas { get; set; }

        /// <summary>
        ///     replicas is the number of Pods created by the StatefulSet controller.
        /// </summary>
        [YamlMember(Alias = "replicas")]
        [JsonProperty("replicas", NullValueHandling = NullValueHandling.Include)]
        public int Replicas { get; set; }

        /// <summary>
        ///     updatedReplicas is the number of Pods created by the StatefulSet controller from the StatefulSet version indicated by updateRevision.
        /// </summary>
        [YamlMember(Alias = "updatedReplicas")]
        [JsonProperty("updatedReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? UpdatedReplicas { get; set; }

        /// <summary>
        ///     collisionCount is the count of hash collisions for the StatefulSet. The StatefulSet controller uses this field as a collision avoidance mechanism when it needs to create the name for the newest ControllerRevision.
        /// </summary>
        [YamlMember(Alias = "collisionCount")]
        [JsonProperty("collisionCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? CollisionCount { get; set; }
    }
}
