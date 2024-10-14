using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LeaseList is a list of Lease objects.
    /// </summary>
    [KubeListItem("Lease", "coordination.k8s.io/v1")]
    [KubeObject("LeaseList", "coordination.k8s.io/v1")]
    public partial class LeaseListV1 : KubeResourceListV1<LeaseV1>
    {
        /// <summary>
        ///     items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<LeaseV1> Items { get; } = new List<LeaseV1>();
    }
}
