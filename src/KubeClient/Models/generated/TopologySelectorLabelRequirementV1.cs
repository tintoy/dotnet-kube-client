using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     A topology selector requirement is a selector that matches given label. This is an alpha feature and may change in the future.
    /// </summary>
    public partial class TopologySelectorLabelRequirementV1
    {
        /// <summary>
        ///     An array of string values. One value must match the label to be selected. Each entry in Values is ORed.
        /// </summary>
        [YamlMember(Alias = "values")]
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Values { get; set; } = new List<string>();

        /// <summary>
        ///     The label key that the selector applies to.
        /// </summary>
        [JsonProperty("key")]
        [YamlMember(Alias = "key")]
        public string Key { get; set; }
    }
}
