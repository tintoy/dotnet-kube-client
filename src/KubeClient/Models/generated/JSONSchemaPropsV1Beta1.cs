using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSONSchemaProps is a JSON-Schema following Specification Draft 4 (http://json-schema.org/).
    /// </summary>
    public partial class JSONSchemaPropsV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "$schema")]
        [JsonProperty("$schema", NullValueHandling = NullValueHandling.Ignore)]
        public string Schema { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "id")]
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "required")]
        [JsonProperty("required", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Required { get; set; } = new List<string>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "example")]
        [JsonProperty("example", NullValueHandling = NullValueHandling.Ignore)]
        public JSONV1Beta1 Example { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "title")]
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "type")]
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "$ref")]
        [JsonProperty("$ref", NullValueHandling = NullValueHandling.Ignore)]
        public string Ref { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "allOf")]
        [JsonProperty("allOf", NullValueHandling = NullValueHandling.Ignore)]
        public List<JSONSchemaPropsV1Beta1> AllOf { get; set; } = new List<JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "anyOf")]
        [JsonProperty("anyOf", NullValueHandling = NullValueHandling.Ignore)]
        public List<JSONSchemaPropsV1Beta1> AnyOf { get; set; } = new List<JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "multipleOf")]
        [JsonProperty("multipleOf", NullValueHandling = NullValueHandling.Ignore)]
        public double MultipleOf { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "oneOf")]
        [JsonProperty("oneOf", NullValueHandling = NullValueHandling.Ignore)]
        public List<JSONSchemaPropsV1Beta1> OneOf { get; set; } = new List<JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "maxLength")]
        [JsonProperty("maxLength", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxLength { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "minLength")]
        [JsonProperty("minLength", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinLength { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "enum")]
        [JsonProperty("enum", NullValueHandling = NullValueHandling.Ignore)]
        public List<JSONV1Beta1> Enum { get; set; } = new List<JSONV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "exclusiveMaximum")]
        [JsonProperty("exclusiveMaximum", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExclusiveMaximum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "exclusiveMinimum")]
        [JsonProperty("exclusiveMinimum", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExclusiveMinimum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "maximum")]
        [JsonProperty("maximum", NullValueHandling = NullValueHandling.Ignore)]
        public double Maximum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "minimum")]
        [JsonProperty("minimum", NullValueHandling = NullValueHandling.Ignore)]
        public double Minimum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "description")]
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "pattern")]
        [JsonProperty("pattern", NullValueHandling = NullValueHandling.Ignore)]
        public string Pattern { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "additionalItems")]
        [JsonProperty("additionalItems", NullValueHandling = NullValueHandling.Ignore)]
        public JSONSchemaPropsOrBoolV1Beta1 AdditionalItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "additionalProperties")]
        [JsonProperty("additionalProperties", NullValueHandling = NullValueHandling.Ignore)]
        public JSONSchemaPropsOrBoolV1Beta1 AdditionalProperties { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "definitions")]
        [JsonProperty("definitions", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, JSONSchemaPropsV1Beta1> Definitions { get; set; } = new Dictionary<string, JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "dependencies")]
        [JsonProperty("dependencies", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, JSONSchemaPropsOrStringArrayV1Beta1> Dependencies { get; set; } = new Dictionary<string, JSONSchemaPropsOrStringArrayV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "externalDocs")]
        [JsonProperty("externalDocs", NullValueHandling = NullValueHandling.Ignore)]
        public ExternalDocumentationV1Beta1 ExternalDocs { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "items")]
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public JSONSchemaPropsV1Beta1 Items { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "maxItems")]
        [JsonProperty("maxItems", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "maxProperties")]
        [JsonProperty("maxProperties", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxProperties { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "minItems")]
        [JsonProperty("minItems", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "minProperties")]
        [JsonProperty("minProperties", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinProperties { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "patternProperties")]
        [JsonProperty("patternProperties", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, JSONSchemaPropsV1Beta1> PatternProperties { get; set; } = new Dictionary<string, JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "properties")]
        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, JSONSchemaPropsV1Beta1> Properties { get; set; } = new Dictionary<string, JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "uniqueItems")]
        [JsonProperty("uniqueItems", NullValueHandling = NullValueHandling.Ignore)]
        public bool? UniqueItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "default")]
        [JsonProperty("default", NullValueHandling = NullValueHandling.Ignore)]
        public JSONV1Beta1 Default { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "format")]
        [JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)]
        public string Format { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "not")]
        [JsonProperty("not", NullValueHandling = NullValueHandling.Ignore)]
        public JSONSchemaPropsV1Beta1 Not { get; set; }
    }
}
