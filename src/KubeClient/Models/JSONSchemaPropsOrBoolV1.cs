using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSONSchemaPropsOrBool represents JSONSchemaProps or a boolean value. Defaults to true for the boolean property.
    /// </summary>
    public partial class JSONSchemaPropsOrBoolV1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "Schema")]
        [JsonProperty("Schema", NullValueHandling = NullValueHandling.Include)]
        public JSONSchemaPropsV1 Schema { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "Allows")]
        [JsonProperty("Allows", NullValueHandling = NullValueHandling.Include)]
        public bool Allows { get; set; }
    }
}
