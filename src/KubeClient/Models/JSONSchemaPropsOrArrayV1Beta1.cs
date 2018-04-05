using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSONSchemaPropsOrArray represents a value that can either be a JSONSchemaProps or an array of JSONSchemaProps. Mainly here for serialization purposes.
    /// </summary>
    public partial class JSONSchemaPropsOrArrayV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("JSONSchemas", NullValueHandling = NullValueHandling.Ignore)]
        public List<JSONSchemaPropsV1Beta1> JSONSchemas { get; set; } = new List<JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("Schema")]
        public JSONSchemaPropsV1Beta1 Schema { get; set; }
    }
}
