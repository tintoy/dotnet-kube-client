using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     PersistentVolumeClaimVolumeSource references the user's PVC in the same namespace. This volume finds the bound PV and mounts that volume for the pod. A PersistentVolumeClaimVolumeSource is, essentially, a wrapper around another type of volume that is owned by someone else (the system).
    /// </summary>
    public partial class PersistentVolumeClaimVolumeSourceV1 : Models.PersistentVolumeClaimVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     ClaimName is the name of a PersistentVolumeClaim in the same namespace as the pod using this volume. More info: https://kubernetes.io/docs/concepts/storage/persistent-volumes#persistentvolumeclaims
        /// </summary>
        [JsonProperty("claimName")]
        [YamlMember(Alias = "claimName")]
        public override string ClaimName
        {
            get
            {
                return base.ClaimName;
            }
            set
            {
                base.ClaimName = value;

                __ModifiedProperties__.Add("ClaimName");
            }
        }


        /// <summary>
        ///     Will force the ReadOnly setting in VolumeMounts. Default false.
        /// </summary>
        [JsonProperty("readOnly")]
        [YamlMember(Alias = "readOnly")]
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;

                __ModifiedProperties__.Add("ReadOnly");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
