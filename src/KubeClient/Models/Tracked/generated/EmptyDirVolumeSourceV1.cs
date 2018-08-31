using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     Represents an empty directory for a pod. Empty directory volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class EmptyDirVolumeSourceV1 : Models.EmptyDirVolumeSourceV1, ITracked
    {
        /// <summary>
        ///     What type of storage medium should back this directory. The default is "" which means to use the node's default medium. Must be an empty string (default) or Memory. More info: https://kubernetes.io/docs/concepts/storage/volumes#emptydir
        /// </summary>
        [JsonProperty("medium")]
        [YamlMember(Alias = "medium")]
        public override string Medium
        {
            get
            {
                return base.Medium;
            }
            set
            {
                base.Medium = value;

                __ModifiedProperties__.Add("Medium");
            }
        }


        /// <summary>
        ///     Total amount of local storage required for this EmptyDir volume. The size limit is also applicable for memory medium. The maximum usage on memory medium EmptyDir would be the minimum value between the SizeLimit specified here and the sum of memory limits of all containers in a pod. The default is nil which means that the limit is undefined. More info: http://kubernetes.io/docs/user-guide/volumes#emptydir
        /// </summary>
        [JsonProperty("sizeLimit")]
        [YamlMember(Alias = "sizeLimit")]
        public override string SizeLimit
        {
            get
            {
                return base.SizeLimit;
            }
            set
            {
                base.SizeLimit = value;

                __ModifiedProperties__.Add("SizeLimit");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
