using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Represents an empty directory for a pod. Empty directory volumes support ownership management and SELinux relabeling.
    /// </summary>
    public partial class EmptyDirVolumeSourceV1
    {
        /// <summary>
        ///     medium represents what type of storage medium should back this directory. The default is "" which means to use the node's default medium. Must be an empty string (default) or Memory. More info: https://kubernetes.io/docs/concepts/storage/volumes#emptydir
        /// </summary>
        [YamlMember(Alias = "medium")]
        [JsonProperty("medium", NullValueHandling = NullValueHandling.Ignore)]
        public string Medium { get; set; }

        /// <summary>
        ///     sizeLimit is the total amount of local storage required for this EmptyDir volume. The size limit is also applicable for memory medium. The maximum usage on memory medium EmptyDir would be the minimum value between the SizeLimit specified here and the sum of memory limits of all containers in a pod. The default is nil which means that the limit is undefined. More info: https://kubernetes.io/docs/concepts/storage/volumes#emptydir
        /// </summary>
        [YamlMember(Alias = "sizeLimit")]
        [JsonProperty("sizeLimit", NullValueHandling = NullValueHandling.Ignore)]
        public string SizeLimit { get; set; }
    }
}
