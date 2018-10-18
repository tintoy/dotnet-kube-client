using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

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
        [YamlMember(Alias = "Schema")]
        [JsonProperty("Schema", NullValueHandling = NullValueHandling.Include)]
        public JSONSchemaPropsV1Beta1 Schema { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "Allows")]
        [JsonProperty("Allows", NullValueHandling = NullValueHandling.Include)]
        public bool Allows { get; set; }
    }
}
