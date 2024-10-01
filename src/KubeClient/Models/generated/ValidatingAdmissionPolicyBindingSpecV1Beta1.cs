using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     ValidatingAdmissionPolicyBindingSpec is the specification of the ValidatingAdmissionPolicyBinding.
    /// </summary>
    public partial class ValidatingAdmissionPolicyBindingSpecV1Beta1
    {
        /// <summary>
        ///     PolicyName references a ValidatingAdmissionPolicy name which the ValidatingAdmissionPolicyBinding binds to. If the referenced resource does not exist, this binding is considered invalid and will be ignored Required.
        /// </summary>
        [YamlMember(Alias = "policyName")]
        [JsonProperty("policyName", NullValueHandling = NullValueHandling.Ignore)]
        public string PolicyName { get; set; }

        /// <summary>
        ///     paramRef specifies the parameter resource used to configure the admission control policy. It should point to a resource of the type specified in ParamKind of the bound ValidatingAdmissionPolicy. If the policy specifies a ParamKind and the resource referred to by ParamRef does not exist, this binding is considered mis-configured and the FailurePolicy of the ValidatingAdmissionPolicy applied. If the policy does not specify a ParamKind then this field is ignored, and the rules are evaluated without a param.
        /// </summary>
        [YamlMember(Alias = "paramRef")]
        [JsonProperty("paramRef", NullValueHandling = NullValueHandling.Ignore)]
        public ParamRefV1Beta1 ParamRef { get; set; }

        /// <summary>
        ///     MatchResources declares what resources match this binding and will be validated by it. Note that this is intersected with the policy's matchConstraints, so only requests that are matched by the policy can be selected by this. If this is unset, all resources matched by the policy are validated by this binding When resourceRules is unset, it does not constrain resource matching. If a resource is matched by the other fields of this object, it will be validated. Note that this is differs from ValidatingAdmissionPolicy matchConstraints, where resourceRules are required.
        /// </summary>
        [YamlMember(Alias = "matchResources")]
        [JsonProperty("matchResources", NullValueHandling = NullValueHandling.Ignore)]
        public MatchResourcesV1Beta1 MatchResources { get; set; }

        /// <summary>
        ///     validationActions declares how Validations of the referenced ValidatingAdmissionPolicy are enforced. If a validation evaluates to false it is always enforced according to these actions.
        ///     
        ///     Failures defined by the ValidatingAdmissionPolicy's FailurePolicy are enforced according to these actions only if the FailurePolicy is set to Fail, otherwise the failures are ignored. This includes compilation errors, runtime errors and misconfigurations of the policy.
        ///     
        ///     validationActions is declared as a set of action values. Order does not matter. validationActions may not contain duplicates of the same action.
        ///     
        ///     The supported actions values are:
        ///     
        ///     "Deny" specifies that a validation failure results in a denied request.
        ///     
        ///     "Warn" specifies that a validation failure is reported to the request client in HTTP Warning headers, with a warning code of 299. Warnings can be sent both for allowed or denied admission responses.
        ///     
        ///     "Audit" specifies that a validation failure is included in the published audit event for the request. The audit event will contain a `validation.policy.admission.k8s.io/validation_failure` audit annotation with a value containing the details of the validation failures, formatted as a JSON list of objects, each with the following fields: - message: The validation failure message string - policy: The resource name of the ValidatingAdmissionPolicy - binding: The resource name of the ValidatingAdmissionPolicyBinding - expressionIndex: The index of the failed validations in the ValidatingAdmissionPolicy - validationActions: The enforcement actions enacted for the validation failure Example audit annotation: `"validation.policy.admission.k8s.io/validation_failure": "[{"message": "Invalid value", {"policy": "policy.example.com", {"binding": "policybinding.example.com", {"expressionIndex": "1", {"validationActions": ["Audit"]}]"`
        ///     
        ///     Clients should expect to handle additional values by ignoring any values not recognized.
        ///     
        ///     "Deny" and "Warn" may not be used together since this combination needlessly duplicates the validation failure both in the API response body and the HTTP warning headers.
        ///     
        ///     Required.
        /// </summary>
        [YamlMember(Alias = "validationActions")]
        [JsonProperty("validationActions", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<string> ValidationActions { get; } = new List<string>();

        /// <summary>
        ///     Determine whether the <see cref="ValidationActions"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeValidationActions() => ValidationActions.Count > 0;
    }
}
