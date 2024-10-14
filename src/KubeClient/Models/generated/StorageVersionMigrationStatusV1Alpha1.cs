using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Status of the storage version migration.
    /// </summary>
    public partial class StorageVersionMigrationStatusV1Alpha1
    {
        /// <summary>
        ///     ResourceVersion to compare with the GC cache for performing the migration. This is the current resource version of given group, version and resource when kube-controller-manager first observes this StorageVersionMigration resource.
        /// </summary>
        [YamlMember(Alias = "resourceVersion")]
        [JsonProperty("resourceVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string ResourceVersion { get; set; }

        /// <summary>
        ///     The latest available observations of the migration's current state.
        /// </summary>
        [MergeStrategy(Key = "type")]
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<MigrationConditionV1Alpha1> Conditions { get; } = new List<MigrationConditionV1Alpha1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;
    }
}
