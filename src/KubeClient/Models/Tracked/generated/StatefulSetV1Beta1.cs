using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     StatefulSet represents a set of pods with consistent identities. Identities are defined as:
    ///      - Network: A single stable DNS and hostname.
    ///      - Storage: As many VolumeClaims as requested.
    ///     The StatefulSet guarantees that a given network identity will always map to the same storage identity.
    /// </summary>
    [KubeObject("StatefulSet", "apps/v1beta1")]
    public partial class StatefulSetV1Beta1 : Models.StatefulSetV1Beta1, ITracked
    {
        /// <summary>
        ///     Spec defines the desired identities of pods in this set.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public override Models.StatefulSetSpecV1Beta1 Spec
        {
            get
            {
                return base.Spec;
            }
            set
            {
                base.Spec = value;

                __ModifiedProperties__.Add("Spec");
            }
        }


        /// <summary>
        ///     Status is the current status of Pods in this StatefulSet. This data may be out of date by some window of time.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public override Models.StatefulSetStatusV1Beta1 Status
        {
            get
            {
                return base.Status;
            }
            set
            {
                base.Status = value;

                __ModifiedProperties__.Add("Status");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
