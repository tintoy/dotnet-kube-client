using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ServiceBackendPort is the service port being referenced.
    /// </summary>
    public partial class ServiceBackendPortV1
    {
        /// <summary>
        ///     name is the name of the port on the Service. This is a mutually exclusive setting with "Number".
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        ///     number is the numerical port number (e.g. 80) on the Service. This is a mutually exclusive setting with "Name".
        /// </summary>
        [YamlMember(Alias = "number")]
        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        public int? Number { get; set; }
    }
}
