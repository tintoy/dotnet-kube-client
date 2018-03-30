using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSetList is a collection of daemon sets.
    /// </summary>
    [KubeObject("DaemonSetList", "extensions/v1beta1")]
    public class DaemonSetListV1Beta1 : KubeResourceListV1<DaemonSetV1Beta1>
    {
        /// <summary>
        ///     A list of daemon sets.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<DaemonSetV1Beta1> Items { get; } = new List<DaemonSetV1Beta1>();
    }
}
