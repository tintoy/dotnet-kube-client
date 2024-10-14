using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceClaimConsumerReference contains enough information to let you locate the consumer of a ResourceClaim. The user must be a resource in the same namespace as the ResourceClaim.
    /// </summary>
    public partial class ResourceClaimConsumerReferenceV1Alpha3
    {
        /// <summary>
        ///     UID identifies exactly one incarnation of the resource.
        /// </summary>
        [YamlMember(Alias = "uid")]
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Include)]
        public string Uid { get; set; }

        /// <summary>
        ///     Name is the name of resource being referenced.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Resource is the type of resource being referenced, for example "pods".
        /// </summary>
        [YamlMember(Alias = "resource")]
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Include)]
        public string Resource { get; set; }

        /// <summary>
        ///     APIGroup is the group for the resource being referenced. It is empty for the core API. This matches the group in the APIVersion that is used when creating the resources.
        /// </summary>
        [YamlMember(Alias = "apiGroup")]
        [JsonProperty("apiGroup", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiGroup { get; set; }
    }
}
