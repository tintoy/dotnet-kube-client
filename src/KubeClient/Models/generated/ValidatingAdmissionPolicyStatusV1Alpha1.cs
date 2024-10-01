using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ValidatingAdmissionPolicyStatus represents the status of a ValidatingAdmissionPolicy.
    /// </summary>
    public partial class ValidatingAdmissionPolicyStatusV1Alpha1
    {
        /// <summary>
        ///     The results of type checking for each expression. Presence of this field indicates the completion of the type checking.
        /// </summary>
        [YamlMember(Alias = "typeChecking")]
        [JsonProperty("typeChecking", NullValueHandling = NullValueHandling.Ignore)]
        public TypeCheckingV1Alpha1 TypeChecking { get; set; }

        /// <summary>
        ///     The generation observed by the controller.
        /// </summary>
        [YamlMember(Alias = "observedGeneration")]
        [JsonProperty("observedGeneration", NullValueHandling = NullValueHandling.Ignore)]
        public long? ObservedGeneration { get; set; }

        /// <summary>
        ///     The conditions represent the latest available observations of a policy's current state.
        /// </summary>
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ConditionV1> Conditions { get; } = new List<ConditionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;
    }
}
