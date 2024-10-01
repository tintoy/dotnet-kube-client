using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     CertificateSigningRequest objects provide a mechanism to obtain x509 certificates by submitting a certificate signing request, and having it asynchronously approved and issued.
    ///     
    ///     Kubelets use this API to obtain:
    ///      1. client certificates to authenticate to kube-apiserver (with the "kubernetes.io/kube-apiserver-client-kubelet" signerName).
    ///      2. serving certificates for TLS endpoints kube-apiserver can connect to securely (with the "kubernetes.io/kubelet-serving" signerName).
    ///     
    ///     This API can be used to request client certificates to authenticate to kube-apiserver (with the "kubernetes.io/kube-apiserver-client" signerName), or to obtain certificates from custom non-Kubernetes signers.
    /// </summary>
    [KubeObject("CertificateSigningRequest", "certificates.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/certificates.k8s.io/v1/certificatesigningrequests")]
    [KubeApi(KubeAction.Create, "apis/certificates.k8s.io/v1/certificatesigningrequests")]
    [KubeApi(KubeAction.Get, "apis/certificates.k8s.io/v1/certificatesigningrequests/{name}")]
    [KubeApi(KubeAction.Patch, "apis/certificates.k8s.io/v1/certificatesigningrequests/{name}")]
    [KubeApi(KubeAction.Delete, "apis/certificates.k8s.io/v1/certificatesigningrequests/{name}")]
    [KubeApi(KubeAction.Update, "apis/certificates.k8s.io/v1/certificatesigningrequests/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/certificates.k8s.io/v1/watch/certificatesigningrequests")]
    [KubeApi(KubeAction.DeleteCollection, "apis/certificates.k8s.io/v1/certificatesigningrequests")]
    [KubeApi(KubeAction.Get, "apis/certificates.k8s.io/v1/certificatesigningrequests/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/certificates.k8s.io/v1/watch/certificatesigningrequests/{name}")]
    [KubeApi(KubeAction.Get, "apis/certificates.k8s.io/v1/certificatesigningrequests/{name}/approval")]
    [KubeApi(KubeAction.Patch, "apis/certificates.k8s.io/v1/certificatesigningrequests/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/certificates.k8s.io/v1/certificatesigningrequests/{name}/status")]
    [KubeApi(KubeAction.Patch, "apis/certificates.k8s.io/v1/certificatesigningrequests/{name}/approval")]
    [KubeApi(KubeAction.Update, "apis/certificates.k8s.io/v1/certificatesigningrequests/{name}/approval")]
    public partial class CertificateSigningRequestV1 : KubeResourceV1
    {
        /// <summary>
        ///     spec contains the certificate request, and is immutable after creation. Only the request, signerName, expirationSeconds, and usages fields can be set on creation. Other fields are derived by Kubernetes and cannot be modified by users.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Include)]
        public CertificateSigningRequestSpecV1 Spec { get; set; }

        /// <summary>
        ///     status contains information about whether the request is approved or denied, and the certificate issued by the signer, or the failure condition indicating signer failure.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public CertificateSigningRequestStatusV1 Status { get; set; }
    }
}
