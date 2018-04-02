using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     ExternalDocumentation allows referencing an external resource for extended documentation.
    /// </summary>
    [KubeObject("ExternalDocumentation", "v1beta1")]
    public partial class ExternalDocumentationV1Beta1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
