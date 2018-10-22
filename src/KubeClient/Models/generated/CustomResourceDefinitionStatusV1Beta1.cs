using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionStatus indicates the state of the CustomResourceDefinition
    /// </summary>
    public partial class CustomResourceDefinitionStatusV1Beta1
    {
        /// <summary>
        ///     AcceptedNames are the names that are actually being used to serve discovery They may be different than the names in spec.
        /// </summary>
        [YamlMember(Alias = "acceptedNames")]
        [JsonProperty("acceptedNames", NullValueHandling = NullValueHandling.Include)]
        public CustomResourceDefinitionNamesV1Beta1 AcceptedNames { get; set; }

        /// <summary>
        ///     Conditions indicate state for particular aspects of a CustomResourceDefinition
        /// </summary>
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<CustomResourceDefinitionConditionV1Beta1> Conditions { get; } = new List<CustomResourceDefinitionConditionV1Beta1>();

        /// <summary>
        ///     StoredVersions are all versions of CustomResources that were ever persisted. Tracking these versions allows a migration path for stored versions in etcd. The field is mutable so the migration controller can first finish a migration to another version (i.e. that no old objects are left in the storage), and then remove the rest of the versions from this list. None of the versions in this list can be removed from the spec.Versions field.
        /// </summary>
        [YamlMember(Alias = "storedVersions")]
        [JsonProperty("storedVersions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> StoredVersions { get; } = new List<string>();
    }
}
