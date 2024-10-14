using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Spec of the storage version migration.
    /// </summary>
    public partial class StorageVersionMigrationSpecV1Alpha1
    {
        /// <summary>
        ///     The resource that is being migrated. The migrator sends requests to the endpoint serving the resource. Immutable.
        /// </summary>
        [YamlMember(Alias = "resource")]
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Include)]
        public GroupVersionResourceV1Alpha1 Resource { get; set; }

        /// <summary>
        ///     The token used in the list options to get the next chunk of objects to migrate. When the .status.conditions indicates the migration is "Running", users can use this token to check the progress of the migration.
        /// </summary>
        [YamlMember(Alias = "continueToken")]
        [JsonProperty("continueToken", NullValueHandling = NullValueHandling.Ignore)]
        public string ContinueToken { get; set; }
    }
}
