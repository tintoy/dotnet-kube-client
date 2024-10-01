using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     IngressSpec describes the Ingress the user wishes to exist.
    /// </summary>
    public partial class IngressSpecV1
    {
        /// <summary>
        ///     defaultBackend is the backend that should handle requests that don't match any rule. If Rules are not specified, DefaultBackend must be specified. If DefaultBackend is not set, the handling of requests that do not match any of the rules will be up to the Ingress controller.
        /// </summary>
        [YamlMember(Alias = "defaultBackend")]
        [JsonProperty("defaultBackend", NullValueHandling = NullValueHandling.Ignore)]
        public IngressBackendV1 DefaultBackend { get; set; }

        /// <summary>
        ///     ingressClassName is the name of an IngressClass cluster resource. Ingress controller implementations use this field to know whether they should be serving this Ingress resource, by a transitive connection (controller -&gt; IngressClass -&gt; Ingress resource). Although the `kubernetes.io/ingress.class` annotation (simple constant name) was never formally defined, it was widely supported by Ingress controllers to create a direct binding between Ingress controller and Ingress resources. Newly created Ingress resources should prefer using the field. However, even though the annotation is officially deprecated, for backwards compatibility reasons, ingress controllers should still honor that annotation if present.
        /// </summary>
        [YamlMember(Alias = "ingressClassName")]
        [JsonProperty("ingressClassName", NullValueHandling = NullValueHandling.Ignore)]
        public string IngressClassName { get; set; }

        /// <summary>
        ///     rules is a list of host rules used to configure the Ingress. If unspecified, or no rule matches, all traffic is sent to the default backend.
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<IngressRuleV1> Rules { get; } = new List<IngressRuleV1>();

        /// <summary>
        ///     Determine whether the <see cref="Rules"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRules() => Rules.Count > 0;

        /// <summary>
        ///     tls represents the TLS configuration. Currently the Ingress only supports a single TLS port, 443. If multiple members of this list specify different hosts, they will be multiplexed on the same port according to the hostname specified through the SNI TLS extension, if the ingress controller fulfilling the ingress supports SNI.
        /// </summary>
        [YamlMember(Alias = "tls")]
        [JsonProperty("tls", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<IngressTLSV1> Tls { get; } = new List<IngressTLSV1>();

        /// <summary>
        ///     Determine whether the <see cref="Tls"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeTls() => Tls.Count > 0;
    }
}
