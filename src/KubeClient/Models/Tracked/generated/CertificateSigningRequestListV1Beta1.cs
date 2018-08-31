using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models.Tracked
{
    /// <summary>
    ///     No description provided.
    /// </summary>
    [KubeListItem("CertificateSigningRequest", "certificates.k8s.io/v1beta1")]
    [KubeObject("CertificateSigningRequestList", "certificates.k8s.io/v1beta1")]
    public partial class CertificateSigningRequestListV1Beta1 : Models.CertificateSigningRequestListV1Beta1, ITracked
    {
        /// <summary>
        ///     Description not provided.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<Models.CertificateSigningRequestV1Beta1> Items { get; } = new List<Models.CertificateSigningRequestV1Beta1>();

        /// <summary>
        ///     Names of model properties that have been modified.
        /// </summary>
        [JsonIgnore, YamlIgnore]
        public ISet<string> __ModifiedProperties__ { get; } = new HashSet<string>();
    }
}
