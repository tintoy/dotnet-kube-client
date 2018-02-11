using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressTLS describes the transport layer security associated with an Ingress.
    /// </summary>
    [KubeResource("IngressTLS", "v1beta1")]
    public class IngressTLSV1Beta1
    {
        /// <summary>
        ///     SecretName is the name of the secret used to terminate SSL traffic on 443. Field is left optional to allow SSL routing based on SNI hostname alone. If the SNI host in a listener conflicts with the "Host" header field used by an IngressRule, the SNI host is used for termination and value of the Host header is used for routing.
        /// </summary>
        [JsonProperty("secretName")]
        public string SecretName { get; set; }

        /// <summary>
        ///     Hosts are a list of hosts included in the TLS certificate. The values in this list must match the name/s used in the tlsSecret. Defaults to the wildcard host setting for the loadbalancer controller fulfilling this Ingress, if left unspecified.
        /// </summary>
        [JsonProperty("hosts", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Hosts { get; set; } = new List<string>();
    }
}
