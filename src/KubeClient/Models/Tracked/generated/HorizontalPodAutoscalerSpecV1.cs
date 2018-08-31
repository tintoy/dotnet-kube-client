using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     specification of a horizontal pod autoscaler.
    /// </summary>
    public partial class HorizontalPodAutoscalerSpecV1 : Models.HorizontalPodAutoscalerSpecV1, ITracked
    {
        /// <summary>
        ///     target average CPU utilization (represented as a percentage of requested CPU) over all the pods; if not specified the default autoscaling policy will be used.
        /// </summary>
        [JsonProperty("targetCPUUtilizationPercentage")]
        [YamlMember(Alias = "targetCPUUtilizationPercentage")]
        public override int? TargetCPUUtilizationPercentage
        {
            get
            {
                return base.TargetCPUUtilizationPercentage;
            }
            set
            {
                base.TargetCPUUtilizationPercentage = value;

                __ModifiedProperties__.Add("TargetCPUUtilizationPercentage");
            }
        }


        /// <summary>
        ///     reference to scaled resource; horizontal pod autoscaler will learn the current resource consumption and will set the desired number of pods by using its Scale subresource.
        /// </summary>
        [JsonProperty("scaleTargetRef")]
        [YamlMember(Alias = "scaleTargetRef")]
        public override Models.CrossVersionObjectReferenceV1 ScaleTargetRef
        {
            get
            {
                return base.ScaleTargetRef;
            }
            set
            {
                base.ScaleTargetRef = value;

                __ModifiedProperties__.Add("ScaleTargetRef");
            }
        }


        /// <summary>
        ///     upper limit for the number of pods that can be set by the autoscaler; cannot be smaller than MinReplicas.
        /// </summary>
        [JsonProperty("maxReplicas")]
        [YamlMember(Alias = "maxReplicas")]
        public override int MaxReplicas
        {
            get
            {
                return base.MaxReplicas;
            }
            set
            {
                base.MaxReplicas = value;

                __ModifiedProperties__.Add("MaxReplicas");
            }
        }


        /// <summary>
        ///     lower limit for the number of pods that can be set by the autoscaler, default 1.
        /// </summary>
        [JsonProperty("minReplicas")]
        [YamlMember(Alias = "minReplicas")]
        public override int MinReplicas
        {
            get
            {
                return base.MinReplicas;
            }
            set
            {
                base.MinReplicas = value;

                __ModifiedProperties__.Add("MinReplicas");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
