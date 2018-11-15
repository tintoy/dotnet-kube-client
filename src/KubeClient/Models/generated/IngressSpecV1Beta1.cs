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
        [YamlMember(Alias = "backend")]
        [JsonProperty("backend", NullValueHandling = NullValueHandling.Ignore)]
        public IngressBackendV1Beta1 Backend { get; set; }

        /// <summary>
        ///     A list of host rules used to configure the Ingress. If unspecified, or no rule matches, all traffic is sent to the default backend.
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<IngressRuleV1Beta1> Rules { get; } = new List<IngressRuleV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Rules"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRules() => Rules.Count > 0;

        /// <summary>
        ///     TLS configuration. Currently the Ingress only supports a single TLS port, 443. If multiple members of this list specify different hosts, they will be multiplexed on the same port according to the hostname specified through the SNI TLS extension, if the ingress controller fulfilling the ingress supports SNI.
        /// </summary>
        [YamlMember(Alias = "tls")]
        [JsonProperty("tls", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<IngressTLSV1Beta1> Tls { get; } = new List<IngressTLSV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Tls"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeTls() => Tls.Count > 0;
    }
}
