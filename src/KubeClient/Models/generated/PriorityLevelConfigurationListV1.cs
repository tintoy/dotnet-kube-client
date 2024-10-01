using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PriorityLevelConfigurationList is a list of PriorityLevelConfiguration objects.
    /// </summary>
    [KubeListItem("PriorityLevelConfiguration", "flowcontrol.apiserver.k8s.io/v1")]
    [KubeObject("PriorityLevelConfigurationList", "flowcontrol.apiserver.k8s.io/v1")]
    public partial class PriorityLevelConfigurationListV1 : KubeResourceListV1<PriorityLevelConfigurationV1>
    {
        /// <summary>
        ///     `items` is a list of request-priorities.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PriorityLevelConfigurationV1> Items { get; } = new List<PriorityLevelConfigurationV1>();
    }
}
