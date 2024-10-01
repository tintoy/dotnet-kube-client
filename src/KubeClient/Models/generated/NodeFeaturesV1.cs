using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NodeFeatures describes the set of features implemented by the CRI implementation. The features contained in the NodeFeatures should depend only on the cri implementation independent of runtime handlers.
    /// </summary>
    public partial class NodeFeaturesV1
    {
        /// <summary>
        ///     SupplementalGroupsPolicy is set to true if the runtime supports SupplementalGroupsPolicy and ContainerUser.
        /// </summary>
        [YamlMember(Alias = "supplementalGroupsPolicy")]
        [JsonProperty("supplementalGroupsPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SupplementalGroupsPolicy { get; set; }
    }
}
