using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ModifyVolumeStatus represents the status object of ControllerModifyVolume operation
    /// </summary>
    public partial class ModifyVolumeStatusV1
    {
        /// <summary>
        ///     targetVolumeAttributesClassName is the name of the VolumeAttributesClass the PVC currently being reconciled
        /// </summary>
        [YamlMember(Alias = "targetVolumeAttributesClassName")]
        [JsonProperty("targetVolumeAttributesClassName", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetVolumeAttributesClassName { get; set; }

        /// <summary>
        ///     status is the status of the ControllerModifyVolume operation. It can be in any of following states:
        ///      - Pending
        ///        Pending indicates that the PersistentVolumeClaim cannot be modified due to unmet requirements, such as
        ///        the specified VolumeAttributesClass not existing.
        ///      - InProgress
        ///        InProgress indicates that the volume is being modified.
        ///      - Infeasible
        ///       Infeasible indicates that the request has been rejected as invalid by the CSI driver. To
        ///     	  resolve the error, a valid VolumeAttributesClass needs to be specified.
        ///     Note: New statuses can be added in the future. Consumers should check for unknown statuses and fail appropriately.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Include)]
        public string Status { get; set; }
    }
}
