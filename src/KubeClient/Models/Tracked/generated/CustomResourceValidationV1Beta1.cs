using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     CustomResourceValidation is a list of validation methods for CustomResources.
    /// </summary>
    public partial class CustomResourceValidationV1Beta1 : Models.CustomResourceValidationV1Beta1, ITracked
    {
        /// <summary>
        ///     OpenAPIV3Schema is the OpenAPI v3 schema to be validated against.
        /// </summary>
        [JsonProperty("openAPIV3Schema")]
        [YamlMember(Alias = "openAPIV3Schema")]
        public override Models.JSONSchemaPropsV1Beta1 OpenAPIV3Schema
        {
            get
            {
                return base.OpenAPIV3Schema;
            }
            set
            {
                base.OpenAPIV3Schema = value;

                __ModifiedProperties__.Add("OpenAPIV3Schema");
            }
        }


        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public HashSet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
