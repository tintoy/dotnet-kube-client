using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ValidatingAdmissionPolicy describes the definition of an admission validation policy that accepts or rejects an object without changing it.
    /// </summary>
    [KubeObject("ValidatingAdmissionPolicy", "admissionregistration.k8s.io/v1beta1")]
    [KubeApi(KubeAction.List, "apis/admissionregistration.k8s.io/v1beta1/validatingadmissionpolicies")]
    [KubeApi(KubeAction.Create, "apis/admissionregistration.k8s.io/v1beta1/validatingadmissionpolicies")]
    [KubeApi(KubeAction.Get, "apis/admissionregistration.k8s.io/v1beta1/validatingadmissionpolicies/{name}")]
    [KubeApi(KubeAction.Patch, "apis/admissionregistration.k8s.io/v1beta1/validatingadmissionpolicies/{name}")]
    [KubeApi(KubeAction.Delete, "apis/admissionregistration.k8s.io/v1beta1/validatingadmissionpolicies/{name}")]
    [KubeApi(KubeAction.Update, "apis/admissionregistration.k8s.io/v1beta1/validatingadmissionpolicies/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/admissionregistration.k8s.io/v1beta1/watch/validatingadmissionpolicies")]
    [KubeApi(KubeAction.DeleteCollection, "apis/admissionregistration.k8s.io/v1beta1/validatingadmissionpolicies")]
    [KubeApi(KubeAction.Get, "apis/admissionregistration.k8s.io/v1beta1/validatingadmissionpolicies/{name}/status")]
    [KubeApi(KubeAction.Watch, "apis/admissionregistration.k8s.io/v1beta1/watch/validatingadmissionpolicies/{name}")]
    [KubeApi(KubeAction.Patch, "apis/admissionregistration.k8s.io/v1beta1/validatingadmissionpolicies/{name}/status")]
    [KubeApi(KubeAction.Update, "apis/admissionregistration.k8s.io/v1beta1/validatingadmissionpolicies/{name}/status")]
    public partial class ValidatingAdmissionPolicyV1Beta1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the ValidatingAdmissionPolicy.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public ValidatingAdmissionPolicySpecV1Beta1 Spec { get; set; }

        /// <summary>
        ///     The status of the ValidatingAdmissionPolicy, including warnings that are useful to determine if the policy behaves in the expected way. Populated by the system. Read-only.
        /// </summary>
        [YamlMember(Alias = "status")]
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public ValidatingAdmissionPolicyStatusV1Beta1 Status { get; set; }
    }
}
