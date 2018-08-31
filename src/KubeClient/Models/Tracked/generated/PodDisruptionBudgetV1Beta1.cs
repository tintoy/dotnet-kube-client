using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PodDisruptionBudget is an object to define the max disruption that can be caused to a collection of pods
    /// </summary>
    [KubeObject("PodDisruptionBudget", "policy/v1beta1")]
    public partial class PodDisruptionBudgetV1Beta1 : Models.PodDisruptionBudgetV1Beta1
    {
        /// <summary>
        ///     Specification of the desired behavior of the PodDisruptionBudget.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public override Models.PodDisruptionBudgetSpecV1Beta1 Spec
        {
            get
            {
                return base.Spec;
            }
            set
            {
                base.Spec = value;

                __ModifiedProperties__.Add("Spec");
            }
        }


        /// <summary>
        ///     Most recently observed status of the PodDisruptionBudget.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public override Models.PodDisruptionBudgetStatusV1Beta1 Status
        {
            get
            {
                return base.Status;
            }
            set
            {
                base.Status = value;

                __ModifiedProperties__.Add("Status");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
