using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSetList is a collection of daemon sets.
    /// </summary>
    [KubeListItem("DaemonSet", "extensions/v1beta1")]
    [KubeObject("DaemonSetList", "extensions/v1beta1")]
    public partial class DaemonSetListV1Beta1 : KubeResourceListV1<DaemonSetV1Beta1>
    {
        /// <summary>
        ///     A list of daemon sets.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<DaemonSetV1Beta1> Items { get; } = new List<DaemonSetV1Beta1>();
    }
}
