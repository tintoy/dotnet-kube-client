using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CrossVersionObjectReference contains enough information to let you identify the referred resource.
    /// </summary>
    public partial class CrossVersionObjectReferenceV1
    {
        /// <summary>
        ///     Kind of the referent; More info: https://git.k8s.io/community/contributors/devel/api-conventions.md#types-kinds"
        /// </summary>
        [JsonProperty("kind")]
        [YamlMember(Alias = "kind")]
        public string Kind { get; set; }

        /// <summary>
        ///     Name of the referent; More info: http://kubernetes.io/docs/user-guide/identifiers#names
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     API version of the referent
        /// </summary>
        [JsonProperty("apiVersion")]
        [YamlMember(Alias = "apiVersion")]
        public string ApiVersion { get; set; }
    }
}
