using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ValidatingAdmissionPolicyBindingList is a list of ValidatingAdmissionPolicyBinding.
    /// </summary>
    [KubeListItem("ValidatingAdmissionPolicyBinding", "admissionregistration.k8s.io/v1")]
    [KubeObject("ValidatingAdmissionPolicyBindingList", "admissionregistration.k8s.io/v1")]
    public partial class ValidatingAdmissionPolicyBindingListV1 : KubeResourceListV1<ValidatingAdmissionPolicyBindingV1>
    {
        /// <summary>
        ///     List of PolicyBinding.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ValidatingAdmissionPolicyBindingV1> Items { get; } = new List<ValidatingAdmissionPolicyBindingV1>();
    }
}
