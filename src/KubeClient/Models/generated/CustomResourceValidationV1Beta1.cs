using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceValidation is a list of validation methods for CustomResources.
    /// </summary>
    public partial class CustomResourceValidationV1Beta1
    {
        /// <summary>
        ///     OpenAPIV3Schema is the OpenAPI v3 schema to be validated against.
        /// </summary>
        [YamlMember(Alias = "openAPIV3Schema")]
        [JsonProperty("openAPIV3Schema", NullValueHandling = NullValueHandling.Ignore)]
        public JSONSchemaPropsV1Beta1 OpenAPIV3Schema { get; set; }
    }
}
