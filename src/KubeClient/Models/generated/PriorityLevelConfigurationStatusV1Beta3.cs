using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PriorityLevelConfigurationStatus represents the current state of a "request-priority".
    /// </summary>
    public partial class PriorityLevelConfigurationStatusV1Beta3
    {
        /// <summary>
        ///     `conditions` is the current state of "request-priority".
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PriorityLevelConfigurationConditionV1Beta3> Conditions { get; } = new List<PriorityLevelConfigurationConditionV1Beta3>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;
    }
}
