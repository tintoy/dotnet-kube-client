using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     PolicyRulesWithSubjects prescribes a test that applies to a request to an apiserver. The test considers the subject making the request, the verb being requested, and the resource to be acted upon. This PolicyRulesWithSubjects matches a request if and only if both (a) at least one member of subjects matches the request and (b) at least one member of resourceRules or nonResourceRules matches the request.
    /// </summary>
    public partial class PolicyRulesWithSubjectsV1
    {
        /// <summary>
        ///     `nonResourceRules` is a list of NonResourcePolicyRules that identify matching requests according to their verb and the target non-resource URL.
        /// </summary>
        [YamlMember(Alias = "nonResourceRules")]
        [JsonProperty("nonResourceRules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<NonResourcePolicyRuleV1> NonResourceRules { get; } = new List<NonResourcePolicyRuleV1>();

        /// <summary>
        ///     Determine whether the <see cref="NonResourceRules"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeNonResourceRules() => NonResourceRules.Count > 0;

        /// <summary>
        ///     `resourceRules` is a slice of ResourcePolicyRules that identify matching requests according to their verb and the target resource. At least one of `resourceRules` and `nonResourceRules` has to be non-empty.
        /// </summary>
        [YamlMember(Alias = "resourceRules")]
        [JsonProperty("resourceRules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<ResourcePolicyRuleV1> ResourceRules { get; } = new List<ResourcePolicyRuleV1>();

        /// <summary>
        ///     Determine whether the <see cref="ResourceRules"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeResourceRules() => ResourceRules.Count > 0;

        /// <summary>
        ///     subjects is the list of normal user, serviceaccount, or group that this rule cares about. There must be at least one member in this slice. A slice that includes both the system:authenticated and system:unauthenticated user groups matches every request. Required.
        /// </summary>
        [YamlMember(Alias = "subjects")]
        [JsonProperty("subjects", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<SubjectV1> Subjects { get; } = new List<SubjectV1>();
    }
}
