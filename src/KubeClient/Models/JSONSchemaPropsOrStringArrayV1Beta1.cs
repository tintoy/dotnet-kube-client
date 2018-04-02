using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSONSchemaPropsOrStringArray represents a JSONSchemaProps or a string array.
    /// </summary>
    [KubeObject("JSONSchemaPropsOrStringArray", "v1beta1")]
    public partial class JSONSchemaPropsOrStringArrayV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("Schema")]
        public JSONSchemaPropsV1Beta1 Schema { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("Property", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Property { get; set; } = new List<string>();
    }
}
