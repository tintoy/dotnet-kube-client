using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     NonResourceAttributes includes the authorization attributes available for non-resource requests to the Authorizer interface
    /// </summary>
    public partial class NonResourceAttributesV1Beta1
    {
        /// <summary>
        ///     Verb is the standard HTTP verb
        /// </summary>
        [YamlMember(Alias = "verb")]
        [JsonProperty("verb", NullValueHandling = NullValueHandling.Ignore)]
        public string Verb { get; set; }

        /// <summary>
        ///     Path is the URL path of the request
        /// </summary>
        [YamlMember(Alias = "path")]
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }
    }
}
