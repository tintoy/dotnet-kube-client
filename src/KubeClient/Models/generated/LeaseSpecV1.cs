using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     LeaseSpec is a specification of a Lease.
    /// </summary>
    public partial class LeaseSpecV1
    {
        /// <summary>
        ///     acquireTime is a time when the current lease was acquired.
        /// </summary>
        [YamlMember(Alias = "acquireTime")]
        [JsonProperty("acquireTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? AcquireTime { get; set; }

        /// <summary>
        ///     renewTime is a time when the current holder of a lease has last updated the lease.
        /// </summary>
        [YamlMember(Alias = "renewTime")]
        [JsonProperty("renewTime", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? RenewTime { get; set; }

        /// <summary>
        ///     PreferredHolder signals to a lease holder that the lease has a more optimal holder and should be given up. This field can only be set if Strategy is also set.
        /// </summary>
        [YamlMember(Alias = "preferredHolder")]
        [JsonProperty("preferredHolder", NullValueHandling = NullValueHandling.Ignore)]
        public string PreferredHolder { get; set; }

        /// <summary>
        ///     leaseDurationSeconds is a duration that candidates for a lease need to wait to force acquire it. This is measured against the time of last observed renewTime.
        /// </summary>
        [YamlMember(Alias = "leaseDurationSeconds")]
        [JsonProperty("leaseDurationSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public int? LeaseDurationSeconds { get; set; }

        /// <summary>
        ///     leaseTransitions is the number of transitions of a lease between holders.
        /// </summary>
        [YamlMember(Alias = "leaseTransitions")]
        [JsonProperty("leaseTransitions", NullValueHandling = NullValueHandling.Ignore)]
        public int? LeaseTransitions { get; set; }

        /// <summary>
        ///     holderIdentity contains the identity of the holder of a current lease. If Coordinated Leader Election is used, the holder identity must be equal to the elected LeaseCandidate.metadata.name field.
        /// </summary>
        [YamlMember(Alias = "holderIdentity")]
        [JsonProperty("holderIdentity", NullValueHandling = NullValueHandling.Ignore)]
        public string HolderIdentity { get; set; }

        /// <summary>
        ///     Strategy indicates the strategy for picking the leader for coordinated leader election. If the field is not specified, there is no active coordination for this lease. (Alpha) Using this field requires the CoordinatedLeaderElection feature gate to be enabled.
        /// </summary>
        [YamlMember(Alias = "strategy")]
        [JsonProperty("strategy", NullValueHandling = NullValueHandling.Ignore)]
        public string Strategy { get; set; }
    }
}
