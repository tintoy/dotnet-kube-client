using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ExternalDocumentation allows referencing an external resource for extended documentation.
    /// </summary>
    public partial class ExternalDocumentationV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("description")]
        [YamlMember(Alias = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("url")]
        [YamlMember(Alias = "url")]
        public string Url { get; set; }
    }
}
