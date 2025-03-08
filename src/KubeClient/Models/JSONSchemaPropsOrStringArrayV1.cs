using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSONSchemaPropsOrStringArray represents a JSONSchemaProps or a string array.
    /// </summary>
    public partial class JSONSchemaPropsOrStringArrayV1
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
        [YamlMember(Alias = "Property")]
        [JsonProperty("Property", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Property { get; } = new List<string>();
    }
}
