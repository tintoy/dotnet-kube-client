using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     JSONSchemaProps is a JSON-Schema following Specification Draft 4 (http://json-schema.org/).
    /// </summary>
    public partial class JSONSchemaPropsV1Beta1 : Models.JSONSchemaPropsV1Beta1, ITracked
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("$schema")]
        [YamlMember(Alias = "$schema")]
        public override string Schema
        {
            get
            {
                return base.Schema;
            }
            set
            {
                base.Schema = value;

                __ModifiedProperties__.Add("Schema");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("id")]
        [YamlMember(Alias = "id")]
        public override string Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;

                __ModifiedProperties__.Add("Id");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "required")]
        [JsonProperty("required", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Required { get; set; } = new List<string>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("example")]
        [YamlMember(Alias = "example")]
        public override Models.JSONV1Beta1 Example
        {
            get
            {
                return base.Example;
            }
            set
            {
                base.Example = value;

                __ModifiedProperties__.Add("Example");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("title")]
        [YamlMember(Alias = "title")]
        public override string Title
        {
            get
            {
                return base.Title;
            }
            set
            {
                base.Title = value;

                __ModifiedProperties__.Add("Title");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("type")]
        [YamlMember(Alias = "type")]
        public override string Type
        {
            get
            {
                return base.Type;
            }
            set
            {
                base.Type = value;

                __ModifiedProperties__.Add("Type");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("$ref")]
        [YamlMember(Alias = "$ref")]
        public override string Ref
        {
            get
            {
                return base.Ref;
            }
            set
            {
                base.Ref = value;

                __ModifiedProperties__.Add("Ref");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "allOf")]
        [JsonProperty("allOf", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.JSONSchemaPropsV1Beta1> AllOf { get; set; } = new List<Models.JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "anyOf")]
        [JsonProperty("anyOf", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.JSONSchemaPropsV1Beta1> AnyOf { get; set; } = new List<Models.JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("multipleOf")]
        [YamlMember(Alias = "multipleOf")]
        public override double MultipleOf
        {
            get
            {
                return base.MultipleOf;
            }
            set
            {
                base.MultipleOf = value;

                __ModifiedProperties__.Add("MultipleOf");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "oneOf")]
        [JsonProperty("oneOf", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.JSONSchemaPropsV1Beta1> OneOf { get; set; } = new List<Models.JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maxLength")]
        [YamlMember(Alias = "maxLength")]
        public override int MaxLength
        {
            get
            {
                return base.MaxLength;
            }
            set
            {
                base.MaxLength = value;

                __ModifiedProperties__.Add("MaxLength");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minLength")]
        [YamlMember(Alias = "minLength")]
        public override int MinLength
        {
            get
            {
                return base.MinLength;
            }
            set
            {
                base.MinLength = value;

                __ModifiedProperties__.Add("MinLength");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "enum")]
        [JsonProperty("enum", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.JSONV1Beta1> Enum { get; set; } = new List<Models.JSONV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("exclusiveMaximum")]
        [YamlMember(Alias = "exclusiveMaximum")]
        public override bool ExclusiveMaximum
        {
            get
            {
                return base.ExclusiveMaximum;
            }
            set
            {
                base.ExclusiveMaximum = value;

                __ModifiedProperties__.Add("ExclusiveMaximum");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("exclusiveMinimum")]
        [YamlMember(Alias = "exclusiveMinimum")]
        public override bool ExclusiveMinimum
        {
            get
            {
                return base.ExclusiveMinimum;
            }
            set
            {
                base.ExclusiveMinimum = value;

                __ModifiedProperties__.Add("ExclusiveMinimum");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maximum")]
        [YamlMember(Alias = "maximum")]
        public override double Maximum
        {
            get
            {
                return base.Maximum;
            }
            set
            {
                base.Maximum = value;

                __ModifiedProperties__.Add("Maximum");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minimum")]
        [YamlMember(Alias = "minimum")]
        public override double Minimum
        {
            get
            {
                return base.Minimum;
            }
            set
            {
                base.Minimum = value;

                __ModifiedProperties__.Add("Minimum");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("description")]
        [YamlMember(Alias = "description")]
        public override string Description
        {
            get
            {
                return base.Description;
            }
            set
            {
                base.Description = value;

                __ModifiedProperties__.Add("Description");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("pattern")]
        [YamlMember(Alias = "pattern")]
        public override string Pattern
        {
            get
            {
                return base.Pattern;
            }
            set
            {
                base.Pattern = value;

                __ModifiedProperties__.Add("Pattern");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("additionalItems")]
        [YamlMember(Alias = "additionalItems")]
        public override Models.JSONSchemaPropsOrBoolV1Beta1 AdditionalItems
        {
            get
            {
                return base.AdditionalItems;
            }
            set
            {
                base.AdditionalItems = value;

                __ModifiedProperties__.Add("AdditionalItems");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("additionalProperties")]
        [YamlMember(Alias = "additionalProperties")]
        public override Models.JSONSchemaPropsOrBoolV1Beta1 AdditionalProperties
        {
            get
            {
                return base.AdditionalProperties;
            }
            set
            {
                base.AdditionalProperties = value;

                __ModifiedProperties__.Add("AdditionalProperties");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "definitions")]
        [JsonProperty("definitions", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, Models.JSONSchemaPropsV1Beta1> Definitions { get; set; } = new Dictionary<string, Models.JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "dependencies")]
        [JsonProperty("dependencies", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, Models.JSONSchemaPropsOrStringArrayV1Beta1> Dependencies { get; set; } = new Dictionary<string, Models.JSONSchemaPropsOrStringArrayV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("externalDocs")]
        [YamlMember(Alias = "externalDocs")]
        public override Models.ExternalDocumentationV1Beta1 ExternalDocs
        {
            get
            {
                return base.ExternalDocs;
            }
            set
            {
                base.ExternalDocs = value;

                __ModifiedProperties__.Add("ExternalDocs");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items")]
        [YamlMember(Alias = "items")]
        public override Models.JSONSchemaPropsV1Beta1 Items
        {
            get
            {
                return base.Items;
            }
            set
            {
                base.Items = value;

                __ModifiedProperties__.Add("Items");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maxItems")]
        [YamlMember(Alias = "maxItems")]
        public override int MaxItems
        {
            get
            {
                return base.MaxItems;
            }
            set
            {
                base.MaxItems = value;

                __ModifiedProperties__.Add("MaxItems");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("maxProperties")]
        [YamlMember(Alias = "maxProperties")]
        public override int MaxProperties
        {
            get
            {
                return base.MaxProperties;
            }
            set
            {
                base.MaxProperties = value;

                __ModifiedProperties__.Add("MaxProperties");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minItems")]
        [YamlMember(Alias = "minItems")]
        public override int MinItems
        {
            get
            {
                return base.MinItems;
            }
            set
            {
                base.MinItems = value;

                __ModifiedProperties__.Add("MinItems");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("minProperties")]
        [YamlMember(Alias = "minProperties")]
        public override int MinProperties
        {
            get
            {
                return base.MinProperties;
            }
            set
            {
                base.MinProperties = value;

                __ModifiedProperties__.Add("MinProperties");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "patternProperties")]
        [JsonProperty("patternProperties", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, Models.JSONSchemaPropsV1Beta1> PatternProperties { get; set; } = new Dictionary<string, Models.JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [YamlMember(Alias = "properties")]
        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public override Dictionary<string, Models.JSONSchemaPropsV1Beta1> Properties { get; set; } = new Dictionary<string, Models.JSONSchemaPropsV1Beta1>();

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("uniqueItems")]
        [YamlMember(Alias = "uniqueItems")]
        public override bool UniqueItems
        {
            get
            {
                return base.UniqueItems;
            }
            set
            {
                base.UniqueItems = value;

                __ModifiedProperties__.Add("UniqueItems");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("default")]
        [YamlMember(Alias = "default")]
        public override Models.JSONV1Beta1 Default
        {
            get
            {
                return base.Default;
            }
            set
            {
                base.Default = value;

                __ModifiedProperties__.Add("Default");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("format")]
        [YamlMember(Alias = "format")]
        public override string Format
        {
            get
            {
                return base.Format;
            }
            set
            {
                base.Format = value;

                __ModifiedProperties__.Add("Format");
            }
        }


        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("not")]
        [YamlMember(Alias = "not")]
        public override Models.JSONSchemaPropsV1Beta1 Not
        {
            get
            {
                return base.Not;
            }
            set
            {
                base.Not = value;

                __ModifiedProperties__.Add("Not");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
