using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     HPAScalingRules configures the scaling behavior for one direction. These Rules are applied after calculating DesiredReplicas from metrics for the HPA. They can limit the scaling velocity by specifying scaling policies. They can prevent flapping by specifying the stabilization window, so that the number of replicas is not set instantly, instead, the safest value from the stabilization window is chosen.
    /// </summary>
    public partial class HPAScalingRulesV2
    {
        /// <summary>
        ///     policies is a list of potential scaling polices which can be used during scaling. At least one policy must be specified, otherwise the HPAScalingRules will be discarded as invalid
        /// </summary>
        [YamlMember(Alias = "policies")]
        [JsonProperty("policies", ObjectCreationHandling = ObjectCreationHandling.Reuse)]
        public List<HPAScalingPolicyV2> Policies { get; } = new List<HPAScalingPolicyV2>();

        /// <summary>
        ///     Determine whether the <see cref="Policies"/> property should be serialised.
        /// </summary>
        public bool ShouldSerializePolicies() => Policies.Count > 0;

        /// <summary>
        ///     stabilizationWindowSeconds is the number of seconds for which past recommendations should be considered while scaling up or scaling down. StabilizationWindowSeconds must be greater than or equal to zero and less than or equal to 3600 (one hour). If not set, use the default values: - For scale up: 0 (i.e. no stabilization is done). - For scale down: 300 (i.e. the stabilization window is 300 seconds long).
        /// </summary>
        [YamlMember(Alias = "stabilizationWindowSeconds")]
        [JsonProperty("stabilizationWindowSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public int? StabilizationWindowSeconds { get; set; }

        /// <summary>
        ///     selectPolicy is used to specify which policy should be used. If not set, the default value Max is used.
        /// </summary>
        [YamlMember(Alias = "selectPolicy")]
        [JsonProperty("selectPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public string SelectPolicy { get; set; }
    }
}
