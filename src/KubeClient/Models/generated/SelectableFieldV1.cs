using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SelectableField specifies the JSON path of a field that may be used with field selectors.
    /// </summary>
    public partial class SelectableFieldV1
    {
        /// <summary>
        ///     jsonPath is a simple JSON path which is evaluated against each custom resource to produce a field selector value. Only JSON paths without the array notation are allowed. Must point to a field of type string, boolean or integer. Types with enum values and strings with formats are allowed. If jsonPath refers to absent field in a resource, the jsonPath evaluates to an empty string. Must not point to metdata fields. Required.
        /// </summary>
        [YamlMember(Alias = "jsonPath")]
        [JsonProperty("jsonPath", NullValueHandling = NullValueHandling.Include)]
        public string JsonPath { get; set; }
    }
}
