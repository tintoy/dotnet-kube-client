using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     IngressTLS describes the transport layer security associated with an Ingress.
    /// </summary>
    public partial class IngressTLSV1Beta1 : Models.IngressTLSV1Beta1, ITracked
    {
        /// <summary>
        ///     SecretName is the name of the secret used to terminate SSL traffic on 443. Field is left optional to allow SSL routing based on SNI hostname alone. If the SNI host in a listener conflicts with the "Host" header field used by an IngressRule, the SNI host is used for termination and value of the Host header is used for routing.
        /// </summary>
        [JsonProperty("secretName")]
        [YamlMember(Alias = "secretName")]
        public override string SecretName
        {
            get
            {
                return base.SecretName;
            }
            set
            {
                base.SecretName = value;

                __ModifiedProperties__.Add("SecretName");
            }
        }


        /// <summary>
        ///     Hosts are a list of hosts included in the TLS certificate. The values in this list must match the name/s used in the tlsSecret. Defaults to the wildcard host setting for the loadbalancer controller fulfilling this Ingress, if left unspecified.
        /// </summary>
        [YamlMember(Alias = "hosts")]
        [JsonProperty("hosts", NullValueHandling = NullValueHandling.Ignore)]
        public override List<string> Hosts { get; set; } = new List<string>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
