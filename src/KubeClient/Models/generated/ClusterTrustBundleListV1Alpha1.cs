using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ClusterTrustBundleList is a collection of ClusterTrustBundle objects
    /// </summary>
    [KubeListItem("ClusterTrustBundle", "certificates.k8s.io/v1alpha1")]
    [KubeObject("ClusterTrustBundleList", "certificates.k8s.io/v1alpha1")]
    public partial class ClusterTrustBundleListV1Alpha1 : KubeResourceListV1<ClusterTrustBundleV1Alpha1>
    {
        /// <summary>
        ///     items is a collection of ClusterTrustBundle objects
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ClusterTrustBundleV1Alpha1> Items { get; } = new List<ClusterTrustBundleV1Alpha1>();
    }
}
