using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodResourceClaim references exactly one ResourceClaim, either directly or by naming a ResourceClaimTemplate which is then turned into a ResourceClaim for the pod.
    ///     
    ///     It adds a name to it that uniquely identifies the ResourceClaim inside the Pod. Containers that need access to the ResourceClaim reference it with this name.
    /// </summary>
    public partial class PodResourceClaimV1
    {
        /// <summary>
        ///     Name uniquely identifies this resource claim inside the pod. This must be a DNS_LABEL.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     ResourceClaimName is the name of a ResourceClaim object in the same namespace as this pod.
        ///     
        ///     Exactly one of ResourceClaimName and ResourceClaimTemplateName must be set.
        /// </summary>
        [YamlMember(Alias = "resourceClaimName")]
        [JsonProperty("resourceClaimName", NullValueHandling = NullValueHandling.Ignore)]
        public string ResourceClaimName { get; set; }

        /// <summary>
        ///     ResourceClaimTemplateName is the name of a ResourceClaimTemplate object in the same namespace as this pod.
        ///     
        ///     The template will be used to create a new ResourceClaim, which will be bound to this pod. When this pod is deleted, the ResourceClaim will also be deleted. The pod name and resource name, along with a generated component, will be used to form a unique name for the ResourceClaim, which will be recorded in pod.status.resourceClaimStatuses.
        ///     
        ///     This field is immutable and no changes will be made to the corresponding ResourceClaim by the control plane after creating the ResourceClaim.
        ///     
        ///     Exactly one of ResourceClaimName and ResourceClaimTemplateName must be set.
        /// </summary>
        [YamlMember(Alias = "resourceClaimTemplateName")]
        [JsonProperty("resourceClaimTemplateName", NullValueHandling = NullValueHandling.Ignore)]
        public string ResourceClaimTemplateName { get; set; }
    }
}
