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
        [JsonProperty("values", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Values { get; } = new List<string>();

        /// <summary>
        ///     The label key that the selector applies to.
        /// </summary>
        [YamlMember(Alias = "key")]
        [JsonProperty("key", NullValueHandling = NullValueHandling.Include)]
        public string Key { get; set; }
    }
}
