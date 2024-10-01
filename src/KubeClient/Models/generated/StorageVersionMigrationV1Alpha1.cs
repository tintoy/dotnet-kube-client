using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     StorageVersionMigration represents a migration of stored data to the latest storage version.
    /// </summary>
    [KubeObject("StorageVersionMigration", "storagemigration.k8s.io/v1alpha1")]
    [KubeApi(KubeAction.List, "apis/storagemigration.k8s.io/v1alpha1/storageversionmigrations")]
    [KubeApi(KubeAction.Create, "apis/storagemigration.k8s.io/v1alpha1/storageversionmigrations")]
    [KubeApi(KubeAction.Get, "apis/storagemigration.k8s.io/v1alpha1/storageversionmigrations/{name}")]
    [KubeApi(KubeAction.Patch, "apis/storagemigration.k8s.io/v1alpha1/storageversionmigrations/{name}")]
    [KubeApi(KubeAction.Delete, "apis/storagemigration.k8s.io/v1alpha1/storageversionmigrations/{name}")]
    [KubeApi(KubeAction.Update, "apis/storagemigration.k8s.io/v1alpha1/storageversionmigrations/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/storagemigration.k8s.io/v1alpha1/watch/storageversionmigrations")]
    [KubeApi(KubeAction.DeleteCollection, "apis/storagemigration.k8s.io/v1alpha1/storageversionmigrations")]
    [KubeApi(KubeAction.Get, "apis/storagemigration.k8s.io/v1alpha1/storageversionmigrations/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/storagemigration.k8s.io/v1alpha1/watch/storageversionmigrations/{name}")]
    [KubeApi(KubeAction.Patch, "apis/storagemigration.k8s.io/v1alpha1/storageversionmigrations/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/storagemigration.k8s.io/v1alpha1/storageversionmigrations/{name}/status")]
    public partial class StorageVersionMigrationV1Alpha1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the migration.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public StorageVersionMigrationSpecV1Alpha1 Spec { get; set; }

        /// <summary>
        ///     Status of the migration.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public StorageVersionMigrationStatusV1Alpha1 Status { get; set; }
    }
}
