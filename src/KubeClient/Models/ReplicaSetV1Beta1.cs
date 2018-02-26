using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicaSet represents the configuration of a ReplicaSet.
    /// </summary>
    [KubeObject("ReplicaSet", "v1beta1")]
    public class ReplicaSetV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the specification of the desired behavior of the ReplicaSet. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        public ReplicaSetSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Status is the most recently observed status of the ReplicaSet. This data may be out of date by some window of time. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        public ReplicaSetStatusV1Beta1 Status { get; set; }
    }
}
