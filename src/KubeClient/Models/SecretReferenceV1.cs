using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     SecretReference represents a Secret Reference. It has enough information to retrieve secret in any namespace
    /// </summary>
    [KubeObject("SecretReference", "v1")]
    public partial class SecretReferenceV1
    {
        /// <summary>
        ///     Name is unique within a namespace to reference a secret resource.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Namespace defines the space within which the secret name must be unique.
        /// </summary>
        [JsonProperty("namespace")]
        public string Namespace { get; set; }
    }
}
