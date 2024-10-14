using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LeaseCandidateSpec is a specification of a Lease.
    /// </summary>
    public partial class LeaseCandidateSpecV1Alpha1
    {
        /// <summary>
        ///     LeaseName is the name of the lease for which this candidate is contending. This field is immutable.
        /// </summary>
        [YamlMember(Alias = "leaseName")]
        [JsonProperty("leaseName", NullValueHandling = NullValueHandling.Include)]
        public string LeaseName { get; set; }

        /// <summary>
        ///     PingTime is the last time that the server has requested the LeaseCandidate to renew. It is only done during leader election to check if any LeaseCandidates have become ineligible. When PingTime is updated, the LeaseCandidate will respond by updating RenewTime.
        /// </summary>
        [YamlMember(Alias = "pingTime")]
        [JsonProperty("pingTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? PingTime { get; set; }

        /// <summary>
        ///     RenewTime is the time that the LeaseCandidate was last updated. Any time a Lease needs to do leader election, the PingTime field is updated to signal to the LeaseCandidate that they should update the RenewTime. Old LeaseCandidate objects are also garbage collected if it has been hours since the last renew. The PingTime field is updated regularly to prevent garbage collection for still active LeaseCandidates.
        /// </summary>
        [YamlMember(Alias = "renewTime")]
        [JsonProperty("renewTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? RenewTime { get; set; }

        /// <summary>
        ///     BinaryVersion is the binary version. It must be in a semver format without leading `v`. This field is required when strategy is "OldestEmulationVersion"
        /// </summary>
        [YamlMember(Alias = "binaryVersion")]
        [JsonProperty("binaryVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string BinaryVersion { get; set; }

        /// <summary>
        ///     EmulationVersion is the emulation version. It must be in a semver format without leading `v`. EmulationVersion must be less than or equal to BinaryVersion. This field is required when strategy is "OldestEmulationVersion"
        /// </summary>
        [YamlMember(Alias = "emulationVersion")]
        [JsonProperty("emulationVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string EmulationVersion { get; set; }

        /// <summary>
        ///     PreferredStrategies indicates the list of strategies for picking the leader for coordinated leader election. The list is ordered, and the first strategy supersedes all other strategies. The list is used by coordinated leader election to make a decision about the final election strategy. This follows as - If all clients have strategy X as the first element in this list, strategy X will be used. - If a candidate has strategy [X] and another candidate has strategy [Y, X], Y supersedes X and strategy Y
        ///       will be used.
        ///     - If a candidate has strategy [X, Y] and another candidate has strategy [Y, X], this is a user error and leader
        ///       election will not operate the Lease until resolved.
        ///     (Alpha) Using this field requires the CoordinatedLeaderElection feature gate to be enabled.
        /// </summary>
        [YamlMember(Alias = "preferredStrategies")]
        [JsonProperty("preferredStrategies", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> PreferredStrategies { get; } = new List<string>();
    }
}
