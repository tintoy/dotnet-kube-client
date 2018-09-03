using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceColumnDefinition specifies a column for server side printing.
    /// </summary>
    public partial class CustomResourceColumnDefinitionV1Beta1
    {
        /// <summary>
        ///     type is an OpenAPI type definition for this column. See https://github.com/OAI/OpenAPI-Specification/blob/master/versions/2.0.md#data-types for more.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     name is a human readable name for the column.
        /// </summary>
        [JsonProperty("name")]
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>
        ///     JSONPath is a simple JSON path, i.e. with array notation.
        /// </summary>
        [JsonProperty("JSONPath")]
        [YamlMember(Alias = "JSONPath")]
        public string JSONPath { get; set; }

        /// <summary>
        ///     format is an optional OpenAPI type definition for this column. The 'name' format is applied to the primary identifier column to assist in clients identifying column is the resource name. See https://github.com/OAI/OpenAPI-Specification/blob/master/versions/2.0.md#data-types for more.
        /// </summary>
        [JsonProperty("format")]
        [YamlMember(Alias = "format")]
        public string Format { get; set; }

        /// <summary>
        ///     description is a human readable description of this column.
        /// </summary>
        [JsonProperty("description")]
        [YamlMember(Alias = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     priority is an integer defining the relative importance of this column compared to others. Lower numbers are considered higher priority. Columns that may be omitted in limited space scenarios should be given a higher priority.
        /// </summary>
        [JsonProperty("priority")]
        [YamlMember(Alias = "priority")]
        public int Priority { get; set; }
    }
}
