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
    [KubeApi(KubeAction.List, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests")]
    [KubeApi(KubeAction.Create, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests")]
    [KubeApi(KubeAction.Get, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}")]
    [KubeApi(KubeAction.Patch, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}")]
    [KubeApi(KubeAction.Delete, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}")]
    [KubeApi(KubeAction.Update, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/certificates.k8s.io/v1beta1/watch/certificatesigningrequests")]
    [KubeApi(KubeAction.DeleteCollection, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests")]
    [KubeApi(KubeAction.Get, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/certificates.k8s.io/v1beta1/watch/certificatesigningrequests/{name}")]
    [KubeApi(KubeAction.Patch, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}/approval")]
    public partial class CertificateSigningRequestV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     The certificate request itself and any additional information.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public CertificateSigningRequestSpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     Derived information about the request.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public CertificateSigningRequestStatusV1Beta1 Status { get; set; }
    }
}
