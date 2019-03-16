using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressBackend describes all endpoints for a given service and port.
    /// </summary>
    public partial class IngressBackendV1Beta1
    {
        /// <summary>
        ///     Specifies the name of the referenced service.
        /// </summary>
        [YamlMember(Alias = "serviceName")]
        [JsonProperty("serviceName", NullValueHandling = NullValueHandling.Include)]
        public string ServiceName { get; set; }

        /// <summary>
        ///     Specifies the port of the referenced service.
        /// </summary>
        [YamlMember(Alias = "servicePort")]
        [JsonProperty("servicePort", NullValueHandling = NullValueHandling.Include)]
        public Int32OrStringV1 ServicePort { get; set; }
    }
}
