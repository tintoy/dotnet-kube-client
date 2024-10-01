using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionStatus indicates the state of the CustomResourceDefinition
    /// </summary>
    public partial class CustomResourceDefinitionStatusV1
    {
        /// <summary>
        ///     acceptedNames are the names that are actually being used to serve discovery. They may be different than the names in spec.
        /// </summary>
        [YamlMember(Alias = "acceptedNames")]
        [JsonProperty("acceptedNames", NullValueHandling = NullValueHandling.Ignore)]
        public CustomResourceDefinitionNamesV1 AcceptedNames { get; set; }

        /// <summary>
        ///     conditions indicate state for particular aspects of a CustomResourceDefinition
        /// </summary>
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<CustomResourceDefinitionConditionV1> Conditions { get; } = new List<CustomResourceDefinitionConditionV1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;

        /// <summary>
        ///     storedVersions lists all versions of CustomResources that were ever persisted. Tracking these versions allows a migration path for stored versions in etcd. The field is mutable so a migration controller can finish a migration to another version (ensuring no old objects are left in storage), and then remove the rest of the versions from this list. Versions may not be removed from `spec.versions` while they exist in this list.
        /// </summary>
        [YamlMember(Alias = "storedVersions")]
        [JsonProperty("storedVersions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> StoredVersions { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="StoredVersions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeStoredVersions() => StoredVersions.Count > 0;
    }
}
