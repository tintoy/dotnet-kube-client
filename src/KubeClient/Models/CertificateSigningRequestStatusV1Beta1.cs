using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class CertificateSigningRequestStatusV1Beta1
    {
        /// <summary>
        ///     Conditions applied to the request, such as approval or denial.
        /// </summary>
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public List<CertificateSigningRequestConditionV1Beta1> Conditions { get; set; } = new List<CertificateSigningRequestConditionV1Beta1>();

        /// <summary>
        ///     If request was approved, the controller will place the issued certificate here.
        /// </summary>
        [JsonProperty("certificate")]
        public string Certificate { get; set; }
    }
}
