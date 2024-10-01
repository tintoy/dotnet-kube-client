using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressServiceBackend references a Kubernetes Service as a Backend.
    /// </summary>
    public partial class IngressServiceBackendV1
    {
        /// <summary>
        ///     name is the referenced service. The service must exist in the same namespace as the Ingress object.
        /// </summary>
        [YamlMember(Alias = "name")]
        [JsonProperty("name", NullValueHandling = NullValueHandling.Include)]
        public string Name { get; set; }

        /// <summary>
        ///     port of the referenced service. A port name or port number is required for a IngressServiceBackend.
        /// </summary>
        [YamlMember(Alias = "port")]
        [JsonProperty("port", NullValueHandling = NullValueHandling.Ignore)]
        public ServiceBackendPortV1 Port { get; set; }
    }
}
