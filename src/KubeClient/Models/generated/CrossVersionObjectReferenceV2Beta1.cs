using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CrossVersionObjectReference contains enough information to let you identify the referred resource.
    /// </summary>
    public partial class CrossVersionObjectReferenceV2Beta1 : KubeObjectV1
    {
        /// <summary>
        ///     Name of the referent; More info: http://kubernetes.io/docs/user-guide/identifiers#names
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }
    }
}
