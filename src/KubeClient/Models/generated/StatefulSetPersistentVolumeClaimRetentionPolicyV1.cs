using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     StatefulSetPersistentVolumeClaimRetentionPolicy describes the policy used for PVCs created from the StatefulSet VolumeClaimTemplates.
    /// </summary>
    public partial class StatefulSetPersistentVolumeClaimRetentionPolicyV1
    {
        /// <summary>
        ///     WhenDeleted specifies what happens to PVCs created from StatefulSet VolumeClaimTemplates when the StatefulSet is deleted. The default policy of `Retain` causes PVCs to not be affected by StatefulSet deletion. The `Delete` policy causes those PVCs to be deleted.
        /// </summary>
        [YamlMember(Alias = "whenDeleted")]
        [JsonProperty("whenDeleted", NullValueHandling = NullValueHandling.Ignore)]
        public string WhenDeleted { get; set; }

        /// <summary>
        ///     WhenScaled specifies what happens to PVCs created from StatefulSet VolumeClaimTemplates when the StatefulSet is scaled down. The default policy of `Retain` causes PVCs to not be affected by a scaledown. The `Delete` policy causes the associated PVCs for any excess pods above the replica count to be deleted.
        /// </summary>
        [YamlMember(Alias = "whenScaled")]
        [JsonProperty("whenScaled", NullValueHandling = NullValueHandling.Ignore)]
        public string WhenScaled { get; set; }
    }
}
