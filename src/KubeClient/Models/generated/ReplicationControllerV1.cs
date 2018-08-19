using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ReplicationController represents the configuration of a replication controller.
    /// </summary>
    [KubeObject("ReplicationController", "v1")]
    public partial class ReplicationControllerV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the specification of the desired behavior of the replication controller. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public ReplicationControllerSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status is the most recently observed status of the replication controller. This data may be out of date by some window of time. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public ReplicationControllerStatusV1 Status { get; set; }
    }
}
