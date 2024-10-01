using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     JSONSchemaProps is a JSON-Schema following Specification Draft 4 (http://json-schema.org/).
    /// </summary>
    public partial class JSONSchemaPropsV1
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
        public JSONV1 Example { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "nullable")]
        [JsonProperty("nullable", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Nullable { get; set; }

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
        ///     x-kubernetes-embedded-resource defines that the value is an embedded Kubernetes runtime.Object, with TypeMeta and ObjectMeta. The type must be object. It is allowed to further restrict the embedded object. kind, apiVersion and metadata are validated automatically. x-kubernetes-preserve-unknown-fields is allowed to be true, but does not have to be if the object is fully specified (up to kind, apiVersion, metadata).
        /// </summary>
        [YamlMember(Alias = "x-kubernetes-embedded-resource")]
        [JsonProperty("x-kubernetes-embedded-resource", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KubernetesEmbeddedResource { get; set; }

        /// <summary>
        ///     x-kubernetes-list-type annotates an array to further describe its topology. This extension must only be used on lists and may have 3 possible values:
        ///     
        ///     1) `atomic`: the list is treated as a single entity, like a scalar.
        ///          Atomic lists will be entirely replaced when updated. This extension
        ///          may be used on any type of list (struct, scalar, ...).
        ///     2) `set`:
        ///          Sets are lists that must not have multiple items with the same value. Each
        ///          value must be a scalar, an object with x-kubernetes-map-type `atomic` or an
        ///          array with x-kubernetes-list-type `atomic`.
        ///     3) `map`:
        ///          These lists are like maps in that their elements have a non-index key
        ///          used to identify them. Order is preserved upon merge. The map tag
        ///          must only be used on a list with elements of type object.
        ///     Defaults to atomic for arrays.
        /// </summary>
        [YamlMember(Alias = "x-kubernetes-list-type")]
        [JsonProperty("x-kubernetes-list-type", NullValueHandling = NullValueHandling.Ignore)]
        public string KubernetesListType { get; set; }

        /// <summary>
        ///     x-kubernetes-map-type annotates an object to further describe its topology. This extension must only be used when type is object and may have 2 possible values:
        ///     
        ///     1) `granular`:
        ///          These maps are actual maps (key-value pairs) and each fields are independent
        ///          from each other (they can each be manipulated by separate actors). This is
        ///          the default behaviour for all maps.
        ///     2) `atomic`: the list is treated as a single entity, like a scalar.
        ///          Atomic maps will be entirely replaced when updated.
        /// </summary>
        [YamlMember(Alias = "x-kubernetes-map-type")]
        [JsonProperty("x-kubernetes-map-type", NullValueHandling = NullValueHandling.Ignore)]
        public string KubernetesMapType { get; set; }

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
        public List<JSONSchemaPropsV1> AllOf { get; } = new List<JSONSchemaPropsV1>();

        /// <summary>
        ///     Determine whether the <see cref="AllOf"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeAllOf() => AllOf.Count > 0;

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "anyOf")]
        [JsonProperty("anyOf", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<JSONSchemaPropsV1> AnyOf { get; } = new List<JSONSchemaPropsV1>();

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
        public List<JSONSchemaPropsV1> OneOf { get; } = new List<JSONSchemaPropsV1>();

        /// <summary>
        ///     Determine whether the <see cref="OneOf"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeOneOf() => OneOf.Count > 0;

        /// <summary>
        ///     x-kubernetes-int-or-string specifies that this value is either an integer or a string. If this is true, an empty type is allowed and type as child of anyOf is permitted if following one of the following patterns:
        ///     
        ///     1) anyOf:
        ///        - type: integer
        ///        - type: string
        ///     2) allOf:
        ///        - anyOf:
        ///          - type: integer
        ///          - type: string
        ///        - ... zero or more
        /// </summary>
        [YamlMember(Alias = "x-kubernetes-int-or-string")]
        [JsonProperty("x-kubernetes-int-or-string", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KubernetesIntOrString { get; set; }

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
        public List<JSONV1> Enum { get; } = new List<JSONV1>();

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
        public JSONSchemaPropsOrBoolV1 AdditionalItems { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "additionalProperties")]
        [JsonProperty("additionalProperties", NullValueHandling = NullValueHandling.Ignore)]
        public JSONSchemaPropsOrBoolV1 AdditionalProperties { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "definitions")]
        [JsonProperty("definitions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, JSONSchemaPropsV1> Definitions { get; } = new Dictionary<string, JSONSchemaPropsV1>();

        /// <summary>
        ///     Determine whether the <see cref="Definitions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeDefinitions() => Definitions.Count > 0;

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "dependencies")]
        [JsonProperty("dependencies", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, JSONSchemaPropsOrStringArrayV1> Dependencies { get; } = new Dictionary<string, JSONSchemaPropsOrStringArrayV1>();

        /// <summary>
        ///     Determine whether the <see cref="Dependencies"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeDependencies() => Dependencies.Count > 0;

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "externalDocs")]
        [JsonProperty("externalDocs", NullValueHandling = NullValueHandling.Ignore)]
        public ExternalDocumentationV1 ExternalDocs { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "items")]
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public JSONSchemaPropsV1 Items { get; set; }

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
        public Dictionary<string, JSONSchemaPropsV1> PatternProperties { get; } = new Dictionary<string, JSONSchemaPropsV1>();

        /// <summary>
        ///     Determine whether the <see cref="PatternProperties"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePatternProperties() => PatternProperties.Count > 0;

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "properties")]
        [JsonProperty("properties", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public Dictionary<string, JSONSchemaPropsV1> Properties { get; } = new Dictionary<string, JSONSchemaPropsV1>();

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
        ///     x-kubernetes-list-map-keys annotates an array with the x-kubernetes-list-type `map` by specifying the keys used as the index of the map.
        ///     
        ///     This tag MUST only be used on lists that have the "x-kubernetes-list-type" extension set to "map". Also, the values specified for this attribute must be a scalar typed field of the child structure (no nesting is supported).
        ///     
        ///     The properties specified must either be required or have a default value, to ensure those properties are present for all list items.
        /// </summary>
        [YamlMember(Alias = "x-kubernetes-list-map-keys")]
        [JsonProperty("x-kubernetes-list-map-keys", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> KubernetesListMapKeys { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="KubernetesListMapKeys"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeKubernetesListMapKeys() => KubernetesListMapKeys.Count > 0;

        /// <summary>
        ///     x-kubernetes-preserve-unknown-fields stops the API server decoding step from pruning fields which are not specified in the validation schema. This affects fields recursively, but switches back to normal pruning behaviour if nested properties or additionalProperties are specified in the schema. This can either be true or undefined. False is forbidden.
        /// </summary>
        [YamlMember(Alias = "x-kubernetes-preserve-unknown-fields")]
        [JsonProperty("x-kubernetes-preserve-unknown-fields", NullValueHandling = NullValueHandling.Ignore)]
        public bool? KubernetesPreserveUnknownFields { get; set; }

        /// <summary>
        ///     x-kubernetes-validations describes a list of validation rules written in the CEL expression language.
        /// </summary>
        [MergeStrategy(Key = "rule")]
        [YamlMember(Alias = "x-kubernetes-validations")]
        [JsonProperty("x-kubernetes-validations", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ValidationRuleV1> KubernetesValidations { get; } = new List<ValidationRuleV1>();

        /// <summary>
        ///     Determine whether the <see cref="KubernetesValidations"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeKubernetesValidations() => KubernetesValidations.Count > 0;

        /// <summary>
        ///     default is a default value for undefined object fields. Defaulting is a beta feature under the CustomResourceDefaulting feature gate. Defaulting requires spec.preserveUnknownFields to be false.
        /// </summary>
        [YamlMember(Alias = "default")]
        [JsonProperty("default", NullValueHandling = NullValueHandling.Ignore)]
        public JSONV1 Default { get; set; }

        /// <summary>
        ///     format is an OpenAPI v3 format string. Unknown formats are ignored. The following formats are validated:
        ///     
        ///     - bsonobjectid: a bson object ID, i.e. a 24 characters hex string - uri: an URI as parsed by Golang net/url.ParseRequestURI - email: an email address as parsed by Golang net/mail.ParseAddress - hostname: a valid representation for an Internet host name, as defined by RFC 1034, section 3.1 [RFC1034]. - ipv4: an IPv4 IP as parsed by Golang net.ParseIP - ipv6: an IPv6 IP as parsed by Golang net.ParseIP - cidr: a CIDR as parsed by Golang net.ParseCIDR - mac: a MAC address as parsed by Golang net.ParseMAC - uuid: an UUID that allows uppercase defined by the regex (?i)^[0-9a-f]{8}-?[0-9a-f]{4}-?[0-9a-f]{4}-?[0-9a-f]{4}-?[0-9a-f]{12}$ - uuid3: an UUID3 that allows uppercase defined by the regex (?i)^[0-9a-f]{8}-?[0-9a-f]{4}-?3[0-9a-f]{3}-?[0-9a-f]{4}-?[0-9a-f]{12}$ - uuid4: an UUID4 that allows uppercase defined by the regex (?i)^[0-9a-f]{8}-?[0-9a-f]{4}-?4[0-9a-f]{3}-?[89ab][0-9a-f]{3}-?[0-9a-f]{12}$ - uuid5: an UUID5 that allows uppercase defined by the regex (?i)^[0-9a-f]{8}-?[0-9a-f]{4}-?5[0-9a-f]{3}-?[89ab][0-9a-f]{3}-?[0-9a-f]{12}$ - isbn: an ISBN10 or ISBN13 number string like "0321751043" or "978-0321751041" - isbn10: an ISBN10 number string like "0321751043" - isbn13: an ISBN13 number string like "978-0321751041" - creditcard: a credit card number defined by the regex ^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$ with any non digit characters mixed in - ssn: a U.S. social security number following the regex ^\d{3}[- ]?\d{2}[- ]?\d{4}$ - hexcolor: an hexadecimal color code like "#FFFFFF: following the regex ^#?([0-9a-fA-F]{3}|[0-9a-fA-F]{6})$ - rgbcolor: an RGB color code like rgb like "rgb(255,255,2559" - byte: base64 encoded binary data - password: any kind of string - date: a date string like "2006-01-02" as defined by full-date in RFC3339 - duration: a duration string like "22 ns" as parsed by Golang time.ParseDuration or compatible with Scala duration format - datetime: a date time string like "2014-12-15T19:30:20.000Z" as defined by date-time in RFC3339.
        /// </summary>
        [YamlMember(Alias = "format")]
        [JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)]
        public string Format { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "not")]
        [JsonProperty("not", NullValueHandling = NullValueHandling.Ignore)]
        public JSONSchemaPropsV1 Not { get; set; }
    }
}
