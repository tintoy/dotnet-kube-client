using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     Describes a certificate signing request
    /// </summary>
    [KubeObject("CertificateSigningRequest", "v1beta1")]
    [KubeApi("apis/certificates.k8s.io/v1beta1/watch/certificatesigningrequests", KubeAction.WatchList)]
    [KubeApi("apis/certificates.k8s.io/v1beta1/watch/certificatesigningrequests/{name}", KubeAction.Watch)]
    [KubeApi("apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}/status", KubeAction.Update)]
    [KubeApi("apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}/approval", KubeAction.Update)]
    [KubeApi("apis/certificates.k8s.io/v1beta1/certificatesigningrequests", KubeAction.Create, KubeAction.DeleteCollection, KubeAction.List)]
    [KubeApi("apis/certificates.k8s.io/v1beta1/certificatesigningrequests/{name}", KubeAction.Delete, KubeAction.Get, KubeAction.Patch, KubeAction.Update)]
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
