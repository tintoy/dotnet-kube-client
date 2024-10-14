using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     API server instances report the versions they can decode and the version they encode objects to when persisting objects in the backend.
    /// </summary>
    public partial class StorageVersionStatusV1Alpha1
    {
        /// <summary>
        ///     If all API server instances agree on the same encoding storage version, then this field is set to that version. Otherwise this field is left empty. API servers should finish updating its storageVersionStatus entry before serving write operations, so that this field will be in sync with the reality.
        /// </summary>
        [YamlMember(Alias = "commonEncodingVersion")]
        [JsonProperty("commonEncodingVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string CommonEncodingVersion { get; set; }

        /// <summary>
        ///     The latest available observations of the storageVersion's state.
        /// </summary>
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<StorageVersionConditionV1Alpha1> Conditions { get; } = new List<StorageVersionConditionV1Alpha1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;

        /// <summary>
        ///     The reported versions per API server instance.
        /// </summary>
        [YamlMember(Alias = "storageVersions")]
        [JsonProperty("storageVersions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ServerStorageVersionV1Alpha1> StorageVersions { get; } = new List<ServerStorageVersionV1Alpha1>();

        /// <summary>
        ///     Determine whether the <see cref="StorageVersions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeStorageVersions() => StorageVersions.Count > 0;
    }
}
