using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LeaseCandidateList is a list of Lease objects.
    /// </summary>
    [KubeListItem("LeaseCandidate", "coordination.k8s.io/v1alpha1")]
    [KubeObject("LeaseCandidateList", "coordination.k8s.io/v1alpha1")]
    public partial class LeaseCandidateListV1Alpha1 : KubeResourceListV1<LeaseCandidateV1Alpha1>
    {
        /// <summary>
        ///     items is a list of schema objects.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<LeaseCandidateV1Alpha1> Items { get; } = new List<LeaseCandidateV1Alpha1>();
    }
}
