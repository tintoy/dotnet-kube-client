using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SelfSubjectAccessReviewSpec is a description of the access request.  Exactly one of ResourceAuthorizationAttributes and NonResourceAuthorizationAttributes must be set
    /// </summary>
    public partial class SelfSubjectAccessReviewSpecV1
    {
        /// <summary>
        ///     NonResourceAttributes describes information for a non-resource access request
        /// </summary>
        [JsonProperty("nonResourceAttributes")]
        [YamlMember(Alias = "nonResourceAttributes")]
        public NonResourceAttributesV1 NonResourceAttributes { get; set; }

        /// <summary>
        ///     ResourceAuthorizationAttributes describes information for a resource access request
        /// </summary>
        [JsonProperty("resourceAttributes")]
        [YamlMember(Alias = "resourceAttributes")]
        public ResourceAttributesV1 ResourceAttributes { get; set; }
    }
}
