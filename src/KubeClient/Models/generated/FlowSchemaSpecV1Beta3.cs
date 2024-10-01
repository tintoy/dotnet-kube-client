using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     FlowSchemaSpec describes how the FlowSchema's specification looks like.
    /// </summary>
    public partial class FlowSchemaSpecV1Beta3
    {
        /// <summary>
        ///     `distinguisherMethod` defines how to compute the flow distinguisher for requests that match this schema. `nil` specifies that the distinguisher is disabled and thus will always be the empty string.
        /// </summary>
        [YamlMember(Alias = "distinguisherMethod")]
        [JsonProperty("distinguisherMethod", NullValueHandling = NullValueHandling.Ignore)]
        public FlowDistinguisherMethodV1Beta3 DistinguisherMethod { get; set; }

        /// <summary>
        ///     `matchingPrecedence` is used to choose among the FlowSchemas that match a given request. The chosen FlowSchema is among those with the numerically lowest (which we take to be logically highest) MatchingPrecedence.  Each MatchingPrecedence value must be ranged in [1,10000]. Note that if the precedence is not specified, it will be set to 1000 as default.
        /// </summary>
        [YamlMember(Alias = "matchingPrecedence")]
        [JsonProperty("matchingPrecedence", NullValueHandling = NullValueHandling.Ignore)]
        public int? MatchingPrecedence { get; set; }

        /// <summary>
        ///     `priorityLevelConfiguration` should reference a PriorityLevelConfiguration in the cluster. If the reference cannot be resolved, the FlowSchema will be ignored and marked as invalid in its status. Required.
        /// </summary>
        [YamlMember(Alias = "priorityLevelConfiguration")]
        [JsonProperty("priorityLevelConfiguration", NullValueHandling = NullValueHandling.Include)]
        public PriorityLevelConfigurationReferenceV1Beta3 PriorityLevelConfiguration { get; set; }

        /// <summary>
        ///     `rules` describes which requests will match this flow schema. This FlowSchema matches a request if and only if at least one member of rules matches the request. if it is an empty slice, there will be no requests matching the FlowSchema.
        /// </summary>
        [YamlMember(Alias = "rules")]
        [JsonProperty("rules", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<PolicyRulesWithSubjectsV1Beta3> Rules { get; } = new List<PolicyRulesWithSubjectsV1Beta3>();

        /// <summary>
        ///     Determine whether the <see cref="Rules"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializeRules() => Rules.Count > 0;
    }
}
