using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSONSchemaPropsOrBool represents JSONSchemaProps or a boolean value. Defaults to true for the boolean property.
    /// </summary>
    public partial class JSONSchemaPropsOrBoolV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("Allows")]
        public bool Allows { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("Schema")]
        public JSONSchemaPropsV1Beta1 Schema { get; set; }
    }
}
