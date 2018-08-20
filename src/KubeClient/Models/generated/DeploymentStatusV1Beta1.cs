using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DeploymentStatus is the most recently observed status of the Deployment.
    /// </summary>
    public partial class DeploymentStatusV1Beta1
    {
        /// <summary>
        ///     The generation observed by the deployment controller.
        /// </summary>
        [JsonProperty("observedGeneration")]
        [YamlMember(Alias = "observedGeneration")]
        public int ObservedGeneration { get; set; }

        /// <summary>
        ///     Total number of available pods (ready for at least minReadySeconds) targeted by this deployment.
        /// </summary>
        [JsonProperty("availableReplicas")]
        [YamlMember(Alias = "availableReplicas")]
        public int AvailableReplicas { get; set; }

        /// <summary>
        ///     Represents the latest available observations of a deployment's current state.
        /// </summary>
        [StrategicMergePatch("type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public List<DeploymentConditionV1Beta1> Conditions { get; set; } = new List<DeploymentConditionV1Beta1>();

        /// <summary>
        ///     Total number of ready pods targeted by this deployment.
        /// </summary>
        [JsonProperty("readyReplicas")]
        [YamlMember(Alias = "readyReplicas")]
        public int ReadyReplicas { get; set; }

        /// <summary>
        ///     Total number of non-terminated pods targeted by this deployment (their labels match the selector).
        /// </summary>
        [JsonProperty("replicas")]
        [YamlMember(Alias = "replicas")]
        public int Replicas { get; set; }

        /// <summary>
        ///     Total number of unavailable pods targeted by this deployment.
        /// </summary>
        [JsonProperty("unavailableReplicas")]
        [YamlMember(Alias = "unavailableReplicas")]
        public int UnavailableReplicas { get; set; }

        /// <summary>
        ///     Total number of non-terminated pods targeted by this deployment that have the desired template spec.
        /// </summary>
        [JsonProperty("updatedReplicas")]
        [YamlMember(Alias = "updatedReplicas")]
        public int UpdatedReplicas { get; set; }

        /// <summary>
        ///     Count of hash collisions for the Deployment. The Deployment controller uses this field as a collision avoidance mechanism when it needs to create the name for the newest ReplicaSet.
        /// </summary>
        [JsonProperty("collisionCount")]
        [YamlMember(Alias = "collisionCount")]
        public int CollisionCount { get; set; }
    }
}
