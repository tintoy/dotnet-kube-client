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
        [JsonProperty("required", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> Required { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="Required"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRequired() => Required.Count > 0;

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
        [JsonProperty("allOf", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<JSONSchemaPropsV1Beta1> AllOf { get; } = new List<JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="AllOf"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAllOf() => AllOf.Count > 0;

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "anyOf")]
        [JsonProperty("anyOf", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<JSONSchemaPropsV1Beta1> AnyOf { get; } = new List<JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="AnyOf"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAnyOf() => AnyOf.Count > 0;

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "multipleOf")]
        [JsonProperty("multipleOf", NullValueHandling = NullValueHandling.Ignore)]
        public double? MultipleOf { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "oneOf")]
        [JsonProperty("oneOf", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<JSONSchemaPropsV1Beta1> OneOf { get; } = new List<JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="OneOf"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeOneOf() => OneOf.Count > 0;

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "maxLength")]
        [JsonProperty("maxLength", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxLength { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "minLength")]
        [JsonProperty("minLength", NullValueHandling = NullValueHandling.Ignore)]
        public long? MinLength { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "enum")]
        [JsonProperty("enum", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<JSONV1Beta1> Enum { get; } = new List<JSONV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Enum"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeEnum() => Enum.Count > 0;

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
        public double? Maximum { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "minimum")]
        [JsonProperty("minimum", NullValueHandling = NullValueHandling.Ignore)]
        public double? Minimum { get; set; }

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
        [JsonProperty("definitions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, JSONSchemaPropsV1Beta1> Definitions { get; } = new Dictionary<string, JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Definitions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeDefinitions() => Definitions.Count > 0;

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "dependencies")]
        [JsonProperty("dependencies", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, JSONSchemaPropsOrStringArrayV1Beta1> Dependencies { get; } = new Dictionary<string, JSONSchemaPropsOrStringArrayV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Dependencies"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeDependencies() => Dependencies.Count > 0;

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
        public long? MaxItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "maxProperties")]
        [JsonProperty("maxProperties", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxProperties { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "minItems")]
        [JsonProperty("minItems", NullValueHandling = NullValueHandling.Ignore)]
        public long? MinItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "minProperties")]
        [JsonProperty("minProperties", NullValueHandling = NullValueHandling.Ignore)]
        public long? MinProperties { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "patternProperties")]
        [JsonProperty("patternProperties", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, JSONSchemaPropsV1Beta1> PatternProperties { get; } = new Dictionary<string, JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="PatternProperties"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePatternProperties() => PatternProperties.Count > 0;

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "properties")]
        [JsonProperty("properties", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, JSONSchemaPropsV1Beta1> Properties { get; } = new Dictionary<string, JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Properties"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeProperties() => Properties.Count > 0;

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
