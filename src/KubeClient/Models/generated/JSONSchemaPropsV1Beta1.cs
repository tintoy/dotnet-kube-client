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
        [JsonProperty("$schema")]
        [YamlMember(Alias = "$schema")]
        public string Schema { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("id")]
        [YamlMember(Alias = "id")]
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
        [JsonProperty("example")]
        [YamlMember(Alias = "example")]
        public JSONV1Beta1 Example { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("title")]
        [YamlMember(Alias = "title")]
        public string Title { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("$ref")]
        [YamlMember(Alias = "$ref")]
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
        [JsonProperty("multipleOf")]
        [YamlMember(Alias = "multipleOf")]
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
        [JsonProperty("maxLength")]
        [YamlMember(Alias = "maxLength")]
        public int MaxLength { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minLength")]
        [YamlMember(Alias = "minLength")]
        public int MinLength { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "enum")]
        [JsonProperty("enum", NullValueHandling = NullValueHandling.Ignore)]
        public List<JSONV1Beta1> Enum { get; set; } = new List<JSONV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("exclusiveMaximum")]
        [YamlMember(Alias = "exclusiveMaximum")]
        public bool ExclusiveMaximum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("exclusiveMinimum")]
        [YamlMember(Alias = "exclusiveMinimum")]
        public bool ExclusiveMinimum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maximum")]
        [YamlMember(Alias = "maximum")]
        public double Maximum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minimum")]
        [YamlMember(Alias = "minimum")]
        public double Minimum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("description")]
        [YamlMember(Alias = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("pattern")]
        [YamlMember(Alias = "pattern")]
        public string Pattern { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("additionalItems")]
        [YamlMember(Alias = "additionalItems")]
        public JSONSchemaPropsOrBoolV1Beta1 AdditionalItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("additionalProperties")]
        [YamlMember(Alias = "additionalProperties")]
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
        [JsonProperty("externalDocs")]
        [YamlMember(Alias = "externalDocs")]
        public ExternalDocumentationV1Beta1 ExternalDocs { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items")]
        [YamlMember(Alias = "items")]
        public JSONSchemaPropsV1Beta1 Items { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maxItems")]
        [YamlMember(Alias = "maxItems")]
        public int MaxItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maxProperties")]
        [YamlMember(Alias = "maxProperties")]
        public int MaxProperties { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minItems")]
        [YamlMember(Alias = "minItems")]
        public int MinItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minProperties")]
        [YamlMember(Alias = "minProperties")]
        public int MinProperties { get; set; }

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
        [JsonProperty("uniqueItems")]
        [YamlMember(Alias = "uniqueItems")]
        public bool UniqueItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("default")]
        [YamlMember(Alias = "default")]
        public JSONV1Beta1 Default { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("format")]
        [YamlMember(Alias = "format")]
        public string Format { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("not")]
        [YamlMember(Alias = "not")]
        public JSONSchemaPropsV1Beta1 Not { get; set; }
    }
}
