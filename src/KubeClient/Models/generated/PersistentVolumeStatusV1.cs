using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PersistentVolumeStatus is the current status of a persistent volume.
    /// </summary>
    public partial class PersistentVolumeStatusV1
    {
        /// <summary>
        ///     lastPhaseTransitionTime is the time the phase transitioned from one to another and automatically resets to current time everytime a volume phase transitions.
        /// </summary>
        [YamlMember(Alias = "lastPhaseTransitionTime")]
        [JsonProperty("lastPhaseTransitionTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastPhaseTransitionTime { get; set; }

        /// <summary>
        ///     message is a human-readable message indicating details about why the volume is in this state.
        /// </summary>
        [YamlMember(Alias = "message")]
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        ///     phase indicates if a volume is available, bound to a claim, or released by a claim. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#phase
        /// </summary>
        [YamlMember(Alias = "phase")]
        [JsonProperty("phase", NullValueHandling = NullValueHandling.Ignore)]
        public string Phase { get; set; }

        /// <summary>
        ///     reason is a brief CamelCase string that describes any failure and is meant for machine parsing and tidy display in the CLI.
        /// </summary>
        [YamlMember(Alias = "reason")]
        [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }
    }
}
