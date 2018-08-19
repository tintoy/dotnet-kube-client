using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ResourceQuota sets aggregate quota restrictions enforced per namespace
    /// </summary>
    [KubeObject("ResourceQuota", "v1")]
    public partial class ResourceQuotaV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the desired quota. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public ResourceQuotaSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status defines the actual enforced quota and its current usage. https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public ResourceQuotaStatusV1 Status { get; set; }
    }
}
