using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A list of StorageVersions.
    /// </summary>
    [KubeListItem("StorageVersion", "internal.apiserver.k8s.io/v1alpha1")]
    [KubeObject("StorageVersionList", "internal.apiserver.k8s.io/v1alpha1")]
    public partial class StorageVersionListV1Alpha1 : KubeResourceListV1<StorageVersionV1Alpha1>
    {
        /// <summary>
        ///     Items holds a list of StorageVersion
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<StorageVersionV1Alpha1> Items { get; } = new List<StorageVersionV1Alpha1>();
    }
}
