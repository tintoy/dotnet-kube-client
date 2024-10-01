using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     FlowSchemaStatus represents the current state of a FlowSchema.
    /// </summary>
    public partial class FlowSchemaStatusV1
    {
        /// <summary>
        ///     `conditions` is a list of the current states of FlowSchema.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<FlowSchemaConditionV1> Conditions { get; } = new List<FlowSchemaConditionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;
    }
}
