using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        public string Rule { get; set; }

        /// <summary>
        ///     seLinuxOptions required to run as; required for MustRunAs More info: https://git.k8s.io/community/contributors/design-proposals/security_context.md
        /// </summary>
        [JsonProperty("seLinuxOptions")]
        public SELinuxOptionsV1 SeLinuxOptions { get; set; }
    }
}
