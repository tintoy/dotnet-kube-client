using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PodSchedulingContextStatus describes where resources for the Pod can be allocated.
    /// </summary>
    public partial class PodSchedulingContextStatusV1Alpha3
    {
        /// <summary>
        ///     ResourceClaims describes resource availability for each pod.spec.resourceClaim entry where the corresponding ResourceClaim uses "WaitForFirstConsumer" allocation mode.
        /// </summary>
        [YamlMember(Alias = "resourceClaims")]
        [JsonProperty("resourceClaims", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ResourceClaimSchedulingStatusV1Alpha3> ResourceClaims { get; } = new List<ResourceClaimSchedulingStatusV1Alpha3>();

        /// <summary>
        ///     Determine whether the <see cref="ResourceClaims"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeResourceClaims() => ResourceClaims.Count > 0;
    }
}
