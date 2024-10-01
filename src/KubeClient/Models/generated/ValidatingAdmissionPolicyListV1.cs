using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ValidatingAdmissionPolicyList is a list of ValidatingAdmissionPolicy.
    /// </summary>
    [KubeListItem("ValidatingAdmissionPolicy", "admissionregistration.k8s.io/v1")]
    [KubeObject("ValidatingAdmissionPolicyList", "admissionregistration.k8s.io/v1")]
    public partial class ValidatingAdmissionPolicyListV1 : KubeResourceListV1<ValidatingAdmissionPolicyV1>
    {
        /// <summary>
        ///     List of ValidatingAdmissionPolicy.
        /// </summary>
        [JsonProperty("items", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public override List<ValidatingAdmissionPolicyV1> Items { get; } = new List<ValidatingAdmissionPolicyV1>();
    }
}
