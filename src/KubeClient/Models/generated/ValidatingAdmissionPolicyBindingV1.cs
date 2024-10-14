using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ValidatingAdmissionPolicyBinding binds the ValidatingAdmissionPolicy with paramerized resources. ValidatingAdmissionPolicyBinding and parameter CRDs together define how cluster administrators configure policies for clusters.
    ///     
    ///     For a given admission request, each binding will cause its policy to be evaluated N times, where N is 1 for policies/bindings that don't use params, otherwise N is the number of parameters selected by the binding.
    ///     
    ///     The CEL expressions of a policy must have a computed CEL cost below the maximum CEL budget. Each evaluation of the policy is given an independent CEL cost budget. Adding/removing policies, bindings, or params can not affect whether a given (policy, binding, param) combination is within its own CEL budget.
    /// </summary>
    [KubeObject("ValidatingAdmissionPolicyBinding", "admissionregistration.k8s.io/v1")]
    [KubeApi(KubeAction.List, "apis/admissionregistration.k8s.io/v1/validatingadmissionpolicybindings")]
    [KubeApi(KubeAction.Create, "apis/admissionregistration.k8s.io/v1/validatingadmissionpolicybindings")]
    [KubeApi(KubeAction.Get, "apis/admissionregistration.k8s.io/v1/validatingadmissionpolicybindings/{name}")]
    [KubeApi(KubeAction.Patch, "apis/admissionregistration.k8s.io/v1/validatingadmissionpolicybindings/{name}")]
    [KubeApi(KubeAction.Delete, "apis/admissionregistration.k8s.io/v1/validatingadmissionpolicybindings/{name}")]
    [KubeApi(KubeAction.Update, "apis/admissionregistration.k8s.io/v1/validatingadmissionpolicybindings/{name}")]
    [KubeApi(KubeAction.WatchList, "apis/admissionregistration.k8s.io/v1/watch/validatingadmissionpolicybindings")]
    [KubeApi(KubeAction.DeleteCollection, "apis/admissionregistration.k8s.io/v1/validatingadmissionpolicybindings")]
    [KubeApi(KubeAction.Watch, "apis/admissionregistration.k8s.io/v1/watch/validatingadmissionpolicybindings/{name}")]
    public partial class ValidatingAdmissionPolicyBindingV1 : KubeResourceV1
    {
        /// <summary>
        ///     Specification of the desired behavior of the ValidatingAdmissionPolicyBinding.
        /// </summary>
        [YamlMember(Alias = "spec")]
        [JsonProperty("spec", NullValueHandling = NullValueHandling.Ignore)]
        public ValidatingAdmissionPolicyBindingSpecV1 Spec { get; set; }
    }
}
