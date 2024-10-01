using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     StorageVersionMigrationList is a collection of storage version migrations.
    /// </summary>
    [KubeListItem("StorageVersionMigration", "storagemigration.k8s.io/v1alpha1")]
    [KubeObject("StorageVersionMigrationList", "storagemigration.k8s.io/v1alpha1")]
    public partial class StorageVersionMigrationListV1Alpha1 : KubeResourceListV1<StorageVersionMigrationV1Alpha1>
    {
        /// <summary>
        ///     Items is the list of StorageVersionMigration
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<StorageVersionMigrationV1Alpha1> Items { get; } = new List<StorageVersionMigrationV1Alpha1>();
    }
}
