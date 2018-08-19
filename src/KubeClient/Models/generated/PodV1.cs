using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Pod is a collection of containers that can run on a host. This resource is created by clients and scheduled onto hosts.
    /// </summary>
    [KubeObject("Pod", "v1")]
    public partial class PodV1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the pod. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        public PodSpecV1 Spec { get; set; }

        /// <summary>
        ///     Most recently observed status of the pod. This data may not be up to date. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        public PodStatusV1 Status { get; set; }
    }
}
