using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Rule is a tuple of APIGroups, APIVersion, and Resources.It is recommended to make sure that all the tuple expansions are valid.
    /// </summary>
    public partial class RuleV1Alpha1
    {
        /// <summary>
        ///     APIGroups is the API groups the resources belong to. '*' is all groups. If '*' is present, the length of the slice must be one. Required.
        /// </summary>
        [YamlMember(Alias = "apiGroups")]
        [JsonProperty("apiGroups", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ApiGroups { get; set; } = new List<string>();

        /// <summary>
        ///     APIVersions is the API versions the resources belong to. '*' is all versions. If '*' is present, the length of the slice must be one. Required.
        /// </summary>
        [YamlMember(Alias = "apiVersions")]
        [JsonProperty("apiVersions", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ApiVersions { get; set; } = new List<string>();

        /// <summary>
        ///     Resources is a list of resources this rule applies to.
        ///     
        ///     For example: 'pods' means pods. 'pods/log' means the log subresource of pods. '*' means all resources, but not subresources. 'pods/*' means all subresources of pods. '*/scale' means all scale subresources. '*/*' means all resources and their subresources.
        ///     
        ///     If wildcard is present, the validation rule will ensure resources do not overlap with each other.
        ///     
        ///     Depending on the enclosing object, subresources might not be allowed. Required.
        /// </summary>
        [YamlMember(Alias = "resources")]
        [JsonProperty("resources", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Resources { get; set; } = new List<string>();
    }
}
