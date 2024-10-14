using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CrossVersionObjectReference contains enough information to let you identify the referred resource.
    /// </summary>
    public partial class CrossVersionObjectReferenceV2 : KubeObjectV1
    {
        /// <summary>
        ///     name is the name of the referent; More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }
    }
}
