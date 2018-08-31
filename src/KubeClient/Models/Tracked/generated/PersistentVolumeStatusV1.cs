using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PersistentVolumeStatus is the current status of a persistent volume.
    /// </summary>
    public partial class PersistentVolumeStatusV1 : Models.PersistentVolumeStatusV1, ITracked
    {
        /// <summary>
        ///     A human-readable message indicating details about why the volume is in this state.
        /// </summary>
        [JsonProperty("message")]
        [YamlMember(Alias = "message")]
        public override string Message
        {
            get
            {
                return base.Message;
            }
            set
            {
                base.Message = value;

                __ModifiedProperties__.Add("Message");
            }
        }


        /// <summary>
        ///     Phase indicates if a volume is available, bound to a claim, or released by a claim. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#phase
        /// </summary>
        [JsonProperty("phase")]
        [YamlMember(Alias = "phase")]
        public override string Phase
        {
            get
            {
                return base.Phase;
            }
            set
            {
                base.Phase = value;

                __ModifiedProperties__.Add("Phase");
            }
        }


        /// <summary>
        ///     Reason is a brief CamelCase string that describes any failure and is meant for machine parsing and tidy display in the CLI.
        /// </summary>
        [JsonProperty("reason")]
        [YamlMember(Alias = "reason")]
        public override string Reason
        {
            get
            {
                return base.Reason;
            }
            set
            {
                base.Reason = value;

                __ModifiedProperties__.Add("Reason");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
