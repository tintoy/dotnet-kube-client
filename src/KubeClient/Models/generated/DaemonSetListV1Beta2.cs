using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSetList is a collection of daemon sets.
    /// </summary>
    [KubeListItem("DaemonSet", "apps/v1beta2")]
    [KubeObject("DaemonSetList", "apps/v1beta2")]
    public partial class DaemonSetListV1Beta2 : KubeResourceListV1<DaemonSetV1Beta2>
    {
        /// <summary>
        ///     A list of daemon sets.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<DaemonSetV1Beta2> Items { get; } = new List<DaemonSetV1Beta2>();
    }
}
