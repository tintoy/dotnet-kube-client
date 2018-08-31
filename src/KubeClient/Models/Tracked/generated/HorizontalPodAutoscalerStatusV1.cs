using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     current status of a horizontal pod autoscaler
    /// </summary>
    public partial class HorizontalPodAutoscalerStatusV1 : Models.HorizontalPodAutoscalerStatusV1, ITracked
    {
        /// <summary>
        ///     current average CPU utilization over all pods, represented as a percentage of requested CPU, e.g. 70 means that an average pod is using now 70% of its requested CPU.
        /// </summary>
        [JsonProperty("currentCPUUtilizationPercentage")]
        [YamlMember(Alias = "currentCPUUtilizationPercentage")]
        public override int CurrentCPUUtilizationPercentage
        {
            get
            {
                return base.CurrentCPUUtilizationPercentage;
            }
            set
            {
                base.CurrentCPUUtilizationPercentage = value;

                __ModifiedProperties__.Add("CurrentCPUUtilizationPercentage");
            }
        }


        /// <summary>
        ///     last time the HorizontalPodAutoscaler scaled the number of pods; used by the autoscaler to control how often the number of pods is changed.
        /// </summary>
        [JsonProperty("lastScaleTime")]
        [YamlMember(Alias = "lastScaleTime")]
        public override DateTime? LastScaleTime
        {
            get
            {
                return base.LastScaleTime;
            }
            set
            {
                base.LastScaleTime = value;

                __ModifiedProperties__.Add("LastScaleTime");
            }
        }


        /// <summary>
        ///     most recent generation observed by this autoscaler.
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
        ///     current number of replicas of pods managed by this autoscaler.
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
        ///     desired number of replicas of pods managed by this autoscaler.
        /// </summary>
        [JsonProperty("desiredReplicas")]
        [YamlMember(Alias = "desiredReplicas")]
        public override int DesiredReplicas
        {
            get
            {
                return base.DesiredReplicas;
            }
            set
            {
                base.DesiredReplicas = value;

                __ModifiedProperties__.Add("DesiredReplicas");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
