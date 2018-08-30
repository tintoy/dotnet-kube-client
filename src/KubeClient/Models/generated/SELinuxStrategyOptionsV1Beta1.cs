using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace KubeClient.Models
{
    /// <summary>
    ///     SELinux  Strategy Options defines the strategy type and any options used to create the strategy.
    /// </summary>
    public partial class SELinuxStrategyOptionsV1Beta1
    {
        /// <summary>
        ///     type is the strategy that will dictate the allowable labels that may be set.
        /// </summary>
        [JsonProperty("rule")]
        [YamlMember(Alias = "rule")]
        public virtual string Rule { get; set; }

        /// <summary>
        ///     seLinuxOptions required to run as; required for MustRunAs More info: https://git.k8s.io/community/contributors/design-proposals/security_context.md
        /// </summary>
        [JsonProperty("seLinuxOptions")]
        [YamlMember(Alias = "seLinuxOptions")]
        public virtual SELinuxOptionsV1 SeLinuxOptions { get; set; }
    }
}
