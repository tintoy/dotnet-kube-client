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
        [JsonProperty("Schema")]
        [YamlMember(Alias = "Schema")]
        public virtual JSONSchemaPropsV1Beta1 Schema { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("Allows")]
        [YamlMember(Alias = "Allows")]
        public virtual bool Allows { get; set; }
    }
}
