using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressSpec describes the Ingress the user wishes to exist.
    /// </summary>
    public partial class IngressSpecV1Beta1
    {
        /// <summary>
        ///     A default backend capable of servicing requests that don't match any rule. At least one of 'backend' or 'rules' must be specified. This field is optional to allow the loadbalancer controller or defaulting logic to specify a global default.
        /// </summary>
        [JsonProperty("backend")]
        [YamlMember(Alias = "backend")]
        public IngressBackendV1Beta1 Backend { get; set; }

        /// <summary>
        ///     A list of host rules used to configure the Ingress. If unspecified, or no rule matches, all traffic is sent to the default backend.
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", NullValueHandling = NullValueHandling.Ignore)]
        public List<IngressRuleV1Beta1> Rules { get; set; } = new List<IngressRuleV1Beta1>();

        /// <summary>
        ///     TLS configuration. Currently the Ingress only supports a single TLS port, 443. If multiple members of this list specify different hosts, they will be multiplexed on the same port according to the hostname specified through the SNI TLS extension, if the ingress controller fulfilling the ingress supports SNI.
        /// </summary>
        [YamlMember(Alias = "tls")]
        [JsonProperty("tls", NullValueHandling = NullValueHandling.Ignore)]
        public List<IngressTLSV1Beta1> Tls { get; set; } = new List<IngressTLSV1Beta1>();
    }
}
