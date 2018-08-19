using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     StatefulSet represents a set of pods with consistent identities. Identities are defined as:
    ///      - Network: A single stable DNS and hostname.
    ///      - Storage: As many VolumeClaims as requested.
    ///     The StatefulSet guarantees that a given network identity will always map to the same storage identity.
    /// </summary>
    [KubeObject("StatefulSet", "apps/v1beta1")]
    public partial class StatefulSetV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the desired identities of pods in this set.
        /// </summary>
        [JsonProperty("spec")]
        public StatefulSetSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Status is the current status of Pods in this StatefulSet. This data may be out of date by some window of time.
        /// </summary>
        [JsonProperty("status")]
        public StatefulSetStatusV1Beta1 Status { get; set; }
    }
}
