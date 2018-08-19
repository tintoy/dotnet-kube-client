using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolume (PV) is a storage resource provisioned by an administrator. It is analogous to a node. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes
    /// </summary>
    [KubeObject("PersistentVolume", "v1")]
    public partial class PersistentVolumeV1 : KubeResourceV1
    {
        /// <summary>
        ///     Spec defines a specification of a persistent volume owned by the cluster. Provisioned by an administrator. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistent-volumes
        /// </summary>
        [JsonProperty("spec")]
        public PersistentVolumeSpecV1 Spec { get; set; }

        /// <summary>
        ///     Status represents the current information/status for the persistent volume. Populated by the system. Read-only. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistent-volumes
        /// </summary>
        [JsonProperty("status")]
        public PersistentVolumeStatusV1 Status { get; set; }
    }
}
