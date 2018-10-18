using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SELinuxStrategyOptions defines the strategy type and any options used to create the strategy. Deprecated: use SELinuxStrategyOptions from policy API Group instead.
    /// </summary>
    public partial class SELinuxStrategyOptionsV1Beta1
    {
        /// <summary>
        ///     rule is the strategy that will dictate the allowable labels that may be set.
        /// </summary>
        [YamlMember(Alias = "rule")]
        [JsonProperty("rule", NullValueHandling = NullValueHandling.Include)]
        public string Rule { get; set; }

        /// <summary>
        ///     seLinuxOptions required to run as; required for MustRunAs More info: https://kubernetes.io/docs/tasks/configure-pod-container/security-context/
        /// </summary>
        [YamlMember(Alias = "seLinuxOptions")]
        [JsonProperty("seLinuxOptions", NullValueHandling = NullValueHandling.Ignore)]
        public SELinuxOptionsV1 SeLinuxOptions { get; set; }
    }
}
