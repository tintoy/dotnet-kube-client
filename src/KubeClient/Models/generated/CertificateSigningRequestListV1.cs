using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CertificateSigningRequestList is a collection of CertificateSigningRequest objects
    /// </summary>
    [KubeListItem("CertificateSigningRequest", "certificates.k8s.io/v1")]
    [KubeObject("CertificateSigningRequestList", "certificates.k8s.io/v1")]
    public partial class CertificateSigningRequestListV1 : KubeResourceListV1<CertificateSigningRequestV1>
    {
        /// <summary>
        ///     items is a collection of CertificateSigningRequest objects
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<CertificateSigningRequestV1> Items { get; } = new List<CertificateSigningRequestV1>();
    }
}
