using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ComponentStatus (and ComponentStatusList) holds the cluster validation info.
    /// </summary>
    [KubeObject("ComponentStatus", "v1")]
    [KubeApi(KubeAction.List, "api/v1/componentstatuses")]
    [KubeApi(KubeAction.Get, "api/v1/componentstatuses/{name}")]
    public partial class ComponentStatusV1 : KubeResourceV1
    {
        /// <summary>
        ///     List of component conditions observed
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ComponentConditionV1> Conditions { get; } = new List<ComponentConditionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;
    }
}
