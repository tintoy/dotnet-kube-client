using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressBackend describes all endpoints for a given service and port.
    /// </summary>
    [KubeObject("IngressBackend", "v1beta1")]
    public partial class IngressBackendV1Beta1
    {
        /// <summary>
        ///     Specifies the name of the referenced service.
        /// </summary>
        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }

        /// <summary>
        ///     Specifies the port of the referenced service.
        /// </summary>
        [JsonProperty("servicePort")]
        public string ServicePort { get; set; }
    }
}
