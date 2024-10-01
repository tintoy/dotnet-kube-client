using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceClaim references one entry in PodSpec.ResourceClaims.
    /// </summary>
    public partial class ResourceClaimV1
    {
        /// <summary>
        ///     Name must match the name of one entry in pod.spec.resourceClaims of the Pod where this field is used. It makes that resource available inside a container.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     Request is the name chosen for a request in the referenced claim. If empty, everything from the claim is made available, otherwise only the result of this request.
        /// </summary>
        [YamlMember(Alias = "request")]
        [JsonProperty("request", NullValueHandling = NullValueHandling.Ignore)]
        public string Request { get; set; }
    }
}
