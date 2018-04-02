using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     DaemonSet represents the configuration of a daemon set.
    /// </summary>
    [KubeObject("DaemonSet", "apps/v1beta2")]
    public partial class DaemonSetV1Beta2 : KubeResourceV1
    {
        /// <summary>
        ///     The desired behavior of this daemon set. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("spec")]
        public DaemonSetSpecV1Beta2 Spec { get; set; }

        /// <summary>
        ///     The current status of this daemon set. This data may be out of date by some window of time. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#spec-and-status
        /// </summary>
        [JsonProperty("status")]
        public DaemonSetStatusV1Beta2 Status { get; set; }
    }
}
