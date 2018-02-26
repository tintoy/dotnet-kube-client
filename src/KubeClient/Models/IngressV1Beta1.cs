using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     Ingress is a collection of rules that allow inbound connections to reach the endpoints defined by a backend. An Ingress can be configured to give services externally-reachable urls, load balance traffic, terminate SSL, offer name based virtual hosting etc.
    /// </summary>
    [KubeObject("Ingress", "v1beta1")]
    public class IngressV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec is the desired state of the Ingress. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        public IngressSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Status is the current state of the Ingress. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        public IngressStatusV1Beta1 Status { get; set; }
    }
}
