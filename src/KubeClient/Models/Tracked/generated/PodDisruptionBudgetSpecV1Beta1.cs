using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodDisruptionBudgetSpec is a description of a PodDisruptionBudget.
    /// </summary>
    public partial class PodDisruptionBudgetSpecV1Beta1 : Models.PodDisruptionBudgetSpecV1Beta1, ITracked
    {
        /// <summary>
        ///     An eviction is allowed if at most "maxUnavailable" pods selected by "selector" are unavailable after the eviction, i.e. even in absence of the evicted pod. For example, one can prevent all voluntary evictions by specifying 0. This is a mutually exclusive setting with "minAvailable".
        /// </summary>
        [JsonProperty("maxUnavailable")]
        [YamlMember(Alias = "maxUnavailable")]
        public override string MaxUnavailable
        {
            get
            {
                return base.MaxUnavailable;
            }
            set
            {
                base.MaxUnavailable = value;

                __ModifiedProperties__.Add("MaxUnavailable");
            }
        }


        /// <summary>
        ///     An eviction is allowed if at least "minAvailable" pods selected by "selector" will still be available after the eviction, i.e. even in the absence of the evicted pod.  So for example you can prevent all voluntary evictions by specifying "100%".
        /// </summary>
        [JsonProperty("minAvailable")]
        [YamlMember(Alias = "minAvailable")]
        public override string MinAvailable
        {
            get
            {
                return base.MinAvailable;
            }
            set
            {
                base.MinAvailable = value;

                __ModifiedProperties__.Add("MinAvailable");
            }
        }


        /// <summary>
        ///     Label query over pods whose evictions are managed by the disruption budget.
        /// </summary>
        [JsonProperty("selector")]
        [YamlMember(Alias = "selector")]
        public override Models.LabelSelectorV1 Selector
        {
            get
            {
                return base.Selector;
            }
            set
            {
                base.Selector = value;

                __ModifiedProperties__.Add("Selector");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
