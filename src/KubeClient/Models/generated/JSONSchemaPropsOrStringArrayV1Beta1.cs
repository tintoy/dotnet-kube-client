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
        [YamlMember(Alias = "Schema")]
        [JsonProperty("Schema", NullValueHandling = NullValueHandling.Include)]
        public JSONSchemaPropsV1Beta1 Schema { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "Property")]
        [JsonProperty("Property", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Property { get; } = new List<string>();
    }
}
