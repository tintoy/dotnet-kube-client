using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PriorityLevelConfigurationList is a list of PriorityLevelConfiguration objects.
    /// </summary>
    [KubeListItem("PriorityLevelConfiguration", "flowcontrol.apiserver.k8s.io/v1beta3")]
    [KubeObject("PriorityLevelConfigurationList", "flowcontrol.apiserver.k8s.io/v1beta3")]
    public partial class PriorityLevelConfigurationListV1Beta3 : KubeResourceListV1<PriorityLevelConfigurationV1Beta3>
    {
        /// <summary>
        ///     `items` is a list of request-priorities.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<PriorityLevelConfigurationV1Beta3> Items { get; } = new List<PriorityLevelConfigurationV1Beta3>();
    }
}
