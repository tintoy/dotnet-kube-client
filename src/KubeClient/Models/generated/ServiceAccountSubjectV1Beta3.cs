using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceAccountSubject holds detailed information for service-account-kind subject.
    /// </summary>
    public partial class ServiceAccountSubjectV1Beta3
    {
        /// <summary>
        ///     `name` is the name of matching ServiceAccount objects, or "*" to match regardless of name. Required.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     `namespace` is the namespace of matching ServiceAccount objects. Required.
        /// </summary>
        [YamlMember(Alias = "namespace")]
        [JsonProperty("namespace", NullValueHandling = NullValueHandling.Include)]
        public string Namespace { get; set; }
    }
}
