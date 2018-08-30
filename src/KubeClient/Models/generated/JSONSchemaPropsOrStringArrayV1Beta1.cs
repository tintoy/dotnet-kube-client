using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSONSchemaPropsOrStringArray represents a JSONSchemaProps or a string array.
    /// </summary>
    public partial class JSONSchemaPropsOrStringArrayV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("Schema")]
        [YamlMember(Alias = "Schema")]
        public virtual JSONSchemaPropsV1Beta1 Schema { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "Property")]
        [JsonProperty("Property", NullValueHandling = NullValueHandling.Ignore)]
        public virtual List<string> Property { get; set; } = new List<string>();
    }
}
