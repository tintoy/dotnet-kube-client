using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodDisruptionBudgetStatus represents information about the status of a PodDisruptionBudget. Status may trail the actual state of a system.
    /// </summary>
    public partial class PodDisruptionBudgetStatusV1Beta1 : Models.PodDisruptionBudgetStatusV1Beta1, ITracked
    {
        /// <summary>
        ///     Number of pod disruptions that are currently allowed.
        /// </summary>
        [JsonProperty("disruptionsAllowed")]
        [YamlMember(Alias = "disruptionsAllowed")]
        public override int DisruptionsAllowed
        {
            get
            {
                return base.DisruptionsAllowed;
            }
            set
            {
                base.DisruptionsAllowed = value;

                __ModifiedProperties__.Add("DisruptionsAllowed");
            }
        }


        /// <summary>
        ///     Most recent generation observed when updating this PDB status. PodDisruptionsAllowed and other status informatio is valid only if observedGeneration equals to PDB's object generation.
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
        ///     DisruptedPods contains information about pods whose eviction was processed by the API server eviction subresource handler but has not yet been observed by the PodDisruptionBudget controller. A pod will be in this map from the time when the API server processed the eviction request to the time when the pod is seen by PDB controller as having been marked for deletion (or after a timeout). The key in the map is the name of the pod and the value is the time when the API server processed the eviction request. If the deletion didn't occur and a pod is still there it will be removed from the list automatically by PodDisruptionBudget controller after some time. If everything goes smooth this map should be empty for the most of the time. Large number of entries in the map may indicate problems with pod deletions.
        /// </summary>
        [YamlMember(Alias = "disruptedPods")]
        [JsonProperty("disruptedPods", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, DateTime> DisruptedPods { get; set; } = new Dictionary<string, DateTime>();

        /// <summary>
        ///     total number of pods counted by this disruption budget
        /// </summary>
        [JsonProperty("expectedPods")]
        [YamlMember(Alias = "expectedPods")]
        public override int ExpectedPods
        {
            get
            {
                return base.ExpectedPods;
            }
            set
            {
                base.ExpectedPods = value;

                __ModifiedProperties__.Add("ExpectedPods");
            }
        }


        /// <summary>
        ///     current number of healthy pods
        /// </summary>
        [JsonProperty("currentHealthy")]
        [YamlMember(Alias = "currentHealthy")]
        public override int CurrentHealthy
        {
            get
            {
                return base.CurrentHealthy;
            }
            set
            {
                base.CurrentHealthy = value;

                __ModifiedProperties__.Add("CurrentHealthy");
            }
        }


        /// <summary>
        ///     minimum desired number of healthy pods
        /// </summary>
        [JsonProperty("desiredHealthy")]
        [YamlMember(Alias = "desiredHealthy")]
        public override int DesiredHealthy
        {
            get
            {
                return base.DesiredHealthy;
            }
            set
            {
                base.DesiredHealthy = value;

                __ModifiedProperties__.Add("DesiredHealthy");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
