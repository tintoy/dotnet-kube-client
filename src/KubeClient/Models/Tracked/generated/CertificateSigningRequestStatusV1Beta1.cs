using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    public partial class CertificateSigningRequestStatusV1Beta1 : Models.CertificateSigningRequestStatusV1Beta1, ITracked
    {
        /// <summary>
        ///     If request was approved, the controller will place the issued certificate here.
        /// </summary>
        [JsonProperty("certificate")]
        [YamlMember(Alias = "certificate")]
        public override string Certificate
        {
            get
            {
                return base.Certificate;
            }
            set
            {
                base.Certificate = value;

                __ModifiedProperties__.Add("Certificate");
            }
        }


        /// <summary>
        ///     Conditions applied to the request, such as approval or denial.
        /// </summary>
        [YamlMember(Alias = "conditions")]
        [JsonProperty("conditions", NullValueHandling = NullValueHandling.Ignore)]
        public override List<Models.CertificateSigningRequestConditionV1Beta1> Conditions { get; set; } = new List<Models.CertificateSigningRequestConditionV1Beta1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
