using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    [KubeResource("CertificateSigningRequestList", "v1beta1")]
    public class CertificateSigningRequestListV1Beta1 : KubeResourceListV1
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<CertificateSigningRequestV1Beta1> Items { get; set; } = new List<CertificateSigningRequestV1Beta1>();
    }
}
