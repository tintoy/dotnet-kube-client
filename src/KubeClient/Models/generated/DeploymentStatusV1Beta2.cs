using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeploymentStatus is the most recently observed status of the Deployment.
    /// </summary>
    public partial class DeploymentStatusV1Beta2
    {
        /// <summary>
        ///     The generation observed by the deployment controller.
        /// </summary>
        [YamlMember(Alias = "observedGeneration")]
        [JsonProperty("observedGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObservedGeneration { get; set; }

        /// <summary>
        ///     Total number of available pods (ready for at least minReadySeconds) targeted by this deployment.
        /// </summary>
        [YamlMember(Alias = "availableReplicas")]
        [JsonProperty("availableReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? AvailableReplicas { get; set; }

        /// <summary>
        ///     Represents the latest available observations of a deployment's current state.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<DeploymentConditionV1Beta2> Conditions { get; } = new List<DeploymentConditionV1Beta2>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;

        /// <summary>
        ///     Total number of ready pods targeted by this deployment.
        /// </summary>
        [YamlMember(Alias = "readyReplicas")]
        [JsonProperty("readyReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReadyReplicas { get; set; }

        /// <summary>
        ///     Total number of non-terminated pods targeted by this deployment (their labels match the selector).
        /// </summary>
        [YamlMember(Alias = "replicas")]
        [JsonProperty("replicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? Replicas { get; set; }

        /// <summary>
        ///     Total number of unavailable pods targeted by this deployment. This is the total number of pods that are still required for the deployment to have 100% available capacity. They may either be pods that are running but not yet available or pods that still have not been created.
        /// </summary>
        [YamlMember(Alias = "unavailableReplicas")]
        [JsonProperty("unavailableReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? UnavailableReplicas { get; set; }

        /// <summary>
        ///     Total number of non-terminated pods targeted by this deployment that have the desired template spec.
        /// </summary>
        [YamlMember(Alias = "updatedReplicas")]
        [JsonProperty("updatedReplicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? UpdatedReplicas { get; set; }

        /// <summary>
        ///     Count of hash collisions for the Deployment. The Deployment controller uses this field as a collision avoidance mechanism when it needs to create the name for the newest ReplicaSet.
        /// </summary>
        [YamlMember(Alias = "collisionCount")]
        [JsonProperty("collisionCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? CollisionCount { get; set; }
    }
}
