using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     StatefulSetStatus represents the current state of a StatefulSet.
    /// </summary>
    public partial class StatefulSetStatusV1Beta1 : Models.StatefulSetStatusV1Beta1, ITracked
    {
        /// <summary>
        ///     currentRevision, if not empty, indicates the version of the StatefulSet used to generate Pods in the sequence [0,currentReplicas).
        /// </summary>
        [JsonProperty("currentRevision")]
        [YamlMember(Alias = "currentRevision")]
        public override string CurrentRevision
        {
            get
            {
                return base.CurrentRevision;
            }
            set
            {
                base.CurrentRevision = value;

                __ModifiedProperties__.Add("CurrentRevision");
            }
        }


        /// <summary>
        ///     observedGeneration is the most recent generation observed for this StatefulSet. It corresponds to the StatefulSet's generation, which is updated on mutation by the API Server.
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
        ///     updateRevision, if not empty, indicates the version of the StatefulSet used to generate Pods in the sequence [replicas-updatedReplicas,replicas)
        /// </summary>
        [JsonProperty("updateRevision")]
        [YamlMember(Alias = "updateRevision")]
        public override string UpdateRevision
        {
            get
            {
                return base.UpdateRevision;
            }
            set
            {
                base.UpdateRevision = value;

                __ModifiedProperties__.Add("UpdateRevision");
            }
        }


        /// <summary>
        ///     currentReplicas is the number of Pods created by the StatefulSet controller from the StatefulSet version indicated by currentRevision.
        /// </summary>
        [JsonProperty("currentReplicas")]
        [YamlMember(Alias = "currentReplicas")]
        public override int CurrentReplicas
        {
            get
            {
                return base.CurrentReplicas;
            }
            set
            {
                base.CurrentReplicas = value;

                __ModifiedProperties__.Add("CurrentReplicas");
            }
        }


        /// <summary>
        ///     readyReplicas is the number of Pods created by the StatefulSet controller that have a Ready Condition.
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
        ///     replicas is the number of Pods created by the StatefulSet controller.
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
        ///     updatedReplicas is the number of Pods created by the StatefulSet controller from the StatefulSet version indicated by updateRevision.
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
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
