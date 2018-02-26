using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeStatus is the current status of a persistent volume.
    /// </summary>
    [KubeObject("PersistentVolumeStatus", "v1")]
    public class PersistentVolumeStatusV1
    {
        /// <summary>
        ///     A human-readable message indicating details about why the volume is in this state.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        ///     Phase indicates if a volume is available, bound to a claim, or released by a claim. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#phase
        /// </summary>
        [JsonProperty("phase")]
        public string Phase { get; set; }

        /// <summary>
        ///     Reason is a brief CamelCase string that describes any failure and is meant for machine parsing and tidy display in the CLI.
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
