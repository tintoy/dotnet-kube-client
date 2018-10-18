using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SecretReference represents a Secret Reference. It has enough information to retrieve secret in any namespace
    /// </summary>
    public partial class SecretReferenceV1
    {
        /// <summary>
        ///     Name is unique within a namespace to reference a secret resource.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     Namespace defines the space within which the secret name must be unique.
        /// </summary>
        [YamlMember(Alias = "namespace")]
        [JsonProperty("namespace", NullValueHandling = NullValueHandling.Ignore)]
        public string Namespace { get; set; }
    }
}
