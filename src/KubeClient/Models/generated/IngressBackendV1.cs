using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressBackend describes all endpoints for a given service and port.
    /// </summary>
    public partial class IngressBackendV1
    {
        /// <summary>
        ///     resource is an ObjectRef to another Kubernetes resource in the namespace of the Ingress object. If resource is specified, a service.Name and service.Port must not be specified. This is a mutually exclusive setting with "Service".
        /// </summary>
        [YamlMember(Alias = "resource")]
        [JsonProperty("resource", NullValueHandling = NullValueHandling.Ignore)]
        public TypedLocalObjectReferenceV1 Resource { get; set; }

        /// <summary>
        ///     service references a service as a backend. This is a mutually exclusive setting with "Resource".
        /// </summary>
        [YamlMember(Alias = "service")]
        [JsonProperty("service", NullValueHandling = NullValueHandling.Ignore)]
        public IngressServiceBackendV1 Service { get; set; }
    }
}
