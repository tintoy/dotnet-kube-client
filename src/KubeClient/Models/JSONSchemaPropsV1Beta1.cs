using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSONSchemaProps is a JSON-Schema following Specification Draft 4 (http://json-schema.org/).
    /// </summary>
    [KubeObject("JSONSchemaProps", "v1beta1")]
    public partial class JSONSchemaPropsV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("$schema")]
        public string Schema { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("required", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Required { get; set; } = new List<string>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("example")]
        public JSONV1Beta1 Example { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("$ref")]
        public string Ref { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("allOf", NullValueHandling = NullValueHandling.Ignore)]
        public List<JSONSchemaPropsV1Beta1> AllOf { get; set; } = new List<JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("anyOf", NullValueHandling = NullValueHandling.Ignore)]
        public List<JSONSchemaPropsV1Beta1> AnyOf { get; set; } = new List<JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("multipleOf")]
        public double MultipleOf { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("oneOf", NullValueHandling = NullValueHandling.Ignore)]
        public List<JSONSchemaPropsV1Beta1> OneOf { get; set; } = new List<JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maxLength")]
        public int MaxLength { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minLength")]
        public int MinLength { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("enum", NullValueHandling = NullValueHandling.Ignore)]
        public List<JSONV1Beta1> Enum { get; set; } = new List<JSONV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("exclusiveMaximum")]
        public bool ExclusiveMaximum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("exclusiveMinimum")]
        public bool ExclusiveMinimum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maximum")]
        public double Maximum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minimum")]
        public double Minimum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("pattern")]
        public string Pattern { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("additionalItems")]
        public JSONSchemaPropsOrBoolV1Beta1 AdditionalItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("additionalProperties")]
        public JSONSchemaPropsOrBoolV1Beta1 AdditionalProperties { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("definitions", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, JSONSchemaPropsV1Beta1> Definitions { get; set; } = new Dictionary<string, JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("dependencies", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, JSONSchemaPropsOrStringArrayV1Beta1> Dependencies { get; set; } = new Dictionary<string, JSONSchemaPropsOrStringArrayV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("externalDocs")]
        public ExternalDocumentationV1Beta1 ExternalDocs { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items")]
        public JSONSchemaPropsOrArrayV1Beta1 Items { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maxItems")]
        public int MaxItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maxProperties")]
        public int MaxProperties { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minItems")]
        public int MinItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minProperties")]
        public int MinProperties { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("patternProperties", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, JSONSchemaPropsV1Beta1> PatternProperties { get; set; } = new Dictionary<string, JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, JSONSchemaPropsV1Beta1> Properties { get; set; } = new Dictionary<string, JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("uniqueItems")]
        public bool UniqueItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("default")]
        public JSONV1Beta1 Default { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("not")]
        public JSONSchemaPropsV1Beta1 Not { get; set; }
    }
}
