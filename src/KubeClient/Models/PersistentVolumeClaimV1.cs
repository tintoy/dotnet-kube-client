using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeClaim is a user's request for and claim to a persistent volume
    /// </summary>
    [KubeResource("PersistentVolumeClaim", "v1")]
    public class PersistentVolumeClaimV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines the desired characteristics of a volume requested by a pod author. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [JsonProperty("spec")]
        public PersistentVolumeClaimSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status represents the current information/status of a persistent volume claim. Read-only. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [JsonProperty("status")]
        public PersistentVolumeClaimStatusV1 Status { get; set; }
    }
}
