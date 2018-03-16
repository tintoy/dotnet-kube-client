using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    [KubeObject("CertificateSigningRequestList", "certificates.k8s.io/v1beta1")]
    public class CertificateSigningRequestListV1Beta1 : KubeResourceListV1<CertificateSigningRequestV1Beta1>
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<CertificateSigningRequestV1Beta1> Items { get; } = new List<CertificateSigningRequestV1Beta1>();
    }
}
