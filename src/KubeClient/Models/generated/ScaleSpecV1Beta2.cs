using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ScaleSpec describes the attributes of a scale subresource
    /// </summary>
    public partial class ScaleSpecV1Beta2
    {
        /// <summary>
        ///     desired number of instances for the scaled object.
        /// </summary>
        [YamlMember(Alias = "replicas")]
        [JsonProperty("replicas", NullValueHandling = NullValueHandling.Ignore)]
        public int? Replicas { get; set; }
    }
}
