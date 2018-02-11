using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     LimitRange sets resource usage limits for each kind of resource in a Namespace.
    /// </summary>
    [KubeResource("LimitRange", "v1")]
    public class LimitRangeV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the limits enforced. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        public LimitRangeSpecV1 Spec { get; set; }
    }
}
