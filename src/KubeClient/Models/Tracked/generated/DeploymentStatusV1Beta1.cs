using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     DeploymentStatus is the most recently observed status of the Deployment.
    /// </summary>
    public partial class DeploymentStatusV1Beta1 : Models.DeploymentStatusV1Beta1
    {
        /// <summary>
        ///     The generation observed by the deployment controller.
        /// </summary>
        [JsonProperty("observedGeneration")]
        [YamlMember(Alias = "observedGeneration")]
        public override int ObservedGeneration
        {
            get
            {
                return base.ObservedGeneration;
            }
            set
            {
                base.ObservedGeneration = value;

                __ModifiedProperties__.Add("ObservedGeneration");
            }
        }


        /// <summary>
        ///     Total number of available pods (ready for at least minReadySeconds) targeted by this deployment.
        /// </summary>
        [JsonProperty("availableReplicas")]
        [YamlMember(Alias = "availableReplicas")]
        public override int AvailableReplicas
        {
            get
            {
                return base.AvailableReplicas;
            }
            set
            {
                base.AvailableReplicas = value;

                __ModifiedProperties__.Add("AvailableReplicas");
            }
        }


        /// <summary>
        ///     Represents the latest available observations of a deployment's current state.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.DeploymentConditionV1Beta1> Conditions { get; set; } = new List<Models.DeploymentConditionV1Beta1>();

        /// <summary>
        ///     Total number of ready pods targeted by this deployment.
        /// </summary>
        [JsonProperty("readyReplicas")]
        [YamlMember(Alias = "readyReplicas")]
        public override int ReadyReplicas
        {
            get
            {
                return base.ReadyReplicas;
            }
            set
            {
                base.ReadyReplicas = value;

                __ModifiedProperties__.Add("ReadyReplicas");
            }
        }


        /// <summary>
        ///     Total number of non-terminated pods targeted by this deployment (their labels match the selector).
        /// </summary>
        [JsonProperty("replicas")]
        [YamlMember(Alias = "replicas")]
        public override int Replicas
        {
            get
            {
                return base.Replicas;
            }
            set
            {
                base.Replicas = value;

                __ModifiedProperties__.Add("Replicas");
            }
        }


        /// <summary>
        ///     Total number of unavailable pods targeted by this deployment.
        /// </summary>
        [JsonProperty("unavailableReplicas")]
        [YamlMember(Alias = "unavailableReplicas")]
        public override int UnavailableReplicas
        {
            get
            {
                return base.UnavailableReplicas;
            }
            set
            {
                base.UnavailableReplicas = value;

                __ModifiedProperties__.Add("UnavailableReplicas");
            }
        }


        /// <summary>
        ///     Total number of non-terminated pods targeted by this deployment that have the desired template spec.
        /// </summary>
        [JsonProperty("updatedReplicas")]
        [YamlMember(Alias = "updatedReplicas")]
        public override int UpdatedReplicas
        {
            get
            {
                return base.UpdatedReplicas;
            }
            set
            {
                base.UpdatedReplicas = value;

                __ModifiedProperties__.Add("UpdatedReplicas");
            }
        }


        /// <summary>
        ///     Count of hash collisions for the Deployment. The Deployment controller uses this field as a collision avoidance mechanism when it needs to create the name for the newest ReplicaSet.
        /// </summary>
        [JsonProperty("collisionCount")]
        [YamlMember(Alias = "collisionCount")]
        public override int CollisionCount
        {
            get
            {
                return base.CollisionCount;
            }
            set
            {
                base.CollisionCount = value;

                __ModifiedProperties__.Add("CollisionCount");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
