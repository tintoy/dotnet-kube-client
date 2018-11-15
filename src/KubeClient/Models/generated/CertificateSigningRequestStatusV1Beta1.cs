using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class CertificateSigningRequestStatusV1Beta1
    {
        /// <summary>
        ///     If request was approved, the controller will place the issued certificate here.
        /// </summary>
        [YamlMember(Alias = "certificate")]
        [JsonProperty("certificate", NullValueHandling = NullValueHandling.Ignore)]
        public string Certificate { get; set; }

        /// <summary>
        ///     Conditions applied to the request, such as approval or denial.
        /// </summary>
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<CertificateSigningRequestConditionV1Beta1> Conditions { get; } = new List<CertificateSigningRequestConditionV1Beta1>();

        /// <summary>
        ///     Determine whether the <see cref="Conditions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeConditions() => Conditions.Count > 0;
    }
}
