using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     CustomResourceDefinitionSpec describes how a user wants their resource to appear
    /// </summary>
    public partial class CustomResourceDefinitionSpecV1Beta1
    {
        /// <summary>
        ///     Scope indicates whether this resource is cluster or namespace scoped.  Default is namespaced
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        ///     Validation describes the validation methods for CustomResources This field is alpha-level and should only be sent to servers that enable the CustomResourceValidation feature.
        /// </summary>
        [JsonProperty("validation")]
        public CustomResourceValidationV1Beta1 Validation { get; set; }

        /// <summary>
        ///     Version is the version this resource belongs in
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        ///     Group is the group this resource belongs in
        /// </summary>
        [JsonProperty("group")]
        public string Group { get; set; }

        /// <summary>
        ///     Names are the names used to describe this custom resource
        /// </summary>
        [JsonProperty("names")]
        public CustomResourceDefinitionNamesV1Beta1 Names { get; set; }
    }
}
