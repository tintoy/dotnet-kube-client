using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Describes a certificate signing request
    /// </summary>
    [KubeObject("CertificateSigningRequest", "certificates.k8s.io/v1beta1")]
    public partial class CertificateSigningRequestV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     The certificate request itself and any additional information.
        /// </summary>
        [JsonProperty("spec")]
        [YamlMember(Alias = "spec")]
        public CertificateSigningRequestSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Derived information about the request.
        /// </summary>
        [JsonProperty("status")]
        [YamlMember(Alias = "status")]
        public CertificateSigningRequestStatusV1Beta1 Status { get; set; }
    }
}
