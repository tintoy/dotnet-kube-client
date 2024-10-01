using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceClaimTemplateSpec contains the metadata and fields for a ResourceClaim.
    /// </summary>
    public partial class ResourceClaimTemplateSpecV1Alpha3
    {
        /// <summary>
        ///     ObjectMeta may contain labels and annotations that will be copied into the PVC when creating it. No other fields are allowed and will be rejected during validation.
        /// </summary>
        [YamlMember(Alias = "metadata")]
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectMetaV1 Metadata { get; set; }

        /// <summary>
        ///     Spec for the ResourceClaim. The entire content is copied unchanged into the ResourceClaim that gets created from this template. The same fields as in a ResourceClaim are also valid here.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public ResourceClaimSpecV1Alpha3 Spec { get; set; }
    }
}
