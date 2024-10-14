using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceCIDRStatus describes the current state of the ServiceCIDR.
    /// </summary>
    public partial class ServiceCIDRStatusV1Beta1
    {
        /// <summary>
        ///     conditions holds an array of metav1.Condition that describe the state of the ServiceCIDR. Current service state
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ConditionV1> Conditions { get; } = new List<ConditionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;
    }
}
